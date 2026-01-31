using Godot;

public partial class Enemy : Node, IDamageable
{
	[Export] private EnemyData data;

	[Export] private int currentHealth;

	[Signal] public delegate void HealthChangedEventHandler(int health);

	// David Getter
	public string Name => data.Name;
	public string Description => data.Description;
	public int MaxHealth => data.MaxHealth;
	public int Health => currentHealth;
	public int Damage => data.BaseDamage;

	public override void _Ready()
	{
		currentHealth = data.MaxHealth;
		GD.Print($"{Name} spawned | HP={Health} | DMG={Damage}");
	}
public void TakeDamage(AttackData attack)
	{
		var damage = attack.Damage;

		currentHealth = Mathf.Clamp(currentHealth - attack.Damage, 0, MaxHealth);
		EmitSignal(SignalName.HealthChanged, currentHealth);

		GD.Print($"{Name} hit for {attack.Damage} damage. HP={Health}");
	}

	public AttackData DealDamage()
	{
		return new AttackData
		{
			Damage = currentDamage
		};
	}

	public void HealDamage(int heal)
	{
		currentHealth = Mathf.Clamp(currentHealth + heal, 0, MaxHealth);
		EmitSignal(SignalName.HealthChanged, currentHealth);
	}

	public bool IsDead()
	{
		return currentHealth <= 0;
	}

	public void Die()
	{
		GD.Print($"{Name} died.");
	}
}
