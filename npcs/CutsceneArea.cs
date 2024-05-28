using Godot;
using System;

public partial class CutsceneArea : Area2D
{
    private RichTextLabel dialog;
    private bool playerIsNearby = false;
    [Export] Font font;
	public override void _Ready()
	{
        dialog = GetNode<RichTextLabel>("Dialog");
        dialog.PushFont(font);
        dialog.PushFontSize(11);
	}
    private void ResetTextTo(string text)
    {
        dialog.Clear();
        dialog.PushFont(font);
        dialog.PushFontSize(11);
        dialog.AddText(text);
    }
    public void _on_body_entered(Node2D body)
    {
        if (body.Name == "Player")
        {
            dialog.AddText("Hello! ▶");
            playerIsNearby = true;
        }
    }
    public void _on_body_exited(Node2D body)
    {
        playerIsNearby = false;
        Player.jumpVelocity = -250.0f;
    }
    public override void _Input(InputEvent @event)
    {
        if (playerIsNearby && @event is InputEventKey eventKey && eventKey.Pressed)
        {
            if (Input.IsActionJustPressed("ui_accept"))
            {
                Player.jumpVelocity = 0;
                ResetTextTo("How are you?");
            }
        }
    }
}
