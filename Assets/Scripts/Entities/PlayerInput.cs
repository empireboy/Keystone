using CM.Events;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	public KeystonePositionsSO movablePositions;
	public int damage;
	public float destroyItemDelay;

	[SerializeField]
	private MovingEntity _player;

	[SerializeField]
	private KeystoneInventory _inventory;

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

	private void KeystonePressedBehaviour(GameObject entity, KeystonePressedEvent keystonePressedEvent)
	{
		// Move to keystone if there is no entity on it
		if (!entity)
		{
			_player.MoveToKeystone(_gameManager.GetKeystone(keystonePressedEvent.Key));
			return;
		}

		switch (entity.tag)
		{
			case "Enemy":

				entity.GetComponent<IDamageable>().TakeDamage(damage);

				break;

			case "Item":

				_player.MoveToKeystone(_gameManager.GetKeystone(keystonePressedEvent.Key));

				ItemEntity itemEntity = entity.GetComponent<ItemEntity>();
				new ItemCollector().Collect(_gameManager, _inventory, itemEntity, keystonePressedEvent.Key);

				break;
		}
	}

	private void OnKeystonePressed(object eventData)
	{
		if (_turnManager.CurrentTurn != TurnManager.TurnStates.PlayerTurn)
			return;

		KeystonePressedEvent keystonePressedEvent = eventData as KeystonePressedEvent;
		Keystone[] neighbourKeystones = _gameManager.GetKeystonesByPosition(movablePositions, _player.Key);

		// Loop through all neighbours
		foreach (Keystone neighbourKeystone in neighbourKeystones)
		{
			if (neighbourKeystone == null)
				continue;

			if (keystonePressedEvent.Key != neighbourKeystone.Key)
				continue;

			GameObject entity = _gameManager.GetEntity(keystonePressedEvent.Key);

			KeystonePressedBehaviour(entity, keystonePressedEvent);

			_turnManager.NextTurn();
		}
	}
}