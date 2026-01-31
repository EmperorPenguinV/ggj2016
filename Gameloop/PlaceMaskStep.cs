using Godot;

public partial class PlaceMaskStep : AGameStep
{
	[Export] private Node inventoryGd;

	[Export] private Player player;

	public override GameSteps Identifier => GameSteps.Place;

    public override void Enter(GameLoop gameLoop)
	{
		player.ConnectInventory(inventoryGd);

		//Snap next mask to mouse
		//Move up mask preview
		//Await mask placement and tell gameloop to go to damagestep
	}

    public override void Exit()
	{
		player.DisconnectInventory(inventoryGd);
	}
}
