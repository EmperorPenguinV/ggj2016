using Godot;

public partial class Player : Entity
{
  protected PlayerData PlayerData => (PlayerData)Data;

  [Export] private int currentArmor;

  public int Armor => currentArmor;

  // TODO: Get damage value from inventory
}
