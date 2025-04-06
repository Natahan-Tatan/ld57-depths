using Godot;
using System;
using System.Linq;

public class Shadow : Character
{
    [Export]
    public float WaitingDuration
    {
        get => _waitingDuration;
        set
        {
            _waitingDuration = value;

            if(_waitingTimer != null)
            {
                _waitingTimer.WaitTime = value;
            }
        }
    }
    private float _waitingDuration;

    [Export]
    public bool GoLeft {get; set;}

    private Timer _waitingTimer;
    private RayCast2D[] _floorRayCast;
    public override void _Ready()
    {
        base._Ready();

        _waitingTimer = GetNode<Timer>("WaitingTimer");
        _floorRayCast = GetChildren().OfType<RayCast2D>().ToArray();

        GoLeft = !GoLeft;
        _on_WaitingTimer_timeout();
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        if(_currentAction != Action.IDLE)
        {
            if(IsOnWall() || !IsOnFloor() || IsOnCeiling() || _floorRayCast.Any(r => r.Enabled && !r.IsColliding()))
            {
                _StartWaiting();
            }
        }
    }

    protected void _StartWaiting()
    {
        _currentAction = Action.IDLE;
        _waitingTimer.Start();
    }

    public void _on_WaitingTimer_timeout()
    {
        if(GoLeft)
        {
            _currentAction = Action.MOVE_RIGHT;
            _floorRayCast.First(r => r.Name.Contains("Left")).Enabled = false;
            _floorRayCast.First(r => r.Name.Contains("Right")).Enabled = true;
        }
        else
        {
            _currentAction = Action.MOVE_LEFT;
            _floorRayCast.First(r => r.Name.Contains("Left")).Enabled = true;
            _floorRayCast.First(r => r.Name.Contains("Right")).Enabled = false;
        }

        GoLeft = !GoLeft;
    }
}
