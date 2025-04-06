using Godot;
using System;
using System.Linq;

public class AlarmLight : Light2D
{
    [Signal]
    public delegate void PlayerDetected();
    private RayCast2D[] _raycasts;

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
