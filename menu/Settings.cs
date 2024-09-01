using Godot;
using System;

public partial class Settings : Node2D
{
    public override void _Ready()
    {
        Visible = false;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_cancel"))
        {
            Visible = !Visible;
        }
    }
    
    private int musicBusIndex = AudioServer.GetBusIndex("Master");

    public void _on_h_slider_value_changed(float value)
    {
        float volumeDb = Mathf.Lerp(-80, 0, value / 100.0f);
        AudioServer.SetBusVolumeDb(musicBusIndex, volumeDb);
        GD.Print($"Slider value: {value}, Bus VolumeDb: {volumeDb}");
    }

    private void _on_mute_button_pressed()
    {
        AudioServer.SetBusMute(musicBusIndex, !AudioServer.IsBusMute(musicBusIndex));
    }
    private void _on_exit_button_pressed()
    {
        GetTree().Quit();
    }
    public override void _Process(double delta)
    {
    }
}
