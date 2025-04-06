using Godot;
using System;
using System.Linq;

public class Intro : PanelContainer
{
    [Export(PropertyHint.File,"*.tscn")]
    public string FirstLevel {get;set;}

    private int _currentPage = 0;
    private RichTextLabel[] _pages;
    public override void _Ready()
    {
        GetNode<Button>("VBoxContainer/Buttons/NextButton").GrabFocus();

        _pages = GetNode<Control>("VBoxContainer/HBoxContainer").GetChildren().OfType<RichTextLabel>().ToArray();
    }

    public void _on_SkipButton_pressed()
    {
        GetTree().ChangeScene(FirstLevel);
    }

    public void _on_NextButton_pressed()
    {
        _currentPage++;

        foreach(var p in _pages)
        {
            p.Visible = false;
        }

        _pages[_currentPage].Visible = true;

        if(_currentPage >= _pages.Count()-1)
        {
            GetNode<Button>("VBoxContainer/Buttons/NextButton").Visible = false;
            GetNode<Button>("VBoxContainer/Buttons/SkipButton").GrabFocus();
            GetNode<Button>("VBoxContainer/Buttons/SkipButton").Text = "Dive into mind...";
        }
    }
}
