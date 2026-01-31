using Godot;
using System;

public partial class Entity : Node, IDamageable
{
	[Export] protected EntityData Data;

	[Export] int currentHealth;

	[Signal] public delegate void HealthChangedEventHandler(int health);

	// David Getter
	public new string Name => Data.Name;
	public int MaxHealth => Data.MaxHealth;
	public int Health => currentHealth;

	protected int currentDamage;

	public override void _Ready()
	{
		currentHealth = Data.MaxHealth;
		GD.Print($"{Name} spawned | HP={Health}");
	}

	public void SetHealth(int newHealth)
	{
		currentHealth = newHealth;
		EmitSignal(SignalName.HealthChanged, currentHealth);
	}

	public virtual void TakeDamage(AttackData attack)
	{
		var reducedHealth = Mathf.Clamp(currentHealth - attack.Damage, 0, MaxHealth);
		SetHealth(reducedHealth);
	}

	public virtual AttackData DealDamage()
	{
		return new AttackData
		{
			Damage = currentDamage
		};
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
