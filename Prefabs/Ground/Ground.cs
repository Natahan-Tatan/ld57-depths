using Godot;
using System;

[Tool]
public class Ground : Polygon2D
{
    #if TOOLS
        private CollisionPolygon2D _collisionBox;
        private OccluderPolygon2D _lightOccluderPolygon;

        public override void _Ready()
        {
            _collisionBox = GetNode<CollisionPolygon2D>("StaticBody2D/CollisionPolygon2D");
            _lightOccluderPolygon = GetNode<LightOccluder2D>("LightOccluder2D").Occluder;
        }
        
        public override void _Process(float delta)
        {
            if(Engine.EditorHint)
            {
                base._Process(delta);
                _lightOccluderPolygon.Polygon = _collisionBox.Polygon = Polygon;
            }
        }
    #endif
}
