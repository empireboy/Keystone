using CM.Events;
using UnityEngine;

public class KeystoneTextShowHide : MonoBehaviour
{
	private GameManager _gameManager;
	private KeystoneManager _keystoneManager;

	private void Awake()
	{
		_gameManager = FindObjectOfType<GameManager>();
		_keystoneManager = FindObjectOfType<KeystoneManager>();

		EventManager.AddListener<TurnStartEvent>(OnTurnStart);
		EventManager.AddListener<TurnEndEvent>(OnTurnEnd);
	}

	private void OnTurnStart(object eventData)
	{
		TurnStartEvent turnStartEvent = eventData as TurnStartEvent;

		// Return if it's not the players turn
		if (turnStartEvent.Turn != TurnManager.TurnStates.PlayerTurn)
			return;

		GameObject player = _gameManager.GetEntity("Player");
		KeyCode playerKey = player.GetComponent<IKeystoneEntity>().Key;
		KeystonePositionsSO playerMovablePositions = player.GetComponent<PlayerInput>().movablePositions;

		// Loop through all keystoneObjects, and check for neighbours
		foreach (KeystoneObject keystoneObject in _keystoneManager.keystoneObjects)
		{
			Keystone[] neighbourKeystones = _gameManager.GetKeystonesByPosition(playerMovablePositions, playerKey);

			// Loop through all neighbours from each specific keystoneObject
			foreach (Keystone neighbourKeystone in neighbourKeystones)
			{
				if (neighbourKeystone == null)
					continue;

				if (keystoneObject.key == neighbourKeystone.Key)
				{
					if (_gameManager.GetEntity(keystoneObject.key, "Enemy") == null)
					{
						// Show the key that you can move towards
						keystoneObject.GetComponent<KeystoneText>().ShowKey();
					}
				}
			}
		}
	}

	private void OnTurnEnd(object eventData)
	{
		TurnEndEvent turnEndEvent = eventData as TurnEndEvent;

		// Return if it's not the players turn that ended
		if (turnEndEvent.Turn != TurnManager.TurnStates.PlayerTurn)
			return;

		foreach (KeystoneObject keystoneObject in _keystoneManager.keystoneObjects)
		{
			keystoneObject.GetComponent<KeystoneText>().HideKey();
		}
	}
}