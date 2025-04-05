using Godot;
using System;
using System.Linq;

public class LettersCounter : PanelContainer
{
    private int totalLetters = 0;
    private int lettersOpened = 0;

    private Label _label;
    public override void _Ready()
    {
        foreach(var letter in GetTree().GetNodesInGroup("Letters").OfType<LetterArea>())
        {
            letter.Connect(nameof(LetterArea.LetterDiscovered), this, nameof(_on_Letters_LetterDiscovered));
            totalLetters++;
        }

        _label = GetNode<Label>("HBox/Label");

        _UpdateLabel();
    }

    public void _on_Letters_LetterDiscovered(string identifier, bool isClue)
    {
        lettersOpened++;
        _UpdateLabel();
    }

    protected void _UpdateLabel()
    {
        _label.Text = $"{lettersOpened} / {totalLetters}";
    }
}
