using Godot;
using System;

public class EndLevel : Area2D
{
    [Signal]
    public delegate void PlayerEnded();

    public void _on_EndLevel_body_entered(Node body)
    {
        if(body is Player)
        {
            EmitSignal(nameof(PlayerEnded));
        }
    }
}
