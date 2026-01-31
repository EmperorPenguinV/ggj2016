using Godot;
using System;

public partial class Entity : Node, IDamageable
{
	[Export] protected EntityData Data;

	[Export] int currentHealth;

	// David Getter
	public new string Name => Data.Name;
	public int MaxHealth => Data.MaxHealth;
	public int Health => currentHealth;

	public override void _Ready()
	{
		currentHealth = Data.MaxHealth;
		GD.Print($"{Name} spawned | HP={Health}");
	}

	public void SetHealth(int newHealth)
	{

	}

	public void TakeDamage(AttackData attack)
	{
		int damage = attack.Damage;
		int reducedHealth = currentHealth - damage;
		SetHealth(reducedHealth);
		GD.Print($"{Name} took {damage} damage!");
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
