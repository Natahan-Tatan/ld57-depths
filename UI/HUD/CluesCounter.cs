using Godot;
using System;
using System.Linq;

public class CluesCounter : PanelContainer
{
    public const int TOTAL_LETTERS = 5;
    public static int LettersOpened {get; protected set;} = 5;

    private Label _label;
    public override void _Ready()
    {
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
