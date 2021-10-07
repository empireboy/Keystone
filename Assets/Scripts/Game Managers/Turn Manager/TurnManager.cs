using CM.Events;
using System;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
	public TurnStates CurrentTurn { get; private set; }
	public int TurnStatesLength
	{
		get
		{
			return Enum.GetNames(typeof(TurnStates)).Length;
		}
	}

	public enum TurnStates
	{
		PlayerTurn,
		EnemiesTurn
	}

	public void NextTurn()
	{
		EndTurn();

		CurrentTurn++;

		// Start from the first game state when the final game state ends
		if ((int)CurrentTurn >= TurnStatesLength)
			CurrentTurn = 0;

		StartTurn();
	}

	public void PreviousTurn()
	{
		EndTurn();

		CurrentTurn--;

		if ((int)CurrentTurn < 0)
			CurrentTurn = (TurnStates)TurnStatesLength - 1;

		StartTurn();
	}

	private void StartTurn()
	{
		EventManager.Trigger(new TurnStartEvent(CurrentTurn));
	}

	private void EndTurn()
	{
		EventManager.Trigger(new TurnEndEvent(CurrentTurn));
	}

	/*private IEnumerator NextTurnRoutine()
	{
		CurrentTurn++;

		// Start from the first game state when the final game state ends
		if ((int)CurrentTurn >= Enum.GetNames(typeof(TurnStates)).Length)
			CurrentTurn = 0;

		if (CurrentTurn == TurnStates.EnemiesTurn)
			yield return new WaitForSeconds(gameStateTransitionTime);

		if (CurrentTurn == TurnStates.EnemiesTurn)
			_enemyManager.ExecuteEnemies();
	}*/
}