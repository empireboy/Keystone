using CM.Events;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	public int damage;

	[SerializeField]
	private MovingEntity _player;

	private GameManager _gameManager;
	private TurnManager _turnManager;

	private void Awake()
	{
		_gameManager = FindObjectOfType<GameManager>();
		_turnManager = FindObjectOfType<TurnManager>();
	}

	private void Start()
	{
		EventManager.AddListener<KeystonePressedEvent>(OnKeystonePressed);
	}

	private void OnKeystonePressed(object eventData)
	{
		if (_turnManager.CurrentTurn != TurnManager.TurnStates.PlayerTurn)
			return;

		KeystonePressedEvent keystonePressedEvent = eventData as KeystonePressedEvent;
		Keystone[] neighbourKeystones = _gameManager.GetAllNeighbourKeystones(keystonePressedEvent.Key);

		// Loop through all neighbours
		foreach (Keystone neighbourKeystone in neighbourKeystones)
		{
			if (neighbourKeystone == null)
				continue;

			if (_player.Key == neighbourKeystone.Key)
			{
				GameObject enemy = _gameManager.GetEntity(keystonePressedEvent.Key, "Enemy");

				if (enemy != null)
				{
					enemy.GetComponent<IDamageable>().TakeDamage(damage);
				}
				else
				{
					_player.MoveToKeystone(_gameManager.GetKeystone(keystonePressedEvent.Key));
				}

				_turnManager.NextTurn();
			}
		}
	}
}