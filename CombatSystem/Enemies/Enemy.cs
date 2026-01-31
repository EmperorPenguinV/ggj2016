using Godot;

public partial class Enemy : Node, IDamageable
{
	[Export] private EnemyData data;

	[Export] private int currentHealth;

	// David Getter
	public string Name => data.Name;
	public string Description => data.Description;
	public int MaxHealth => data.MaxHealth;
	public int Health => currentHealth;
	public int Damage => data.BaseDamage;

	public override void _Ready()
	{
		currentHealth = MaxHealth;
		GD.Print($"Enemy spawned | HP={Health} | DMG={Damage}");
	}

	public void TakeDamage(AttackData attack)
	{
		currentHealth -= attack.Damage;

		GD.Print(
						$"{Name} hit for {attack.Damage} damage. HP={Health}"
				);

		if (IsDead()) Die();
	}

	public bool IsDead()
	{
		return currentHealth <= 0;
	}

	private void Die()
	{
		GD.Print($"{Name} died.");
	}
}
