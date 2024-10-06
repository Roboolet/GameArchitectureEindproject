using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Controller
{
    // pathfinding constants
    private const float maximumAirGap = 2;

    private StateRunner<EnemyController> stateRunner;
    private Vector3 humanPosition;
    private Vector3 endGoalPosition;
    private PathingNode targetNode;

    protected override void ProcessSensoryData(SensoryData _sensoryData)
    {
        humanPosition = _sensoryData.position;
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
        // create states
        EnemyIdleState stateIdle = new EnemyIdleState();
        EnemyChaseState stateChase = new EnemyChaseState();

        // create transitions
        EnemySeesTarget transNotSeesTarget = new EnemySeesTarget(stateIdle, true);
        EnemySeesTarget transSeesTarget = new EnemySeesTarget(stateChase, false);

        // create and initialize state runner
        stateRunner = new StateRunner<EnemyController>(this, stateIdle);
        stateRunner.AddTransition<EnemyIdleState>(transSeesTarget);
        stateRunner.AddTransition<EnemyChaseState>(transNotSeesTarget);
    }

    private PathingNode FindNextPathNode()
    {
        PathingNode nextNode = new PathingNode();

        // direct line towards end goal (the player)
        Vector3 step = (humanPosition - endGoalPosition).normalized * PathingSystem.gridScalingFactor;
        nextNode = GetNodeChain(step, 10)[0];

        // if the direct path is unsuitable, try various other lines

        return nextNode;
    }

    private PathingNode[] GetNodeChain(Vector3 _step, int _arrayLength)
    {
        PathingNode[] nodeChain = new PathingNode[_arrayLength];
        for (int i = 0; i < _arrayLength; i++)
        {
            // iterate through a line and find the nearby nodes
            Vector3 pos = humanPosition + _step * i;
            nodeChain[i] = PathingSystem.GetClosestNode(pos);
        }

        return nodeChain;
    }
}

public class EnemyIdleState : IStateRunnerBehaviour<EnemyController>
{
    public void OnEnter(EnemyController _owner) { Debug.Log("Enemy entering Idle"); }

    public void OnExit(EnemyController _owner) { }

    public void OnUpdate(EnemyController _owner) { }
}

public class EnemyChaseState : IStateRunnerBehaviour<EnemyController>
{
    public void OnEnter(EnemyController _owner) { Debug.Log("Enemy entering Chase"); }

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
