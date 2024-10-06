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
        Debug.Log(_owner.isOnGround);
        Vector3 force = new Vector3(_command.normalizedDirection.x, 0,
            _command.normalizedDirection.z) * _command.value;
        _owner.rb.AddForce(force, ForceMode.Impulse);
    }
}

// Allows the player to jump upwards with a "value" amount of force
public class JumpCommand : Command
{
    public override void Execute(Human _owner, InputCommand _command)
    {
        if (_owner.isOnGround)
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
