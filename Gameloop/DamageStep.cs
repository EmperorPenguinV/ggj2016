using Godot;
using System;

public partial class DamageStep : AGameStep
{
	public override GameSteps Identifier => GameSteps.Damage;

    public override void Enter()
	{
		//Damage player and enemy
		//Check Health
		//Either health reaches 0 -> tell gameloop to go to end
		//Neither health is depleated -> tell gamelop to got to place mask
	}

    public override void Exit()
    {
    }
}
