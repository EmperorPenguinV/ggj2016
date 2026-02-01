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

	public void RollDamage()
	{
		currentDamage = rng.RandiRange(1, EnemyData.BaseDamage);
		damagePreview.Text = $"{currentDamage} Damage";

		GD.Print($"{Name} rolls a {currentDamage} for damage");
	}
}
