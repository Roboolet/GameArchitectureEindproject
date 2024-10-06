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
    private float cooldown = 0;

    private Camera cam;

    protected override void ProcessSensoryData(SensoryData _sensoryData)
    {
        
    }

    public override void PumpedUpdate()
    {
        if(cam == null) { cam = Camera.main; }

        Vector3 movement = cam.transform.forward * Input.GetAxis("Vertical")
            + cam.transform.right * Input.GetAxis("Horizontal");
        Avatar.ReceiveInputCommand(new InputCommand(InputCommandAction.MOVE, movement, 1));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Avatar.ReceiveInputCommand(new InputCommand(InputCommandAction.JUMP, Vector3.up, 1));
        }

        cooldown -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && cooldown <= 0)
        {
            Avatar.ReceiveInputCommand(new InputCommand(InputCommandAction.DASH, cam.transform.forward, 1));
            cooldown = 3;
        }
    }

    // Handles player movement and abilities depending on user input.
    public override void PumpedFixedUpdate()
    {
        
    }
}
