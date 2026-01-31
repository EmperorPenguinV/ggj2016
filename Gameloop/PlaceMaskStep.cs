using System.Threading.Tasks;
using Godot;

public partial class PlaceMaskStep : AGameStep
{
	[Export] private Node inventoryGd;

	[Export] private Player player;

	private TaskCompletionSource taskCompletionSource;

	private MaskLogic maskLogic;

	private Callable inventoryUpdate;

	public override GameSteps Identifier => GameSteps.Place;

	public override void _Ready()
	{
		inventoryUpdate = Callable.From((int _, int _) => OnInventoryUpdated());
		maskLogic = new MaskLogic(inventoryGd);
	}

    public async override void Enter(GameLoop gameLoop)
	{
		try
		{
			taskCompletionSource = new TaskCompletionSource();

			player.ConnectInventory(inventoryGd);
			inventoryGd.Connect("mask_placed", inventoryUpdate);

			//Snap next mask to mouse
			maskLogic.InstantiateMask();

			//Move up mask preview
			//Await mask placement and tell gameloop to go to damagestep
			await taskCompletionSource.Task;

			gameLoop.GoToStep(GameSteps.Damage);
		}
		catch (System.Exception e)
		{
			GD.Print(e.Message);
		}
	}

    public override void Exit()
	{
		player.DisconnectInventory(inventoryGd);
		inventoryGd.Disconnect("mask_placed", inventoryUpdate);
		
		taskCompletionSource = null;
	}

	private void OnInventoryUpdated()
	{
		taskCompletionSource.SetResult();
	}
}
