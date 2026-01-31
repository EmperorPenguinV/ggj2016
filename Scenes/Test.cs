using Godot;
using System;

public partial class Test : Node
{
	[Export] private GameLoop gameLoop;

	public override void _Ready()
	{
		gameLoop.StartGame();
	}
}
