using UnityEngine;

[RequireComponent(typeof(MovingEntity))]
public abstract class EnemyCommand : MonoBehaviour, ICommand
{
	protected GameManager gameManager;
	protected MovingEntity enemy;

	private void Awake()
	{
		gameManager = FindObjectOfType<GameManager>();
		enemy = GetComponent<MovingEntity>();
	}

	public abstract bool Execute();
}