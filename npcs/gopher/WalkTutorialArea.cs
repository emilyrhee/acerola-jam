using Godot;
using System;

public partial class WalkTutorialArea : Area2D
{
    private RichTextLabel dialog;
    [Export] Font font;
    
    public override void _Ready()
    {
        dialog = GetNode<RichTextLabel>("Dialog");
    }
    private void ResetTextTo(string text)
    {
        dialog.Clear();
        dialog.PushFont(font);
        dialog.PushFontSize(8);
        dialog.AddText(text);
    }
    public void _on_body_entered(Node2D body)
    {
        if (body is Player) // argument MUST be a Node2D, hence the node type MUST be checked
        {
            ResetTextTo("Use WASD or arrow keys to move.");
        }
    }

    public void _on_body_exited(Node2D body)
    {
        if (body is Player) // argument MUST be a Node2D, hence the node type MUST be checked
        {
            dialog.Clear();
        }
    }
}
