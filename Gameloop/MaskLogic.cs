using Godot;

public partial class MaskLogic
{
	private readonly Node inventoryGd;

    public MaskLogic(Node inventoryGd)
    {
        this.inventoryGd = inventoryGd;
    }

    public void InstantiateMask()
	{
		inventoryGd.Call("_on_get_mask");
	}
}
