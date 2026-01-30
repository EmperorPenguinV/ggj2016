using Godot;

public partial class AttackButton : Button
{
	[Export] public AttackData AttackData;
	[Export] public NodePath EnemyPath;

	private Enemy _enemy;

	public override void _Ready()
	{
		_enemy = GetNode<Enemy>(EnemyPath);
		Pressed += OnPressed;
	}

	private void OnPressed()
	{
		_enemy.TakeDamage(AttackData);
	}
}