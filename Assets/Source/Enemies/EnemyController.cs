using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : Controller
{
    public Transform Target { get; private set; }

    // pathfinding constants
    private const float MAXIMUM_AIR_GAP = 2;
    private const float INTERMEDIATE_TARGET_DISTANCE_REQUIRED = 3;

    private StateRunner<EnemyController> stateRunner;
    private Vector3 humanPosition;
    private PathingNode intermediateTarget;

    private bool sensesConnected = false;
    private SensoryData lastSensoryUpdate;

    protected override void ProcessSensoryData(SensoryData _sensoryData)
    {
        lastSensoryUpdate = _sensoryData;
        humanPosition = _sensoryData.position;
    }

    public override void PumpedFixedUpdate()
    {
        if (Target == null) { return; }

        if ((humanPosition - intermediateTarget.position).sqrMagnitude
            <= INTERMEDIATE_TARGET_DISTANCE_REQUIRED)
        {
            intermediateTarget = FindNextPathNode();
        }

        Vector3 vecToPlayer = (intermediateTarget.position - humanPosition).normalized;

        // walk
        InputCommand walkCommand = new InputCommand();
        walkCommand.action = InputCommandAction.MOVE;
        walkCommand.value = 0.8f;
        walkCommand.normalizedDirection = vecToPlayer
            ;
        Avatar.ReceiveInputCommand(walkCommand);

        // jump
        if (!lastSensoryUpdate.isOnGround)
        {
            InputCommand jumpCommand = new InputCommand();
            jumpCommand.action = InputCommandAction.JUMP;
            jumpCommand.value = 1;
            jumpCommand.normalizedDirection = Vector3.up;
            Avatar.ReceiveInputCommand(jumpCommand);
        }
    }

    public override void PumpedUpdate()
    {
        if (!sensesConnected)
        {
            sensesConnected = true;
            Avatar.sensoryEvent += ProcessSensoryData;
        }

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

    public void MoveTowards(Transform _transform)
    {
        Target = _transform;        
    }

    public void Stop()
    {
        Target = null;
    }

    private PathingNode FindNextPathNode()
    {
        PathingNode nextNode = new PathingNode();

        // direct line towards Target
        Vector3 step = (Target.position - humanPosition).normalized * PathingSystem.GRID_SCALING_FACTOR;
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
    public void OnEnter(EnemyController _owner)
    {
        Debug.Log("Enemy entering Chase");
        _owner.MoveTowards(GameObject.FindGameObjectWithTag("Player").transform);
    }

    public void OnExit(EnemyController _owner)
    {
        _owner.Stop();
    }

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
