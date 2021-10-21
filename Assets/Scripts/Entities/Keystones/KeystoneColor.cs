using CM.Events;
using UnityEngine;

public class KeystoneColor : MonoBehaviour
{
	[SerializeField]
	private Color _defaultColor;

	[SerializeField]
	private Color _selectedColor;

	[SerializeField]
	private Color _targetingColor;

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

		// Loop through all keystoneObjects, and check for neighbours
		foreach (KeystoneObject keystoneObject in _keystoneManager.keystoneObjects)
		{
			Keystone[] neighbourKeystones = _gameManager.GetAllNeighbourKeystones(keystoneObject.key);

			// Loop through all neighbours from each specific keystoneObject
			foreach (Keystone neighbourKeystone in neighbourKeystones)
			{
				if (neighbourKeystone == null)
					continue;

				if (_gameManager.GetEntity("Player").GetComponent<IKeystoneEntity>().Key == neighbourKeystone.Key)
				{
					if (_gameManager.GetEntity(keystoneObject.key, "Enemy") != null)
					{
						// Color the keystone for when you can attack an enemy
						keystoneObject.GetComponent<MeshRenderer>().material.color = _targetingColor;
					}
					else
					{
						// Color the keystone for when you can move to this keystone
						keystoneObject.GetComponent<MeshRenderer>().material.color = _selectedColor;
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
			keystoneObject.GetComponent<MeshRenderer>().material.color = _defaultColor;
		}
	}
}