using Godot;

public partial class InputFieldName : LineEdit
{
	[Export] private PlayerData playerData;

	public override void _Ready()
	{
		TextSubmitted += OnTextSubmitted;
		base._Ready();
	}

	void OnTextSubmitted(string text)
	{
		playerData.Name = text;
		GD.Print($"Welcome {text}!");

		GetTree().ChangeSceneToFile("res://Scenes/GameScene.tscn");

	}
}
