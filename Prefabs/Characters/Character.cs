using System.Linq;
using Godot;
public class Character : KinematicBody2D
{
    public enum Action: int
    {
        IDLE        = 0b00000000,
        MOVE_LEFT   = 0b00000001,
        MOVE_RIGHT  = 0b00000010,
        JUMP        = 0b00000100
    }

    [Export]
    public float Speed {get;set;} = 10f;

    [Export]
    public float Jump {get;set;} = 10f;

    [Export]
    public float Gravity {get;set;} = 2f;

    protected Action _currentAction = Action.IDLE;
    private float _verticalSpeed = 0f;

    protected Node2D _appearance;

    public override void _Ready()
    {
        base._Ready();

        _appearance = GetNode<Node2D>("Appearance");
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        var movement = Vector2.Zero;

        if(_currentAction.Contains(Action.MOVE_LEFT))
        {
            _appearance.Scale = new Vector2(-Mathf.Abs(_appearance.Scale.x), _appearance.Scale.y);
            movement -= new Vector2(Speed * 300 * delta, 0);
        }

        if(_currentAction.Contains(Action.MOVE_RIGHT))
        {
            _appearance.Scale = new Vector2(Mathf.Abs(_appearance.Scale.x), _appearance.Scale.y);
            movement += new Vector2(Speed * 300 * delta, 0);
        }

        if(_currentAction.Contains(Action.JUMP) && IsOnFloor() && !IsOnCeiling() && _verticalSpeed == 0f)
        {
            _verticalSpeed = -Jump*30;
        }
        else if(!IsOnFloor())
        {
            if(IsOnCeiling())
            {
                _verticalSpeed = 0;
            }
            
            _verticalSpeed += Gravity * 30;
        }
        else
        {
            _verticalSpeed = 0;
        }

        movement += new Vector2(0, _verticalSpeed);

        if(movement.Length() != 0)
        {
            if(_verticalSpeed != 0)
            {
                MoveAndSlide(movement, Vector2.Up, stopOnSlope: true);
            }
            else
            {
                MoveAndSlideWithSnap(movement, Vector2.Down*2, Vector2.Up, stopOnSlope: true);
            }
        }
    }
}

public static class CharacterActionExtends
{
    public static bool Contains(this Character.Action action, Character.Action other)
    {
        return (action & other) == other;
    }
}
