using Godot;

public partial class InitializeStep : AGameStep
{
	[Export] private Node inventoryGd;

	[Export] private Enemy enemy;

	[Export] private Player player;

	[Export] private EnemyData[] enemyDatas;

	private int level;

	public override GameSteps Identifier => GameSteps.Initialize;

	public override void Enter(GameLoop gameLoop)
	{
		//Set UI and other classes to initial state
		enemy.SetData(enemyDatas[level]);
		level++;

		enemy.Initialize();

		if (level == 0)
		{
			player.Initialize();
		}

		//Go to Place mask
		gameLoop.GoToStep(GameSteps.Place);
	}

	public override void Exit()
	{
		if (level < enemyDatas.Length)
		{
			return;
		}

		level = enemyDatas.Length - 1;
	}

    public override void Reset()
    {
        level = 0;
    }
}
