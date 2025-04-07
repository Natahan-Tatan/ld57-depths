using Godot;
using System;
using System.Linq;

public class LettersCounter : PanelContainer
{
    private const int TOTAL_LETTERS = 9;
    public static int LettersOpened {get; protected set;} = 0;

    private Label _label;
    public override void _Ready()
    {
        foreach(var letter in GetTree().GetNodesInGroup("Letters").OfType<LetterArea>())
        {
            letter.Connect(nameof(LetterArea.LetterDiscovered), this, nameof(_on_Letters_LetterDiscovered));
        }

        _label = GetNode<Label>("HBox/Label");

        _UpdateLabel();
    }

    public void _on_Letters_LetterDiscovered(string identifier, bool isClue)
    {
        LettersOpened++;
        _UpdateLabel();
    }

    protected void _UpdateLabel()
    {
        _label.Text = $"{LettersOpened} / {TOTAL_LETTERS}";
    }
}
