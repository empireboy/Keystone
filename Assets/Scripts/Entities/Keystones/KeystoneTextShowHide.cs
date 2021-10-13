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

		int neighbourCount = 4;

		foreach (KeystoneObject keystoneObject in _keystoneManager.keystoneObjects)
		{
			for (int i = 0; i < neighbourCount; i++)
			{
				Keystone neighbourKeystone = _gameManager.GetNeighbourKeystone(keystoneObject.key, (GameManager.Direction)i);

				if (neighbourKeystone == null)
					continue;

				if (_gameManager.GetEntity("Player").GetComponent<IKeystoneEntity>().Key == neighbourKeystone.Key)
				{
					keystoneObject.GetComponent<KeystoneText>().ShowKey();
					keystoneObject.GetComponent<MeshRenderer>().material.color = new Color(0.5f, 1, 1);
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
			keystoneObject.GetComponent<MeshRenderer>().material.color = Color.white;
		}
	}
}