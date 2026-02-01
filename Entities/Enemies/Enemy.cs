using Godot;

public partial class Enemy : Entity
{
	protected EnemyData EnemyData => (EnemyData)Data;

	protected RandomNumberGenerator rng = new();

	// David Getter
	public string Description => EnemyData.Description;

	public override void _Ready()
	{
		base._Ready();
		rng.Randomize();
	}

	public override AttackData DealDamage()
	{
		int damage = RollDamage(EnemyData.BaseDamage);
		currentDamage = damage;

		return base.DealDamage();
	}

	public int RollDamage(int max, int min = 1)
	{
		return rng.RandiRange(min, max);
	}
}
