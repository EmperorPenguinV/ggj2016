using Godot;

public partial class Entity : Node, IDamageable
{
	[Export] protected EntityData Data;
	[Export] protected HealthBar healthBar;

	[Export] protected RichTextLabel nameTag;

	[Export] protected int currentHealth;

	[Signal] public delegate void HealthChangedEventHandler(int health);

	// David Getter
	public new string Name => Data.Name;
	public int MaxHealth => Data.MaxHealth;
	public int Health => currentHealth;

	protected int currentDamage;

	public override void _Ready()
	{
		nameTag.Text = Name;
		currentHealth = Data.MaxHealth;
		healthBar.InitHealth(Health);
		HealthChanged += healthBar.SetHealth;
		GD.Print($"{Name} spawned | HP={Health}");
	}

	public void SetHealth(int newHealth)
	{
		currentHealth = newHealth;
		EmitSignal(SignalName.HealthChanged, currentHealth);
	}

	public virtual void TakeDamage(AttackData attack)
	{
		var damage = attack.Damage;
		GD.Print($"{Name} took {damage} damage!");

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
