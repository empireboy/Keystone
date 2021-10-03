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

		MovingEntity player = gameManager.GetPlayer(keystone.Key);

		if (player)
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