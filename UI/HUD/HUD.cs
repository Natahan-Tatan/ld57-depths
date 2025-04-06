using Godot;
using System;

public class HUD : CanvasLayer
{
    public override void _Ready()
    {
        base._Ready();
        
        Visible = true;

        GetTree().GetFirstNodeInGroup("EndLevel").Connect(nameof(EndLevel.PlayerEnded), this, nameof(_on_EndLevel_PlayerEnded));
    }
    public void _on_SleepIndicator_PrisonnerWakeUp()
    {
        GetTree().CreateTimer(1f).Connect("timeout", this, nameof(_on_GameOverTimer_Timeout));
        CreateTween().TweenProperty(GetNode<Control>("Backdrop"), "modulate", Colors.Black, 0.6f);
    }

    public void _on_EndLevel_PlayerEnded()
    {
        GetTree().CreateTimer(1f).Connect("timeout", this, nameof(_on_EndLevelTimer_Timeout));
        CreateTween().TweenProperty(GetNode<Control>("Backdrop"), "modulate", Colors.Black, 0.6f);
    }

    public void _on_GameOverTimer_Timeout()
    {
        GetTree().ChangeScene("res://UI//Screen//GameOver.tscn");
    }

    public void _on_EndLevelTimer_Timeout()
    {
        GetTree().ChangeScene("res://UI/Screens/LevelTransition.tscn");
    }
}
