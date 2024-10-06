using UnityEngine;

public abstract class Controller : IUpdateable
{
    public IBrainInterface Avatar { get; set; }
    protected abstract void ProcessSensoryData(SensoryData _sensoryData);

    public abstract void PumpedUpdate();

    public abstract void PumpedFixedUpdate();
}

public class PlayerController : Controller
{
    private float speed = 5;
    private float jumpForce = 5;
    private float dashSpeed = 10;
    private float cooldown = 0;

    protected override void ProcessSensoryData(SensoryData _sensoryData)
    {
        ;
    }

    public override void PumpedUpdate()
    {
        ;
    }

    // Handles player movement and abilities depending on user input.
    public override void PumpedFixedUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Avatar.ReceiveInputCommand(new InputCommand(InputCommandAction.MOVE, movement, speed));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Avatar.ReceiveInputCommand(new InputCommand(InputCommandAction.JUMP, Vector3.up, jumpForce));
        }

        cooldown -= Time.fixedDeltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && cooldown <= 0)
        {
            Avatar.ReceiveInputCommand(new InputCommand(InputCommandAction.DASH, Vector3.forward, dashSpeed));
            cooldown = 3;
        }
    }
}
