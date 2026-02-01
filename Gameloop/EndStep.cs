using System.Threading.Tasks;
using Godot;

public partial class EndStep : AGameStep
{
	[Export] private CanvasItem shop;

	[Export] private Player player;

	[Export] private Enemy enemy;

	private Callable shopFinished;

	private TaskCompletionSource taskCompletionSource;

	public override GameSteps Identifier => GameSteps.End;

    public override void _Ready()
	{
		shopFinished = Callable.From(FinishedShopping);
	}

    public async override void Enter(GameLoop gameLoop)
	{
		try
		{
			taskCompletionSource = new TaskCompletionSource();

			//Check Health
			var playerDead = player.IsDead();
			var enemyDead = enemy.IsDead();

			if(enemyDead)
			{
				shop.Connect("shopping_finished", shopFinished);
				shop.Visible = true;

				await taskCompletionSource.Task;

				gameLoop.GoToStep(GameSteps.Initialize);

				return;
			}

			if (playerDead)
			{
				GD.Print($"Player lost");

				await Task.Delay(2000);

				gameLoop.EndGame();

				return;
			}
		}
		catch (System.Exception e)
		{
			GD.Print(e.Message);
		}

		//if player has no more health -> you lost
		//if enemy has no more health -> you won
		//Await continue button
		//Go to initializestep
	}

    public override void Exit()
	{
		shop.Visible = false;
		shop.Disconnect("shopping_finished", shopFinished);

		shop.Call("reset_shop");
	}

    public override void Reset()
    {
    }

	private void FinishedShopping()
	{
		taskCompletionSource.SetResult();
	}
}
