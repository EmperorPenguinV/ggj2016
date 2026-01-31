using Godot;
using System;

public partial class Entity : Node, IDamageable
{
	[Export] private EntityData data;

	[Export] int currentHealth;

	// David Getter
	public new string Name => data.Name;
	public int MaxHealth => data.MaxHealth;
	public int Health => currentHealth;

	public override void _Ready()
	{
		currentHealth = data.MaxHealth;
		GD.Print($"{Name} spawned | HP={Health}");
	}

	public void SetHealth(int newHealth)
	{

	}

	public void TakeDamage(AttackData attack)
	{
		int reducedHealth = currentHealth - attack.Damage;
		SetHealth(reducedHealth);
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
