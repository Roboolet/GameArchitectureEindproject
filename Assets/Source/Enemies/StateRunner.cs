using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateRunner<T>
{
    private T owner;
    private IStateRunnerBehaviour<T> behaviour;
    private Dictionary<Type, List<IStateRunnerTransition<T>>> transitions;

    public void Update()
    {
        behaviour.OnUpdate(owner);
        CheckTransitions();
    }

    public void AddTransition<B>(IStateRunnerTransition<T> _transition)
    {
        Type behaviourType = typeof(B);
        if (!transitions.ContainsKey(behaviourType))
        {
            transitions.Add(behaviourType, new List<IStateRunnerTransition<T>>());
        }
        transitions[behaviourType].Add(_transition);
    }

    private void CheckTransitions()
    {
        if (transitions.ContainsKey(behaviour.GetType()))
        {
            foreach(IStateRunnerTransition<T> _tr in transitions[behaviour.GetType()])
            {
                bool checkSuccess = _tr.CheckRequirements(owner);
                if (_tr.Inverted) { checkSuccess = !checkSuccess; }

                if (checkSuccess)
                {
                    SetBehaviour(_tr.NextBehaviour);
                }
            }
        }
    }

    private void SetBehaviour(IStateRunnerBehaviour<T> _behaviour)
    {
        if (behaviour != null) behaviour.OnExit(owner);
        behaviour = _behaviour;
        behaviour.OnEnter(owner);
    }
}

public interface IStateRunnerTransition<T>
{
    public IStateRunnerBehaviour<T> NextBehaviour { get; protected set; }
    public bool Inverted { get; set; }
    public bool CheckRequirements(T _owner);
}

public interface IStateRunnerBehaviour<T>
{
    public void OnEnter(T _owner);
    public void OnUpdate(T _owner);
    public void OnExit(T _owner);
}
