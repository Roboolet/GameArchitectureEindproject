using System;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Human : IBrainInterface, IUpdateable
{
    public GameObject gameObject;
    public Rigidbody rb;
    public bool IsOnGround { get; private set;}
    public bool HasJump { get; set; }
    public bool HasDash { get; set; }

    private MoveCommand moveCommand;
    private JumpCommand jumpCommand;
    private DashCommand dashCommand;

    private const float GRAVITY_SCALE = 0.7f;

    public Human()
    {
        moveCommand = new MoveCommand();
        jumpCommand = new JumpCommand();
        dashCommand = new DashCommand();
    }

    public Action<SensoryData> sensoryEvent { get; set; }

    public void PumpedFixedUpdate()
    {
        // gravity
        if (!IsOnGround)
        {
            rb.AddForce(Vector3.down * GRAVITY_SCALE, ForceMode.Impulse);
        }

        // send sensory data
        SensoryData data = new SensoryData();
        data.position = gameObject.transform.position;
        data.lookRotation = gameObject.transform.rotation.y;
        data.isOnGround = IsOnGround;
        sensoryEvent?.Invoke(data);
    }

    public void PumpedUpdate()
    {
        // ground check
        if (Physics.Raycast(gameObject.transform.position, Vector3.down, 1f))
        {
            IsOnGround = true;
            HasJump = true;
            HasDash = true;
        }
        else
        {
            IsOnGround = false;
        }
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
