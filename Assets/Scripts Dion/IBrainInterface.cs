using System;
using UnityEngine;

public struct InputCommand
{
    public InputCommand(InputCommandAction _action, Vector3 _normalizedDirection, float _value)
    {
        action = _action;
        normalizedDirection = _normalizedDirection;
        value = _value;
    }

    public InputCommandAction action;
    public Vector3 normalizedDirection;
    public float value;
}

public struct SensoryData
{
    public Vector3 position;
    public float lookRotation;
    public bool isOnGround;
    public bool isAlive;
}

public enum InputCommandAction
{
    MOVE = 0,
    JUMP = 1,
    DASH = 2
}

public interface IBrainInterface
{
    public Action<SensoryData> sensoryEvent { get; set; }
    public void ReceiveInputCommand(InputCommand _command);
}
