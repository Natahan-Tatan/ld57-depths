using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class LevelTransition : PanelContainer
{
    public static int CurrentDepth {get; protected set;} = 1;
    private static Dictionary<int, bool> _alreadyVisited = new Dictionary<int, bool>();

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
            _alreadyVisited[1] = true;
            if(SleepIndicator.LastRemaining < 3)
            {
                GetNode<Control>("VBoxContainer/Light").Visible = true;
                GetNode<TextureRect>("VBoxContainer/HBoxContainer/ArrowStay").Visible = true;
            }
            else
            {
                GetNode<Control>("VBoxContainer/Deeper").Visible = true;
                GetNode<TextureRect>("VBoxContainer/HBoxContainer/Arrow").Visible = true;
                CurrentDepth = 2;
            }
        }
        else if(CurrentDepth == 2)
        {
            _alreadyVisited[2] = true;
            if(SleepIndicator.LastRemaining < 2)
            {
                GetNode<Control>("VBoxContainer/Light").Visible = true;
                GetNode<TextureRect>("VBoxContainer/HBoxContainer/Arrow").FlipV = true;
                GetNode<TextureRect>("VBoxContainer/HBoxContainer/Arrow").Visible = true;
                CurrentDepth = 1;
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
                CurrentDepth = 3;
            }
        }
        else if(CurrentDepth >= 3)
        {
            _alreadyVisited[3] = true;
            if(CluesCounter.LettersOpened < CluesCounter.TOTAL_LETTERS)
            {
                if(SleepIndicator.LastRemaining >= 2)
                {
                    GetNode<Control>("VBoxContainer/Deepest").Visible = true;
                    GetNode<Control>("VBoxContainer/MissClues").Visible = true;
                    GetNode<Control>("VBoxContainer/LevelsButtons").Visible = true;
                    GetNode<Control>("VBoxContainer/Buttons").Visible = false;
                    GetNode<Button>("VBoxContainer/LevelsButtons/DeepestButton").GrabFocus();
                }
                else
                {
                    GetNode<Control>("VBoxContainer/Deeper").Visible = true;
                    GetNode<TextureRect>("VBoxContainer/HBoxContainer/Arrow").FlipV = true;
                    GetNode<TextureRect>("VBoxContainer/HBoxContainer/Arrow").Visible = true;
                    CurrentDepth = 2;
                }
            }
            else
            {
                GetNode<Control>("VBoxContainer/Buttons").Visible = false;
                GetNode<Control>("VBoxContainer/HBoxContainer/Prisonner").Visible = false;
                GetNode<Control>("VBoxContainer/HBoxContainer/Prisonner_crying").Visible = true;
                GetNode<Control>("VBoxContainer/Thanks!").Visible = true;
                GetNode<Control>("VBoxContainer/HBoxContainer/Outro").Visible = true;
                GetNode<Control>("VBoxContainer/Congrat").Visible = true;
            }
        }
    }

    public void _on_LevelButton_pressed(int depth)
    {
        if(depth <= 1)
        {
            CurrentDepth = 1;
        }
        else if(depth >= 3)
        {
            CurrentDepth = 3;
        }
        else
        {
            CurrentDepth = depth;
        }
        
        _on_ContinueButton_pressed();
    }

    public void _on_ContinueButton_pressed()
    {
        string nextScene = "";

        switch(CurrentDepth)
        {
            case 3:
                if(_alreadyVisited.ContainsKey(3) && _alreadyVisited[3] && !string.IsNullOrEmpty(DeepestLevel_2))
                {
                    nextScene = DeepestLevel_2;
                }
                else
                {
                    nextScene = DeepestLevel_1;
                }
            break;

            case 2:
                if(_alreadyVisited.ContainsKey(2) && _alreadyVisited[2] && !string.IsNullOrEmpty(DeepLevel_2))
                {
                    nextScene = DeepLevel_2;
                }
                else
                {
                    nextScene = DeepLevel_1;
                }
            break;

            case 1:
            default:
                if(_alreadyVisited.ContainsKey(1) && _alreadyVisited[1] && !string.IsNullOrEmpty(LightLevel_2))
                {
                    nextScene = LightLevel_2;
                }
                else
                {
                    nextScene = LightLevel_1;
                }
            break;
        }

        GetTree().ChangeScene(nextScene);
    }
}
