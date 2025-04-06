using Godot;
using System;

public class DeactivatableAlarmLight : AlarmLight
{
    [Export]
    public NodePath ButtonPath {get;set;}
    private NodePath _buttonPath;
    public override void _Ready()
    {
        base._Ready();
        GetNode<ActivationButton>(ButtonPath).Connect(nameof(ActivationButton.Activated), this, nameof(_on_ActivationButton_Activated));
    }

    public void _on_ActivationButton_Activated(Character character)
    {
        Monitoring = false;
    }
}
