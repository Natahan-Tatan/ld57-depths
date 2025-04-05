using Godot;
using System;

[Tool]
public class Lanterne : Node2D
{
    [Export(PropertyHint.Range,"0,10")]
    public float SwingDuration
    {
        get => _swingDuration;
        set {
            _swingDuration = value;
            UpdateSwing();
        }
    }
    private float _swingDuration = 1f;

    [Export(PropertyHint.Range,"0,90")]
    public float SwimAngle
    {
        get => _swingAngle;
        set {
            _swingAngle = value;
            UpdateSwing();
        }
    }
    private float _swingAngle = 40f;

    private Tween _angleTween;

    private bool _back = false;
    public void UpdateSwing()
    {
        if(_angleTween != null)
        {
            _angleTween.RemoveAll();
            _back = false;
            StartSwing();
        }
    }

    public void StartSwing()
    {
        if(!_back)
        {
            _angleTween.InterpolateProperty(this,"rotation_degrees", -_swingAngle, _swingAngle, _swingDuration, Tween.TransitionType.Quad, Tween.EaseType.InOut);
        }
        else
        {
            _angleTween.InterpolateProperty(this,"rotation_degrees", _swingAngle, -_swingAngle, _swingDuration, Tween.TransitionType.Quad, Tween.EaseType.InOut);
        }

        _angleTween.Start();
    }
    public override void _Ready()
    {
        base._Ready();

        _angleTween = GetNode<Tween>("AngleTween");

        UpdateSwing();
    }

    public void _on_AngleTween_tween_all_completed()
    {
        _back = !_back;
        StartSwing();
    }   
}
