using Godot;
using System;

public partial class InitializeStep : AGameStep
{
	public override GameSteps Identifier => GameSteps.Initialize;

    public override void Enter()
	{
		//Set UI and other classes to initial state
		//Go to Place mask
	}

    public override void Exit()
    {
    }

}
