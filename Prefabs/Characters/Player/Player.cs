using Godot;

public class Player : Character
{
    public bool IsRecovering {get; protected set;} = false;

    private Timer _recoverTimer;
    private int _recoveringAnimCounter = 3;

    public override void _Ready()
    {
        base._Ready();

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

    public override void _Process(float delta)
    {
        base._Process(delta);

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
    }

    public override void _PhysicsProcess(float delta)
    {
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
}
