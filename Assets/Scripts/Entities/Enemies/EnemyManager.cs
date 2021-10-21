using CM.Events;
using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public float enemyTurnDelay;
	public float delayBetweenEnemies = 0.3f;

	private GameManager _gameManager;
	private TurnManager _turnManager;
	private GameObject[] _enemies => _gameManager.GetEntities("Enemy");

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

	public GameObject GetEnemy(KeyCode key)
	{
		foreach (GameObject enemy in _enemies)
		{
			if (enemy.GetComponent<IKeystoneEntity>().Key == key)
				return enemy;
		}

		return null;
	}

	private IEnumerator ExecuteEnemiesRoutine()
	{
		yield return new WaitForSeconds(enemyTurnDelay);

		foreach (GameObject enemy in _enemies)
		{
			if (!enemy)
				continue;

			enemy.GetComponent<EnemyBehaviour>().Execute();

			yield return new WaitForSeconds(delayBetweenEnemies);
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