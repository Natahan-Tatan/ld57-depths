using Godot;
using System;

public class Letter : MarginContainer
{
    private ScrollContainer _scrollContainer;
    public override void _Ready()
    {
        _scrollContainer = GetNode<ScrollContainer>("VBoxContainer/ScrollContainer");
    }

}
