using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InputCommand
{
    //public InputCommand()
    //{

    //}

    public InputCommandAction action;
    public Vector3 normalizedDirection;
    public float value;
}

public struct SensoryData
{
    public Vector3 position;
    public float lookRotation;
    public bool isOnGround;
}

public enum InputCommandAction
{
    MOVE = 0,
    JUMP = 1,
    DASH = 2
}

public interface IBrainInterface
{
    SensoryData SensoryEvent { get; }
    public void ReceiveInputCommand(InputCommand _command);
}
