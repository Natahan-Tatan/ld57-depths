using Godot;
using System;
using System.Collections.Generic;

public class LetterArea : Area2D
{
    public static Dictionary<string, bool> HaveBeenOpened = new Dictionary<string, bool>();

    [Signal]
    public delegate void LetterDiscovered(string identifier, bool isClue);

    [Export]
    public string Identifier {get;set;}

    [Export]
    public bool IsClue {get;set;} = false;

    [Export(PropertyHint.MultilineText)]
    public string Content {
        get => _content;
        set {
            _content = value;
            GetNode<RichTextLabel>("Letter/VBoxContainer/Scroll/ScrollContainer/MarginContainer/PanelContainer/RichTextLabel").BbcodeText = _content;
        }
    }
    public string _content = "";

    public bool IsOpened => HaveBeenOpened[Identifier];

    public bool ClueTested {get; protected set;}

    private AnimatedSprite _animatedSprite;
    private Label _label;
    private Letter _letterUI;

    private bool _playerIn = false;
    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        _label = GetNode<Label>("LabelCanvasLayer/NodeLabel/Label");
        _letterUI = GetNode<Letter>("Letter");

        if(!HaveBeenOpened.ContainsKey(Identifier))
        {
            HaveBeenOpened[Identifier] = false;
        }
        else if(HaveBeenOpened[Identifier])
        {
            _animatedSprite.Play("opened");
        }
        else
        {
            _animatedSprite.Play("closed");
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);

        if(_playerIn && @event.IsActionPressed("Interact"))
        {
            _animatedSprite.Play("opened");
            GetTree().Paused = true;
            _letterUI.Visible = true;
        }
    }

    public void _on_Letter_Closed()
    {
        _letterUI.Visible = false;
        GetTree().Paused = false;

        if(!HaveBeenOpened[Identifier])
        {
            HaveBeenOpened[Identifier] = true;
            EmitSignal(nameof(LetterDiscovered), Identifier, IsClue);
        }
    }

    public void _on_Letter_body_entered(Node body)
    {
        if(body is Player player)
        {
            CreateTween().TweenProperty(_label, "modulate", Colors.White, 0.3f);
            _playerIn = true;
        }
    }

    public void _on_Letter_body_exited(Node body)
    {
        if(body is Player player)
        {
            CreateTween().TweenProperty(_label, "modulate", Colors.Transparent, 0.3f);
            _playerIn = false;
        }
    }
}
