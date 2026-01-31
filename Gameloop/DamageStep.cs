using Godot;
using System;

public partial class DamageStep : AGameStep
{
	[Export] private Player player;

	[Export] private Enemy enemy;

	public override GameSteps Identifier => GameSteps.Damage;

    public override void Enter()
	{
		//Damage player and enemy
		var playerDamage = player.DealDamage();
		enemy.TakeDamage(playerDamage);

		var enemyDamage = enemy.DealDamage();
		//Check Health
		//Either health reaches 0 -> tell gameloop to go to end
		//Neither health is depleated -> tell gamelop to got to place mask
	}

    public override void Exit()
    {
    }
}
