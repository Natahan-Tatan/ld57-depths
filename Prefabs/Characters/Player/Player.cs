using Godot;
using System;

public class Player : Character
{
    public override void _Process(float delta)
    {
        base._Process(delta);

        if(Input.IsActionPressed("Left"))
        {
            _currentAction |= Action.MOVE_LEFT;
        }
        else
        {
            _currentAction &= ~Action.MOVE_LEFT;
        }

        if(Input.IsActionPressed("Right"))
        {
            _currentAction |= Action.MOVE_RIGHT;
        }
        else
        {
            _currentAction &= ~Action.MOVE_RIGHT;
        }

        if(Input.IsActionJustPressed("Jump"))
        {
            _currentAction |= Action.JUMP;
        }
        else
        {
            _currentAction &= ~Action.JUMP;
        }
    }
}
