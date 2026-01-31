using Godot;
using System;

public partial class PlaceMaskStep : AGameStep
{
	public override GameSteps Identifier => GameSteps.Place;

    public override void Enter()
	{
		//Snap next mask to mouse
		//Move up mask preview
		//Await mask placement and tell gameloop to go to damagestep
	}

    public override void Exit()
    {
    }
}
