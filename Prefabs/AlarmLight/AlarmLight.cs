using Godot;
using System;
using System.Linq;

public class AlarmLight : Light2D
{
    [Signal]
    public delegate void PlayerDetected();
    private RayCast2D[] _raycasts;

    [Export]
    public bool Monitoring
    {
        get => base.Visible;
        set{
            base.Visible = value;

            if(_raycasts != null)
            {
                foreach(var r in _raycasts)
                {
                    r.Enabled = value;
                }
            }
        }
    }

    public override void _Ready()
    {
        _raycasts = GetChildren().OfType<RayCast2D>().ToArray();
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        if(_raycasts.Any(ray => ray.IsColliding() && ray.GetCollider() is Player player && !player.IsRecovering))
        {
            EmitSignal(nameof(PlayerDetected));
        }
    }
}
