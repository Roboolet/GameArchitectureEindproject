using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Controller
{
    private StateRunner<EnemyController> stateRunner;
    private Vector3 targetPosition;

    protected override void ProcessSensoryData(SensoryData _sensoryData)
    {
    }

    protected override void PumpedFixedUpdate()
    {
    }

    protected override void PumpedUpdate()
    {
        if (stateRunner == null) { CreateStateRunner(); }

        stateRunner.Update();
    }

    private void CreateStateRunner()
    {
        EnemyIdleState stateIdle = new EnemyIdleState();
        EnemySeesTarget transNotSeesTarget = new EnemySeesTarget(stateIdle, true);
        EnemySeesTarget transSeesTarget = new EnemySeesTarget(stateIdle, false);

        stateRunner = new StateRunner<EnemyController>(this, stateIdle);
    }
}

public class EnemyIdleState : IStateRunnerBehaviour<EnemyController>
{
    public void OnEnter(EnemyController _owner) { Debug.Log("Enemy entering Idle"); }

    public void OnExit(EnemyController _owner) { }

    public void OnUpdate(EnemyController _owner) { }
}

public class EnemySeesTarget : IStateRunnerTransition<EnemyController>
{
    public bool Inverted { get; set; }
    public IStateRunnerBehaviour<EnemyController> NextBehaviour { get; set; }

    public bool CheckRequirements(EnemyController _owner)
    {
        return true;
    }

    public EnemySeesTarget(IStateRunnerBehaviour<EnemyController> _nextBehaviour, bool _inverted = false)
    {
        NextBehaviour = _nextBehaviour;
        Inverted = _inverted;
    }
}
