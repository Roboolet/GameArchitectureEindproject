using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// : IUpdateable
public abstract class Controller
{
    IBrainInterface avatar;
    public abstract void ProcessSensoryData(SensoryData _sensoryData);

    // Pumped update
    // Pumped fixedupdate
}

public class PlayerController : Controller
{
    IBrainInterface avatar;

    public override void ProcessSensoryData(SensoryData _sensoryData)
    {
        ;
    }

    // Pumped updated
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            avatar.ReceiveInputCommand(new InputCommand());
        }
    }
    // Pumped fixedupdate
}
