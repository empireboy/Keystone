using UnityEngine;

public class EntityDestroyer : MonoBehaviour
{
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void DestroySelf()
	{
        _gameManager.RemoveEntity(gameObject);
	}
}