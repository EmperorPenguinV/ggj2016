using Godot;
using System;

public partial class InitializeStep : AGameStep
{
	public override GameSteps Identifier => GameSteps.Initialize;

	public override void Enter(GameLoop gameLoop)
	{
		//Set UI and other classes to initial state
		//Go to Place mask
	}

    public override void Exit()
    {
    }
}
