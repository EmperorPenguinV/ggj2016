using Godot;

public partial class Enemy : Entity
{
	protected EnemyData EnemyData => (EnemyData)Data;

	// David Getter
	public string Description => EnemyData.Description;
	public int Damage => EnemyData.BaseDamage;
}
