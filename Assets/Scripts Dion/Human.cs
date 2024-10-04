using UnityEngine;

public class Human : IBrainInterface
{
    public GameObject gameObject;
    public Rigidbody rb;

    private MoveCommand moveCommand;
    private JumpCommand jumpCommand;
    private DashCommand dashCommand;

    public Human()
    {
        gameObject = new GameObject();
        moveCommand = new MoveCommand();
        jumpCommand = new JumpCommand();
        dashCommand = new DashCommand();
    }

    public IBrainInterface.SensoryEvent sensoryEvent { get; private set; }
    
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
