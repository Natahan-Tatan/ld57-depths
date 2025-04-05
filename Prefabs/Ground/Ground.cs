using Godot;
using System;

[Tool]
public class Ground : StaticBody2D
{
    private Polygon2D _geometry;
    private CollisionPolygon2D _collisionBox;
    private OccluderPolygon2D _lightOccluderPolygon;

    public override void _Ready()
    {
        _geometry = GetNode<Polygon2D>("Polygon2D");
        _collisionBox = GetNode<CollisionPolygon2D>("CollisionPolygon2D");
        _lightOccluderPolygon = GetNode<LightOccluder2D>("LightOccluder2D").Occluder;
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        #if TOOLS
            _lightOccluderPolygon.Polygon = _collisionBox.Polygon = _geometry.Polygon;
        #endif
    }

}
