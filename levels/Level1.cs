using Godot;
using System;

public partial class Level1 : Node2D
{
    private RichTextLabel logLabel;
	private AudioStreamPlayer music;
	public override void _Ready()
	{
		music = GetNode<AudioStreamPlayer>("BackgroundMusic");
		music.Playing = true;
        
		logLabel = GetNode<RichTextLabel>("CanvasLayer/Logcount/Count");
        logLabel.AddText("0");
	}
	public void _on_background_music_finished()
	{
		music.Playing = true;
	}
}
