using Godot;
using System;

public partial class Level1 : Node2D
{
	private AudioStreamPlayer2D music;
	public override void _Ready()
	{
		music = GetNode<AudioStreamPlayer2D>("BackgroundMusic");
		music.Playing = true;
	}
	public void _on_background_music_finished()
	{
		music.Playing = true;
	}
}
