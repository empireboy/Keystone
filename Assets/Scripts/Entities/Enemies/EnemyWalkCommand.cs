using UnityEngine;

public class EnemyWalkCommand : EnemyCommand
{
	public GameManager.Direction direction;
	public int damage = 10;

	public override bool Execute()
	{
		Keystone keystone = gameManager.GetNeighbourKeystone(enemy.Key, direction);

		if (keystone == null)
			return false;

		if (gameManager.GetEnemy(keystone.Key) != null)
			return false;

		IKeystoneEntity player = gameManager.GetEntity(keystone.Key);

		if (player != null)
		{
			(player as Component).GetComponent<IDamageable>().TakeDamage(damage);

			return false;
		}
		else
		{
			enemy.MoveToKeystone(keystone);
		}

		return true;
	}
}