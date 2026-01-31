using Godot;

public partial class Enemy : Node
{
	[Export] private EnemyData data;

	[Export] private int currentHealth;

	// David Getter
	public string Name => data.Name;
	public string Description => data.Description;
	public int MaxHealth => data.MaxHealth;
	public int Health => currentHealth;
	public int BaseDamage => data.BaseDamage;
	public int Damage => GetDamage();

	public override void _Ready()
	{
		currentHealth = MaxHealth;
		GD.Print($"Enemy spawned | HP={Health} | DMG={Damage}");
	}

	public int GetDamage()
	{
		return BaseDamage;
	}

	public void TakeDamage(AttackData attack)
	{
		currentHealth -= attack.Damage;

		GD.Print(
						$"{Name} hit for {attack.Damage} damage. HP={Health}"
				);

		CheckDeath();
	}

	public void CheckDeath()
	{
		if (currentHealth <= 0) Die();
	}

	private void Die()
	{
		GD.Print($"{Name} died.");
	}
}
