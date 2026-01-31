using Godot;

[GlobalClass]
public partial class EnemyData : Resource
{
	[Export] public string Name;
	[Export] public string Description;
	[Export] public int MaxHealth;
	[Export] public int BaseDamage;
}
