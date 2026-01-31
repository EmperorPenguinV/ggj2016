using Godot;
using System;

public partial class EndStep : AGameStep
{
	public override GameSteps Identifier => GameSteps.End;

    public override void Enter(GameLoop gameLoop)
	{
		//Check Health
		//if player has no more health -> you lost
		//if enemy has no more health -> you won
		//Await continue button
		//Go to initializestep
	}

    public override void Exit()
    {
    }
}
