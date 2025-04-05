using Godot;
using System;
using System.Linq;

public class SleepIndicator : PanelContainer
{
    private int Remaining = 3;
    private RichTextLabel _label;
    public override void _Ready()
    {
        _label = GetNode<RichTextLabel>("RichTextLabel");
        _UpdateIndicator();
    }

    protected void _UpdateIndicator()
    {
        _label.BbcodeText = $"[center][wave amp=70.0 freq=2.5.0 connected=1]{string.Join("",Enumerable.Repeat("💤",Remaining))}[/wave][/center]";
    }
}
