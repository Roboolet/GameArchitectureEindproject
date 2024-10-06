using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Human : IBrainInterface, IUpdateable
{
    public GameObject gameObject;
    public Rigidbody rb;
    public bool isOnGround;

    private MoveCommand moveCommand;
    private JumpCommand jumpCommand;
    private DashCommand dashCommand;

    public Human()
    {
        moveCommand = new MoveCommand();
        jumpCommand = new JumpCommand();
        dashCommand = new DashCommand();
    }

    public IBrainInterface.SensoryEvent sensoryEvent { get; private set; }

    public void PumpedFixedUpdate()
    {
        // ground check
        if (Physics.Raycast(gameObject.transform.position, Vector3.down, 0.1f))
        {
            isOnGround = true;
        }
        else 
        { 
            isOnGround = false; 
        }

        // send sensory data
        SensoryData data = new SensoryData();
        data.position = gameObject.transform.position;
        data.lookRotation = gameObject.transform.rotation.y;
        data.isOnGround = isOnGround;
        sensoryEvent?.Invoke();
    }

    public void PumpedUpdate()
    {
    }

    // Receives input from controller, processes which method to call 
    // depending on the InputCommand _command input.
    public void ReceiveInputCommand(InputCommand _command)
    {
        switch(_command.action)
        {
            case InputCommandAction.MOVE:
                moveCommand.Execute(this, _command);
                break;

            case InputCommandAction.JUMP:
                jumpCommand.Execute(this, _command);
                break;

            case InputCommandAction.DASH:
                dashCommand.Execute(this, _command);
                break;
        }
    }
}
