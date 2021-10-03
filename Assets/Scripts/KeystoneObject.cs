using UnityEngine;

public class KeystoneObject : MonoBehaviour
{
	public States State { get; private set; }
	public bool IsPressed { get; private set; }

	public KeyCode key;
	public float moveSpeed = 20f;
	public float pressDistance = 0.2f;
	public float targetPositionThreshold = 0.001f;

	private Vector3 _startPosition;
	private Vector3 _targetPosition;

	private GameManager _gameManager;
	private Keystone _keystone;

	public enum States
	{
		Released,
		Pressed
	}

	public void Press()
	{
		if (State == States.Pressed)
			return;

		_targetPosition = transform.position + Vector3.down * pressDistance;

		IsPressed = true;
		State = States.Pressed;
	}

	public void Release()
	{
		if (State == States.Released)
			return;

		_targetPosition = _startPosition;

		IsPressed = false;
		State = States.Released;
	}

	private void Awake()
	{
		_gameManager = FindObjectOfType<GameManager>();
	}

	private void Start()
	{
		_startPosition = transform.position;
		_targetPosition = transform.position;

		_keystone = _gameManager.GetKeystone(key);

		UpdateKeystonePosition();
	}

	private void Update()
	{
		UpdatePosition();
	}

	private void UpdatePosition()
	{
		bool isAtTargetPosition = Vector3.Distance(_targetPosition, transform.position) <= targetPositionThreshold;

		if (isAtTargetPosition)
		{
			// Snap position to target position if at target position
			if (_targetPosition != transform.position)
				transform.position = _targetPosition;

			return;
		}

		transform.position = Vector3.Lerp(transform.position, _targetPosition, moveSpeed * Time.deltaTime);

		UpdateKeystonePosition();
	}

	private void UpdateKeystonePosition()
	{
		_keystone.Position = transform.position + (Vector3.up * transform.localScale.y / 2);
	}
}