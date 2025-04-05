using Godot;
using System;

public class Letter : MarginContainer
{
    [Export(PropertyHint.Range,"100,500")]
    public int ScrollSpeed {get;set;} = 20;
    private ScrollContainer _scrollContainer;
    public override void _Ready()
    {
        _scrollContainer = GetNode<ScrollContainer>("VBoxContainer/ScrollContainer");
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        if(Input.IsActionPressed("ui_up", true))
        {
            _scrollContainer.ScrollVertical += Mathf.RoundToInt(ScrollSpeed * delta);
        }

        if(Input.IsActionPressed("ui_down", true))
        {
            _scrollContainer.ScrollVertical -= Mathf.RoundToInt(ScrollSpeed * delta);
        }
    }
}
