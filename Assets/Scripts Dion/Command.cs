using UnityEngine;

public abstract class Command
{
    public abstract void Execute(Human _owner, InputCommand _command);
}

// Allows the player to move over the ground using a "value" amount of speed
public class MoveCommand : Command
{
    private const float SPEED = 50;

    public override void Execute(Human _owner, InputCommand _command)
    {
        Debug.Log(_owner.IsOnGround);
        Vector3 force = new Vector3(_command.normalizedDirection.x, 0,
            _command.normalizedDirection.z) * _command.value * SPEED * Time.fixedDeltaTime;
        _owner.rb.AddForce(force, ForceMode.Impulse);
    }
}

// Allows the player to jump upwards with a "value" amount of force
public class JumpCommand : Command
{
    private const float JUMP_FORCE = 14;

    public override void Execute(Human _owner, InputCommand _command)
    {
        if (_owner.HasJump)
        {
            _owner.rb.AddForce(_command.normalizedDirection * _command.value * JUMP_FORCE, ForceMode.Impulse);
            _owner.HasJump = false;
        }
    }
}

// Allows the player to dash forward a "value" amount of force
public class DashCommand : Command
{
    private const float DASH_SPEED = 50;

    public override void Execute(Human _owner, InputCommand _command)
    {
        _owner.rb.AddForce(_command.normalizedDirection * _command.value * DASH_SPEED, ForceMode.Impulse);
    }
}
