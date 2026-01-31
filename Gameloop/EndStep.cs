using Godot;

public partial class EndStep : AGameStep
{
	[Export] private Player player;

	[Export] private Enemy enemy;

	public override GameSteps Identifier => GameSteps.End;

    public override void Enter(GameLoop gameLoop)
	{
		//Check Health
		var playerDead = player.IsDead();
		var enemyDead = enemy.IsDead();

		if (playerDead && enemyDead)
		{
			GD.Print($"Draw");
			return;
		}

		if (playerDead)
		{
			GD.Print($"Player lost");
			return;
		}
		
		if(enemyDead)
		{
			GD.Print($"Player won");
			return;
		}

		//if player has no more health -> you lost
		//if enemy has no more health -> you won
		//Await continue button
		//Go to initializestep
	}

    public override void Exit()
    {
    }
}
