using Godot;
using System;
using System.Linq;

public class SleepIndicator : PanelContainer
{
    // Used by transitional scene to know which kind of level use for the next
    public static int LastRemaining;

    [Signal]
    public delegate void PrisonnerWakeUp();

    protected int Remaining
    {
        get => _remaining;
        set{
            LastRemaining = _remaining = value;

            if(_remaining <= 0)
            {
                EmitSignal(nameof(PrisonnerWakeUp));
                _remaining = 0;
            }
        }
    }
    private int _remaining = 3;

    private RichTextLabel _label;
    public override void _Ready()
    {
        foreach(var alarm in GetTree().GetNodesInGroup("Alarms"))
        {
            if(alarm is AlarmLight light)
            {
                light.Connect(nameof(AlarmLight.PlayerDetected), this, nameof(_on_AlarmLight_PlayerDetected));
            }
            // Ajout d'autres types d'alarmes ici
        }

        GetTree().GetFirstNodeInGroup("Player").Connect(nameof(Player.FallOutOfBound), this, nameof(_on_AlarmLight_PlayerDetected));

        _label = GetNode<RichTextLabel>("RichTextLabel");
        _UpdateIndicator();
    }

    protected void _UpdateIndicator()
    {
        _label.BbcodeText = $"[center][wave amp=70.0 freq=2.5.0 connected=1]{string.Join("",Enumerable.Repeat("💤",Remaining))}[/wave][/center]";
    }

    public void _on_AlarmLight_PlayerDetected()
    {
        Remaining--;
        _UpdateIndicator();
    }
}
