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

		if (gameManager.GetEntity(keystone.Key, EnemyTag) != null)
			return false;

		GameObject player = gameManager.GetEntity(keystone.Key);

		if (player != null)
		{
			player.GetComponent<IDamageable>().TakeDamage(damage);

			return false;
		}
		else
		{
			enemy.MoveToKeystone(keystone);
		}

		return true;
	}
}