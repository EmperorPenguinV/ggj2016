using Godot;

public partial class DamageStep : AGameStep
{
	[Export] private Player player;

	[Export] private Enemy enemy;

	public override GameSteps Identifier => GameSteps.Damage;

    public override void Enter(GameLoop gameLoop)
	{
		//Damage player and enemy
		var playerDamage = player.DealDamage();
		enemy.TakeDamage(playerDamage);

		var enemyDamage = enemy.DealDamage();
		player.TakeDamage(playerDamage);

		//Check Health
		var playerDead = player.IsDead();
		var enemyDead = enemy.IsDead();

		//Neither health is depleated -> tell gameloop to got to place mask
		if (!playerDead && !enemyDead)
		{
			gameLoop.GoToStep(GameSteps.Place);
			return;
		}

		if (playerDead)
		{
			player.Die();
		}
		else
		{
			enemy.Die();
		}

		//Either health reaches 0 -> tell gameloop to go to end
		gameLoop.GoToStep(GameSteps.End);
	}

    public override void Exit()
    {
    }
}
