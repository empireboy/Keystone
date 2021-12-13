using UnityEngine;

public class EnemyWalkToPlayerCommand : EnemyCommand
{
	public int damage = 10;

	private bool _waitBeforeDamagingPlayer = true;

	public override bool Execute()
	{
		Keystone[] neighbourKeystones = gameManager.GetAllNeighbourKeystones(enemy.Key);

		if (neighbourKeystones.Length <= 0)
			return false;

		Keystone keystone = GetClosestKeystone(neighbourKeystones);

		if (gameManager.GetEntity(keystone.Key, EnemyTag) != null)
			return false;

		GameObject player = gameManager.GetEntity(keystone.Key, "Player");

		if (player != null)
		{
			// Wait before damaging the player, so the player has a chance to attack and step back without taking damage
			if (_waitBeforeDamagingPlayer)
			{
				_waitBeforeDamagingPlayer = false;
				return true;
			}

			player.GetComponent<IDamageable>().TakeDamage(damage);

			return false;
		}
		else
		{
			_waitBeforeDamagingPlayer = true;

			enemy.MoveToKeystone(keystone);
		}

		return false;
	}

	private Keystone GetClosestKeystone(Keystone[] keystones)
	{
		Keystone closestKeystone = null;
		Vector3 playerPosition = gameManager.GetEntity("Player").transform.position;
		float closestRange = -1;

		foreach (Keystone keystone in keystones)
		{
			if (keystone == null)
				continue;

			float distance = Vector3.Distance(playerPosition, keystone.Position);

			if ((closestKeystone == null) || (distance < closestRange))
			{
				closestRange = distance;
				closestKeystone = keystone;
			}
		}

		return closestKeystone;
	}
}