using Godot;

public partial class Player : Node, IDamageable
{
  [Export] private PlayerData data;

  [Export] private int currentHealth;
  [Export] private int currentArmor;

  [Export] private HealthBar healthBar;

  [Signal] public delegate void HealthChangedEventHandler(int health);

  // David Getter
  public string Name => data.Name;
  public int MaxHealth => data.MaxHealth;
  public int Health => currentHealth;
  public int Armor => currentArmor;

  private int currentDamage;

  private Callable inventoryUpdate;

  public override void _Ready()
  {
    currentHealth = data.MaxHealth;
    healthBar.InitHealth(Health);
    HealthChanged += healthBar.SetHealth;

    GD.Print($"{Name} spawned | HP={Health}");
    inventoryUpdate = Callable.From((int damage, int armor) => OnInventoryUpdated(damage, armor));
  }

  public void ConnectInventory(Node inventoryGd)
  {
    inventoryGd.Connect("mask_placed", inventoryUpdate);
  }

  public void DisconnectInventory(Node inventoryGd)
  {
    inventoryGd.Disconnect("mask_placed", inventoryUpdate);
  }

  public void TakeDamage(AttackData attack)
  {
    var damage = attack.Damage;
    damage -= currentArmor;

    if (damage <= 0)
    {
      GD.Print($"{Name} hit for 0 damage because it was migigated by armor");
      return;
    }

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

  private void OnInventoryUpdated(int damage, int armor)
  {
    currentArmor = armor;
    currentDamage = damage;
  }
}
