using Godot;

[GlobalClass]
public partial class PlayerData : Resource
{
  [Export] public string Name;
  [Export] public int MaxHealth;
}
