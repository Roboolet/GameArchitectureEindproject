using UnityEngine;

// : IUpdateable
public abstract class Controller
{
    protected IBrainInterface Avatar { get; set; }
    protected abstract void ProcessSensoryData(SensoryData _sensoryData);

    // Pumped update
    // Pumped fixedupdate
}

public class PlayerController : Controller
{
    private float speed = 5;
    private float dashSpeed = 10;

    protected override void ProcessSensoryData(SensoryData _sensoryData)
    {
        ;
    }

    // Pumped update
    public void PumpedUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Avatar.ReceiveInputCommand(new InputCommand(InputCommandAction.MOVE, movement, speed));

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            float jumpForce = 5;
            Avatar.ReceiveInputCommand(new InputCommand(InputCommandAction.JUMP, Vector3.up, jumpForce));
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Avatar.ReceiveInputCommand(new InputCommand(InputCommandAction.DASH, Vector3.forward, dashSpeed));
        }
    }
    // Pumped fixedupdate
}
