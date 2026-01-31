using Godot;

public partial class HealthBar : ProgressBar
{
	Timer timer;
	ProgressBar damageBar;

	int health = 0;

	int Health { get { return health; } set { SetHealth(value); } }



	void InitHealth(int initialHealth)
	{
		health = initialHealth;
		MaxValue = initialHealth;
		Value = initialHealth;
		damageBar.MaxValue = initialHealth;
		damageBar.Value = initialHealth;
	}

	void SetHealth(int newHealth)
	{
		int previousHealth = health;
		health = (int)Mathf.Min(MaxValue, newHealth);
		health = newHealth;
		Value = health;

		if (health <= 0)
			QueueFree();

		// taking damage
		if (health < previousHealth)
		{
			timer.Start();
		}
		// health increase
		else
		{
			damageBar.Value = health;
		}
	}


}
