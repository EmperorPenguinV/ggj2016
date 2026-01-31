using Godot;

public partial class Player : Entity
{
  [Export] private PlayerData data;

  [Export] private int currentArmor;

  public int Armor => currentArmor;

  // TODO: Get damage value from inventory
}
