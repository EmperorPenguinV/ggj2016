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

	public void SetData(EnemyData enemyData)
	{
		Data = enemyData;
	}

	public override AttackData DealDamage()
	{
		int damage = RollDamage(EnemyData.BaseDamage);
		GD.Print($"{Name} rolls a {damage} for damage");
		currentDamage = damage;

		return base.DealDamage();
	}

	public int RollDamage(int max, int min = 1)
	{
		var damage = rng.RandiRange(min, max);
		damagePreview.Text = $"{damage} Damage";
		return damage;
	}
}
