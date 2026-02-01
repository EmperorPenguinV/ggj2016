using Godot;

public partial class InitializeStep : AGameStep
{
	[Export] private Node inventoryGd;

	[Export] private Enemy enemy;

	[Export] private Player player;

	[Export] private EnemyData[] enemyDatas;

	public int Level {get; private set;}

	public override GameSteps Identifier => GameSteps.Initialize;

	public override void Enter(GameLoop gameLoop)
	{
		//Set UI and other classes to initial state
		if (Level == 0)
		{
			player.Initialize();
		}

		enemy.SetData(enemyDatas[Level]);
		Level++;

		enemy.Initialize();

		//Go to Place mask
		gameLoop.GoToStep(GameSteps.Place);
	}

	public override void Exit()
	{
		if (Level < enemyDatas.Length)
		{
			return;
		}

		Level = enemyDatas.Length - 1;
	}

    public override void Reset()
    {
        Level = 0;
    }
}
