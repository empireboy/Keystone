using UnityEngine;

public class EnemyAttackCommand : EnemyCommand
{
	public GameManager.Direction direction;
	public int damage = 40;

	[SerializeField]
	private PrefabCreator _prefabCreator;

	public override bool Execute()
	{
		Keystone keystone = gameManager.GetNeighbourKeystone(enemy.Key, direction);

		if (keystone == null)
			return true;

		if (gameManager.GetEnemy(keystone.Key) != null)
			return true;

		IKeystoneEntity player = gameManager.GetEntity(keystone.Key);

		if (player != null)
		{
			(player as Component).GetComponent<IDamageable>().TakeDamage(damage);

			return false;
		}
		else
		{
			_prefabCreator.CreateAt(keystone.Position);
		}

		return true;
	}
}