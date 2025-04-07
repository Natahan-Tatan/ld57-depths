using Godot;
using System;

public class Warning : PanelContainer
{
    public override void _Ready()
    {
        base._Ready();

        GetNode<Button>("VBoxContainer/Buttons/OkButton").GrabFocus();
    }
    public void _on_OkButton_pressed()
    {
        GetTree().ChangeScene("res://UI/Screens/Intro.tscn");
    }
}
