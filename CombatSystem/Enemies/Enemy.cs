using Godot;

public partial class Enemy : Entity
{
	[Export] private EnemyData data;

	// David Getter
	public string Description => data.Description;
	public int Damage => data.BaseDamage;
}
