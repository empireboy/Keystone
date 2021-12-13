using CM.Events;
using System.Collections;
using UnityEngine;

public class AutoStartPlayerTurn : MonoBehaviour
{
	public event FloatEvent OnStartAutomation;

	public float autoNextTurnTime = 3f;

	private TurnManager _turnManager;

	private bool _firstTurn = true;

	private void Awake()
	{
		_turnManager = FindObjectOfType<TurnManager>();

		EventManager.AddListener<TurnStartEvent>(OnTurnStart);
		EventManager.AddListener<TurnEndEvent>(OnTurnEnd);
	}

	private void OnTurnStart(object eventData)
	{
		if (_firstTurn)
		{
			_firstTurn = false;
			return;
		}

		TurnStartEvent turnStartEvent = eventData as TurnStartEvent;

		if (turnStartEvent.Turn != TurnManager.TurnStates.PlayerTurn)
			return;

		StartCoroutine(AutoNextTurn());

		OnStartAutomation?.Invoke(autoNextTurnTime);
	}

	private void OnTurnEnd(object eventData)
	{
		if (_firstTurn)
			return;

		TurnEndEvent turnEndEvent = eventData as TurnEndEvent;

		if (turnEndEvent.Turn != TurnManager.TurnStates.PlayerTurn)
			return;

		StopAllCoroutines();
	}

	private IEnumerator AutoNextTurn()
	{
		yield return new WaitForSeconds(autoNextTurnTime);

		_turnManager.NextTurn();
	}
}