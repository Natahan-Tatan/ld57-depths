using Godot;

public class Player : Character
{
    [Signal]
    public delegate void FallOutOfBound();

    public bool IsRecovering {get; protected set;} = false;

    private Timer _recoverTimer;
    private int _recoveringAnimCounter = 3;

    private Vector2 _initialPosition;

    public override void _Ready()
    {
        base._Ready();

        _initialPosition = Position;

        foreach(var alarm in GetTree().GetNodesInGroup("Alarms"))
        {
            if(alarm is AlarmLight light)
            {
                light.Connect(nameof(AlarmLight.PlayerDetected), this, nameof(_on_AlarmLight_PlayerDetected));
            }
            // Ajout d'autres types d'alarmes ici
        }

        _recoverTimer = GetNode<Timer>("RecoverTimer");
    }

    public void _on_AlarmLight_PlayerDetected()
    {
        IsRecovering = true;
        _recoverTimer.Start();
    }

    public void _on_RecoverTimer_timeout()
    {
        IsRecovering = false;
    }

    public override void _PhysicsProcess(float delta)
    {

        if(Input.IsActionPressed("Left"))
        {
            _currentAction |= Action.MOVE_LEFT;
        }
        else
        {
            _currentAction &= ~Action.MOVE_LEFT;
        }

        if(Input.IsActionPressed("Right"))
        {
            _currentAction |= Action.MOVE_RIGHT;
        }
        else
        {
            _currentAction &= ~Action.MOVE_RIGHT;
        }

        if(Input.IsActionJustPressed("Jump"))
        {
            _currentAction |= Action.JUMP;
        }
        else
        {
            _currentAction &= ~Action.JUMP;
        }

        base._PhysicsProcess(delta);

        if(IsRecovering)
        {
            _recoveringAnimCounter--;
            if(_recoveringAnimCounter <= 0)
            {
                _appearance.Visible = !_appearance.Visible;
                _recoveringAnimCounter = 3;
            }
        }
        else
        {
            _appearance.Visible = true;
        }
    }

    protected override void _OutOfBound()
    {
        EmitSignal(nameof(FallOutOfBound));
        Position = _initialPosition;
        IsRecovering = true;
        _recoverTimer.Start();
    }
}
