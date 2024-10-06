using UnityEngine;

public abstract class Controller : IUpdateable
{
    public IBrainInterface Avatar { get; set; }
    public abstract void ProcessSensoryData(SensoryData _sensoryData);

    public abstract void PumpedUpdate();

    public abstract void PumpedFixedUpdate();
}

public class PlayerController : Controller
{
    private Camera cam;

    private bool wantsToJump = false;
    private bool wantsToDash = false;
    private Vector2 input;

    public override void ProcessSensoryData(SensoryData _sensoryData)
    {
        if (!_sensoryData.isAlive)
        {
            Game.State = GameState.Dead;
        }
    }

    public override void PumpedUpdate()
    {
        if(cam == null) { cam = Camera.main; }
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            wantsToJump = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            wantsToDash = true;
        }
    }

    // Handles player movement and abilities depending on user input.
    public override void PumpedFixedUpdate()
    {
        if(cam == null) { return; }

        Vector3 movement = cam.transform.forward * input.y
            + cam.transform.right * input.x;

        Avatar.ReceiveInputCommand(new InputCommand(InputCommandAction.MOVE, movement.normalized, 1));

        if (wantsToJump)
        {
            Avatar.ReceiveInputCommand(new InputCommand(InputCommandAction.JUMP, Vector3.up, 1));
            wantsToJump = false;
        }

        if (wantsToDash)
        {
            Avatar.ReceiveInputCommand(new InputCommand(InputCommandAction.DASH, cam.transform.forward, 1));
            wantsToDash = false;
        }
    }
}
