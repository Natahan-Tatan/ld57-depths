using Godot;
using System;
using System.Linq;

public class CluesCounter : PanelContainer
{
    public const int TOTAL_LETTERS = 3;
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
        if(isClue)
        {
            LettersOpened++;
            _UpdateLabel();
        }
    }

    protected void _UpdateLabel()
    {
        _label.Text = $"{LettersOpened} / {TOTAL_LETTERS}";
    }
}
