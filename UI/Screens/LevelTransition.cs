using Godot;
using System;
using System.Linq;

public class LevelTransition : PanelContainer
{
    public static int CurrentDepth {get; protected set;} = 1;

    [Export(PropertyHint.File,"*.tscn")]
    public string LightLevel_1 {get;set;}

    [Export(PropertyHint.File,"*.tscn")]
    public string LightLevel_2 {get;set;}

    [Export(PropertyHint.File,"*.tscn")]
    public string DeepLevel_1 {get;set;}

    [Export(PropertyHint.File,"*.tscn")]
    public string DeepLevel_2 {get;set;}

    [Export(PropertyHint.File,"*.tscn")]
    public string DeepestLevel_1 {get;set;}

    [Export(PropertyHint.File,"*.tscn")]
    public string DeepestLevel_2 {get;set;}


    public override void _Ready()
    {
        base._Ready();

        GetNode<Button>("VBoxContainer/Buttons/ContinueButton").GrabFocus();

        if(CurrentDepth <= 1)
        {
            if(SleepIndicator.LastRemaining < 3)
            {
                GetNode<Control>("VBoxContainer/Light").Visible = true;
                GetNode<TextureRect>("VBoxContainer/HBoxContainer/ArrowStay").Visible = true;
            }
            else
            {
                GetNode<Control>("VBoxContainer/Deeper").Visible = true;
                GetNode<TextureRect>("VBoxContainer/HBoxContainer/Arrow").Visible = true;
            }
        }
        else if(CurrentDepth == 2)
        {
            if(SleepIndicator.LastRemaining < 2)
            {
                GetNode<Control>("VBoxContainer/Light").Visible = true;
                GetNode<TextureRect>("VBoxContainer/HBoxContainer/Arrow").FlipV = true;
                GetNode<TextureRect>("VBoxContainer/HBoxContainer/Arrow").Visible = true;
            }
            else if(SleepIndicator.LastRemaining == 2)
            {
                GetNode<Control>("VBoxContainer/Deeper").Visible = true;
                GetNode<TextureRect>("VBoxContainer/HBoxContainer/ArrowStay").Visible = true;
            }
            else
            {
                GetNode<Control>("VBoxContainer/Deepest").Visible = true;
                GetNode<TextureRect>("VBoxContainer/HBoxContainer/Arrow").Visible = true;
            }
        }
        else if(CurrentDepth >= 3)
        {
            if(CluesCounter.LettersOpened < CluesCounter.TOTAL_LETTERS)
            {
                if(SleepIndicator.LastRemaining >= 2)
                {
                    GetNode<Control>("VBoxContainer/MissClues").Visible = true;
                    GetNode<Control>("VBoxContainer/LevelsButtons").Visible = true;
                    GetNode<Button>("VBoxContainer/LevelsButtons/DeepestButton").GrabFocus();
                }
                else
                {
                    GetNode<Control>("VBoxContainer/Congrat").Visible = true;
                }
            }
            else
            {
                GetNode<Control>("VBoxContainer/HBoxContainer/Prisonner").Visible = false;
                GetNode<Control>("VBoxContainer/HBoxContainer/Prisonner_crying").Visible = true;
                GetNode<Control>("VBoxContainer/Thanks!").Visible = true;
                GetNode<Control>("VBoxContainer/HBoxContainer/Outro").Visible = true;
                GetNode<Control>("VBoxContainer/Congrat").Visible = true;
            }
        }
    }
}
