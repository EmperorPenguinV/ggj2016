using Godot;

public partial class Player : Node, IDamageable
{
  [Export] private PlayerData data;

  [Export] private int currentHealth;
  [Export] private int currentArmor;

  // David Getter
  public string Name => data.Name;
  public int MaxHealth => data.MaxHealth;
  public int Health => currentHealth;
  public int Armor => currentArmor;

  // TODO: Get damage value from inventory

  public override void _Ready()
  {
    currentHealth = data.MaxHealth;
    GD.Print($"{Name} spawned | HP={Health}");
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
