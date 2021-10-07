using CM.Events;
using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public float enemyTurnDelay;
	public MovingEntity[] enemies;

	[SerializeField]
	private PrefabCreator _prefabCreator;

	private GameManager _gameManager;
	private TurnManager _turnManager;

	private void Awake()
	{
		_gameManager = FindObjectOfType<GameManager>();
		_turnManager = FindObjectOfType<TurnManager>();

		EventManager.AddListener<TurnStartEvent>(OnTurnStart);
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
		yield return new WaitForSeconds(enemyTurnDelay);

		foreach (MovingEntity enemy in enemies)
		{
			if (!enemy)
				continue;

			yield return new WaitForSeconds(0.3f);

			enemy.GetComponent<EnemyBehaviour>().Execute();
		}

		_turnManager.NextTurn();
	}

	private void OnTurnStart(object eventData)
	{
		TurnStartEvent turnStartEvent = (TurnStartEvent)eventData;

		// Return if it's not the enemies turn
		if (turnStartEvent.Turn != TurnManager.TurnStates.EnemiesTurn)
			return;

		ExecuteEnemies();
	}
}