using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : IBrainInterface
{
    GameObject gameObject;

    public SensoryData SensoryEvent { get; private set; }

    public void ReceiveInputCommand(InputCommand _command)
    {
        switch(_command.action)
        {
            case InputCommandAction.MOVE:
                Move(_command);
                break;

            case InputCommandAction.JUMP:
                Jump(_command);
                break;

            case InputCommandAction.DASH:
                Dash(_command);
                break;
        }
    }

    private void Move(InputCommand _command)
    {
        // Move implementation
    }

    private void Jump(InputCommand _command)
    {
        // Jump implementation
    }

    private void Dash(InputCommand _command)
    {
        // Dash implementation
    }
}
