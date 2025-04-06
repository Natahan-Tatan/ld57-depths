using Godot;
using System;

public class GameOver : PanelContainer
{
    [Export(PropertyHint.File,"*.tscn")]
    public string FirstLevel {get;set;}

    public override void _Ready()
    {
        base._Ready();

        GetNode<Button>("VBoxContainer/Buttons/RetryButton").GrabFocus();
    }

    public void _on_QuitButton_pressed()
    {
        GetTree().Quit();
    }

    public void _on_RetryButton_pressed()
    {
        GetTree().ChangeScene(FirstLevel);
    }
}
