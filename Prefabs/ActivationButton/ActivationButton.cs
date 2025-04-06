using Godot;
using System;

public class ActivationButton : Area2D
{
    [Signal]
    public delegate void Activated(Character character);

    public void _on_ActivationButton_body_entered(Node node)
    {
        if(node is Character character)
        {
            EmitSignal(nameof(Activated), character);
            GetNode<AnimatedSprite>("AnimatedSprite").Play("enabled");
        }
    }
}
