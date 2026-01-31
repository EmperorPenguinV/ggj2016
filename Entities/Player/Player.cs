using Godot;

public partial class Player : Entity
{
  protected PlayerData PlayerData => (PlayerData)Data;

  [Export] private int currentArmor;

  public int Armor => currentArmor;

  private Callable inventoryUpdate;

	public override void _Ready()
	{
		GD.Print($"{Name} spawned | HP={Health}");
		inventoryUpdate = Callable.From((int damage, int armor) => OnInventoryUpdated(damage, armor));
	}

	public void ConnectInventory(Node inventoryGd)
	{
		inventoryGd.Connect("mask_placed", inventoryUpdate);
	}

	public void DisconnectInventory(Node inventoryGd)
	{
		inventoryGd.Disconnect("mask_placed", inventoryUpdate);
	}

  public override void TakeDamage(AttackData attack)
  {
    var damage = attack.Damage;
		damage -= currentArmor;

		if (damage <= 0)
		{
			GD.Print($"{Name} hit for 0 damage because it was migigated by armor");
			return;
		}

    var reducedHealth = Mathf.Clamp(Health - damage, 0, MaxHealth);
    SetHealth(reducedHealth);
  }

  private void OnInventoryUpdated(int damage, int armor)
	{
		currentArmor = armor;
		currentDamage = damage;
	}
}
