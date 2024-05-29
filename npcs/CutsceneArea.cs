using Godot;
using System;
using System.Threading.Tasks;

public partial class CutsceneArea : Area2D
{
    private RichTextLabel dialog;
    private bool playerIsNearby = false;
    [Export] Font font;
	public override void _Ready()
	{
        dialog = GetNode<RichTextLabel>("Dialog");
        dialog.PushFont(font);
        dialog.PushFontSize(9);
	}
    private void ResetTextTo(string text)
    {
        dialog.Clear();
        dialog.PushFont(font);
        dialog.PushFontSize(9);
        dialog.AddText(text);
    }
    public void _on_body_entered(CharacterBody2D body)
    {
        if (body.Name == "Player")
        {
            dialog.AddText("Hello! How are you? ▶");

            playerIsNearby = true;
        }
    }
    public void _on_body_exited(Node2D body)
    {
        playerIsNearby = false;
        Player.jumpVelocity = -250.0f;
    }
    private async Task Sleep(int milliseconds)
    {
        await Task.Delay(milliseconds);
    }
    public override async void _Input(InputEvent @event)
    {
        if (playerIsNearby && @event is InputEventKey eventKey && eventKey.Pressed)
        {
            if (Input.IsActionJustPressed("ui_accept"))
            {
                await Sleep(1000);
                ResetTextTo("Me too. That meteor storm sure did a number on us.");
            }
        }
    }
}
