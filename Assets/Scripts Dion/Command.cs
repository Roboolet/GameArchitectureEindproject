using UnityEngine;

public abstract class Command
{
    public abstract void Execute(Human _owner, InputCommand _command);
}

// Allows the player to move over the ground using a "value" amount of speed
public class MoveCommand : Command
{
    public override void Execute(Human _owner, InputCommand _command)
    {
        _owner.rb.AddForce(_command.normalizedDirection * _command.value, ForceMode.Impulse);
    }
}

// Allows the player to jump upwards with a "value" amount of force
public class JumpCommand : Command
{
    public override void Execute(Human _owner, InputCommand _command)
    {
        if (Physics.Raycast(_owner.gameObject.transform.position, Vector3.down, 0.1f))
        {
            _owner.rb.AddForce(_command.normalizedDirection * _command.value, ForceMode.Impulse);
        }
    }
}

// Allows the player to dash forward a "value" amount of force
public class DashCommand : Command
{
    public override void Execute(Human _owner, InputCommand _command)
    {
        _owner.rb.AddForce(_command.normalizedDirection * _command.value, ForceMode.Impulse);
    }
}
