using UnityEngine;

public class MovingEntity : MonoBehaviour, IKeystoneEntity
{
	public KeyCode Key
	{
		get
		{
			return _key;
		}
	}

	public float moveSpeed = 20f;
	public float targetPositionThreshold = 0.001f;

	[SerializeField]
	private KeyCode _key;

	protected GameManager gameManager;

	private Keystone _keystone;

	private State _state;

	public enum State
	{
		Idle,
		Moving
	}

	public void MoveToKeystone(Keystone keystone)
	{
		_keystone = keystone;

		_key = keystone.Key;

		_state = State.Moving;
	}

	private void Awake()
	{
		gameManager = FindObjectOfType<GameManager>();
	}

	private void Start()
	{
		_keystone = gameManager.GetKeystone(_key);
	}

	private void Update()
	{
		UpdatePosition();
	}

	private void UpdatePosition()
	{
		switch (_state)
		{
			case State.Idle:

				// Lock to keystone when idle
				if (transform.position != _keystone.Position)
					transform.position = _keystone.Position;

				break;

			case State.Moving:

				bool isAtTargetPosition = Vector3.Distance(_keystone.Position, transform.position) <= targetPositionThreshold;

				if (isAtTargetPosition)
				{
					// Snap position to target position if at target position
					if (_keystone.Position != transform.position)
						transform.position = _keystone.Position;

					_state = State.Idle;
				}

				// Move to a another keystone
				transform.position = Vector3.Lerp(transform.position, _keystone.Position, moveSpeed * Time.deltaTime);

				break;
		}
	}
}