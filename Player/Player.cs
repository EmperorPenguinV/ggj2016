using Godot;

public partial class Player : Node
{
  [Export] private PlayerData data;

  [Export] private int currentHealth;
  [Export] private int currentArmor;

  // David Getter
  public string Name => data.Name;
  public int MaxHealth => data.MaxHealth;
  public int Health => currentHealth;
  public int Armor => currentArmor;

  public override void _Ready()
  {
    currentHealth = MaxHealth;
    GD.Print($"Player {Name} spawned | HP={Health}");
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
