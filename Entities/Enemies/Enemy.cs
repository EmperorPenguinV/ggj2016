using Godot;

public partial class Enemy : Entity
{
	protected EnemyData EnemyData => (EnemyData)Data;

	// David Getter
	public string Description => EnemyData.Description;

	public override AttackData DealDamage()
	{
		//ToDo: Roll enemy damage
		currentDamage = EnemyData.BaseDamage;

		return base.DealDamage();
	}
}
