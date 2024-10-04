using UnityEngine;

public class Human : IBrainInterface
{
    public GameObject gameObject;
    private Rigidbody rb;

    public Human()
    {
        gameObject = new GameObject();
    }

    public IBrainInterface.SensoryEvent sensoryEvent { get; private set; }
    
    // Receives input from controller, processes which method to call 
    // depending on the InputCommand _command input.
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
        rb.AddForce(_command.normalizedDirection * _command.value, ForceMode.Impulse);
    }

    private void Jump(InputCommand _command)
    {
        if (IsGrounded())
        {
            rb.AddForce(_command.normalizedDirection * _command.value, ForceMode.Impulse);
        }
    }

    private void Dash(InputCommand _command)
    {
        rb.AddForce(_command.normalizedDirection * _command.value, ForceMode.Impulse);
    }


    private bool IsGrounded()
    {
        return Physics.Raycast(gameObject.transform.position, Vector3.down, 0.1f);
    }
}
