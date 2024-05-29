using Godot;
using System;
using System.Threading.Tasks;

public partial class CutsceneArea : Area2D
{
    private RichTextLabel dialog;
    private bool playerIsNearby = false;
    [Export] Font font;
    private int dialogState = 0;
    
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

            Player.speed = 0f;

            playerIsNearby = true;
        }
    }
    private async Task Sleep(int milliseconds)
    {
        await Task.Delay(milliseconds);
    }
    public override async void _Input(InputEvent @event)
    {
        if (playerIsNearby && @event is InputEventKey eventKey && eventKey.Pressed && Input.IsActionJustPressed("ui_accept"))
        {
            switch (dialogState)
            {
                case 0:
                    await Sleep(1000);
                    ResetTextTo("Me too. That meteor storm sure did a number on us.");
                    dialogState++;
                    break;
                case 1:
                    Player.jumpVelocity = 0;
                    await Sleep(500);
                    ResetTextTo("It's been hard gathering resources.");
                    dialogState++;
                    break;
                // Add more cases for additional dialog lines
                default:
                    // Optionally handle the end of the dialog sequence
                    break;
            }
        }
    }
}
