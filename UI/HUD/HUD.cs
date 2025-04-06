using Godot;
using System;

public class HUD : CanvasLayer
{
    public void _on_SleepIndicator_PrisonnerWakeUp()
    {
        GetTree().CreateTimer(1f).Connect("timeout", this, nameof(_on_GameOverTimer_Timeout));
        CreateTween().TweenProperty(GetNode<Control>("Backdrop"), "modulate", Colors.Black, 0.6f);
    }

    public void _on_GameOverTimer_Timeout()
    {
        GetTree().ChangeScene("res://UI/GameOver/GameOver.tscn");
    }
}
