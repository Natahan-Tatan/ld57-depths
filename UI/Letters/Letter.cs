using Godot;
using System;
using System.Linq;

public class Letter : MarginContainer
{
    [Signal]
    public delegate void Closed();

    [Export(PropertyHint.Range,"100,500")]
    public int ScrollSpeed {get;set;} = 20;

    [Export]
    public new bool Visible {
        get => base.Visible;
        set {
            base.Visible = value;
            
            if(_layers != null)
            {
                foreach(var l in _layers)
                {
                    l.Visible = value;
                }
            }
        }
    } 
    
    private ScrollContainer _scrollContainer;
    private  CanvasLayer[] _layers;

    public override void _Ready()
    {
        _scrollContainer = GetNode<ScrollContainer>("VBoxContainer/Scroll/ScrollContainer");

        _layers = GetNode<Control>("VBoxContainer").GetChildren().OfType<CanvasLayer>().ToArray();

        Visible = Visible;
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

    public void _on_CloseButton_pressed()
    {
        EmitSignal(nameof(Closed));
    }
}
