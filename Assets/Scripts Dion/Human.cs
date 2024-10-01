using UnityEngine;


// IUpdatable
public class Human : IBrainInterface
{
    GameObject gameObject;
    Rigidbody rb;

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
        rb.AddForce(_command.normalizedDirection * _command.value, ForceMode.Impulse);
    }

    private void Jump(InputCommand _command)
    {
        // Jump implementation
        rb.AddForce(_command.normalizedDirection * _command.value, ForceMode.Impulse);
    }

    private void Dash(InputCommand _command)
    {
        // Dash implementation
        rb.AddForce(_command.normalizedDirection * _command.value, ForceMode.Impulse);
    }
}
