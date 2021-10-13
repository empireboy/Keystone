using UnityEngine;

public class EnemyAttackCommand : EnemyCommand
{
	public GameManager.Direction direction;
	public int damage = 40;
	public bool cycleNextIfAttacked = false;

	[SerializeField]
	private PrefabCreator _prefabCreator;

	public override bool Execute()
	{
		Keystone keystone = gameManager.GetNeighbourKeystone(enemy.Key, direction);

		if (keystone == null)
			return true;

		if (gameManager.GetEntity(keystone.Key, EnemyTag) != null)
			return true;

		GameObject player = gameManager.GetEntity(keystone.Key, "Player");

		if (player)
		{
			player.GetComponent<IDamageable>().TakeDamage(damage);

			return cycleNextIfAttacked;
		}
		else
		{
			_prefabCreator.CreateAt(keystone.Position);
		}

		return true;
	}
}