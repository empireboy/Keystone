using CM.Events;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	public int damage;

	[SerializeField]
	private MovingEntity _player;

	private GameManager _gameManager;

	private void Awake()
	{
		_gameManager = FindObjectOfType<GameManager>();
	}

	private void Start()
	{
		EventManager.AddListener<KeystonePressedEvent>(OnKeystonePressed);
	}

	private void OnKeystonePressed(object eventData)
	{
		if (_gameManager.GameState != GameManager.GameStates.PlayerTurn)
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

				_gameManager.NextGameState();
			}
		}
	}
}