using Godot;

public partial class HealthBar : ProgressBar
{
	[Export] private Timer timer;
	[Export] private ProgressBar damageBar;

	int health = 0;

	int Health { get { return health; } set { SetHealth(value); } }

	public override void _Ready()
	{
		timer.Timeout += OnTimerTimeout;
	}

	public void InitHealth(int initialHealth)
	{
		health = initialHealth;
		MaxValue = initialHealth;
		Value = initialHealth;
		damageBar.MaxValue = initialHealth;
		damageBar.Value = initialHealth;
	}

	public void SetHealth(int newHealth)
	{
		int previousHealth = health;
		health = (int)Mathf.Min(MaxValue, newHealth);
		health = newHealth;
		Value = health;

		// takes damage, delays update of damage bar to visualize dealt damage
		if (health < previousHealth)
		{
			timer.Start();
		}
		// instantly updates damage bar on health increase
		else
		{
			damageBar.Value = health;
		}
	}

	void OnTimerTimeout()
	{
		damageBar.Value = health;
	}
}
