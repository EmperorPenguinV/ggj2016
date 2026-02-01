using System.Threading.Tasks;
using Godot;

public partial class EndStep : AGameStep
{
	[Export] private CanvasItem shop;

	[Export] private Player player;

	[Export] private Enemy enemy;

	[Export] private CanvasItem endPanel;

	[Export] private Button reset;

	[Export] private Button enterName;

	[Export] private Button close;

	private Callable shopFinished;

	private GameLoop loop;

	private TaskCompletionSource shopCompletionSource;

	public override GameSteps Identifier => GameSteps.End;

    public override void _Ready()
	{
		shopFinished = Callable.From(FinishedShopping);
		endPanel.Visible = false;

		close.Pressed += Close;
		enterName.Pressed += BackToName;
		reset.Pressed += Restart;
	}

    public async override void Enter(GameLoop gameLoop)
	{
		try
		{
			loop ??= gameLoop;

			shopCompletionSource = new TaskCompletionSource();

			//Check Health
			var playerDead = player.IsDead();
			var enemyDead = enemy.IsDead();

			if(enemyDead)
			{
				shop.Connect("shopping_finished", shopFinished);
				shop.Visible = true;

				await shopCompletionSource.Task;

				gameLoop.GoToStep(GameSteps.Initialize);

				return;
			}

			if (playerDead)
			{
				GD.Print($"Player lost");

				await Task.Delay(1000);

				endPanel.Visible = true;

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
		endPanel.Visible = false;
		Exit();
	}

	private void FinishedShopping()
	{
		shopCompletionSource.SetResult();
	}

	private void Restart()
	{
		loop.EndGame();
		loop.StartGame();
	}

	private void Close()
	{
		GetTree().Quit();
	}

	private void BackToName()
	{
		GetTree().ChangeSceneToFile("res://Scenes/menu_scene.tscn");
	}
}
