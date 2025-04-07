using Godot;
using System;

public class Prisonner : Sprite
{
    public void _on_PrisonnerLetter_LetterDiscovered(string identifier, bool isClue)
    {
        GetNode<AnimationPlayer>("AnimationPlayer").Play("Action");
    }
}
