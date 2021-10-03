using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public MovingEntity[] enemies;

	[SerializeField]
	private PrefabCreator _prefabCreator;

	private GameManager _gameManager;

	private void Awake()
	{
		_gameManager = FindObjectOfType<GameManager>();
	}

	public void ExecuteEnemies()
	{
		StartCoroutine(ExecuteEnemiesRoutine());
	}

	public MovingEntity GetEnemy(KeyCode key)
	{
		foreach (MovingEntity enemy in enemies)
		{
			if (enemy.Key == key)
				return enemy;
		}

		return null;
	}

	private IEnumerator ExecuteEnemiesRoutine()
	{
		foreach (MovingEntity enemy in enemies)
		{
			if (!enemy)
				continue;

			yield return new WaitForSeconds(0.3f);

			enemy.GetComponent<EnemyBehaviour>().Execute();
		}

		_gameManager.NextGameState();
	}
}