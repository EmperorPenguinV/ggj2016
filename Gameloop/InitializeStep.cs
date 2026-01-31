using Godot;
using System;
using System.Threading.Tasks;

public partial class InitializeStep : AGameStep
{
	[Export] private Node inventoryGd;

	private int itemsPlaced;

	private TaskCompletionSource taskCompletionSource;

	private Callable itemUpdate;

	public override GameSteps Identifier => GameSteps.Initialize;

	public async override void Enter(GameLoop gameLoop)
	{
		//Set UI and other classes to initial state

		itemUpdate = Callable.From(OnItemPlaced);
		inventoryGd.Connect("item_placed", itemUpdate);

		while (itemsPlaced < 1)
		{
			taskCompletionSource = new TaskCompletionSource();

			PlaceItem();
			await taskCompletionSource.Task;
			itemsPlaced++;
		}

		//Go to Place mask
		gameLoop.GoToStep(GameSteps.Place);
	}

    public override void Exit()
	{
		inventoryGd.Disconnect("item_placed", itemUpdate);
	}

	private void PlaceItem()
	{
		inventoryGd.Call("_on_button_spawn_pressed");
	}

	private void OnItemPlaced()
	{
		taskCompletionSource.SetResult();
	}
}
