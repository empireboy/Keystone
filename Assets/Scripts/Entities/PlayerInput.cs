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
		int neighbourCount = 4;

		for (int i = 0; i < neighbourCount; i++)
		{
			Keystone neighbourKeystone = _gameManager.GetNeighbourKeystone(keystonePressedEvent.Key, (GameManager.Direction)i);

			if (neighbourKeystone == null)
				continue;

			if (_player.Key == neighbourKeystone.Key)
			{
				MovingEntity enemy = _gameManager.GetEnemy(keystonePressedEvent.Key);

				if (enemy)
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