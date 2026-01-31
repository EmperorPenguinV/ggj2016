using Godot;

public partial class AttackButton : Button
{
	[Export] public AttackData AttackData;
	[Export] public NodePath Damagable;

	private IDamageable _damagable;

	public override void _Ready()
	{
		_damagable = GetNode<IDamageable>(Damagable);
		Pressed += OnPressed;
	}

	private void OnPressed()
	{
		_damagable.TakeDamage(AttackData);
	}
}