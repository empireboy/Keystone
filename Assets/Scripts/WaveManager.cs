using CM.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    private GameManager _gameManager;

    private int _enemyCount = 0;
    private int _wave = 1;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();

        _gameManager.OnEntityRemoved += OnEntityRemoved;
    }

    private void Start()
    {
        LoadWave(_wave);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    private void LoadWave(int wave)
    {
        switch (wave)
        {
            case 1:

                CreateEnemy(EnemyTypes.SlimeSmall);

                break;

            case 2:

                CreateEnemy(EnemyTypes.SlimeSmall);
                CreateEnemy(EnemyTypes.TurtleShell);

                break;

            case 3:

                CreateEnemy(EnemyTypes.TurtleShell);
                CreateEnemy(EnemyTypes.TurtleShell);
                CreateEnemy(EnemyTypes.TurtleShell);

                break;

            case 4:

                CreateEnemy(EnemyTypes.Slime);
                CreateItem(ItemTypes.TestItem2);

                break;

            case 5:

                CreateEnemy(EnemyTypes.SlimeSmall);
                CreateEnemy(EnemyTypes.Slime);
                CreateEnemy(EnemyTypes.Slime);

                break;

            case 6:

                CreateEnemy(EnemyTypes.SlimeSmall);
                CreateEnemy(EnemyTypes.SlimeSmall);
                CreateEnemy(EnemyTypes.SlimeSmall);
                CreateEnemy(EnemyTypes.SlimeSmall);
                CreateEnemy(EnemyTypes.SlimeSmall);
                CreateEnemy(EnemyTypes.SlimeSmall);
                CreateEnemy(EnemyTypes.SlimeSmall);
                CreateEnemy(EnemyTypes.SlimeSmall);
                CreateItem(ItemTypes.TestItem3);

                break;

            case 7:

                CreateEnemy(EnemyTypes.SlimeBig);
                CreateEnemy(EnemyTypes.TurtleShell);
                CreateEnemy(EnemyTypes.TurtleShell);
                CreateEnemy(EnemyTypes.TurtleShell);

                break;

            default:

                CreateEnemy(EnemyTypes.Slime);
                CreateEnemy(EnemyTypes.TurtleShell);
                CreateEnemy(EnemyTypes.TurtleShell);
                CreateEnemy(EnemyTypes.TurtleShell);
                CreateEnemy(EnemyTypes.SlimeSmall);
                CreateEnemy(EnemyTypes.SlimeSmall);
                CreateEnemy(EnemyTypes.SlimeSmall);

                break;
        }
    }

    private void CreateEnemy(EnemyTypes enemyType)
    {
        MovingEntity movingEntity = Instantiate(AssetManager.Instance.GetAsset<GameObject>(enemyType.ToString()))
                .GetComponent<MovingEntity>();

        KeyCode key;

        do
        {
            key = (KeyCode)Random.Range((int)KeyCode.A, (int)KeyCode.Z + 1);
        }
        while (_gameManager.GetEntity(key) != null);

        movingEntity.TeleportToKeystone(_gameManager.GetKeystone(key));

        _gameManager.AddEntity(movingEntity.gameObject);

        _enemyCount++;
    }

    private void CreateItem(ItemTypes itemType)
    {
        MovingEntity movingEntity = Instantiate(AssetManager.Instance.GetAsset<GameObject>(itemType.ToString()))
                .GetComponent<MovingEntity>();

        KeyCode key;

        do
        {
            key = (KeyCode)Random.Range((int)KeyCode.A, (int)KeyCode.Z + 1);
        }
        while (_gameManager.GetEntity(key) != null);

        movingEntity.TeleportToKeystone(_gameManager.GetKeystone(key));

        _gameManager.AddEntity(movingEntity.gameObject);
    }

    private void OnEntityRemoved(GameObject entity)
    {
        if (!entity.CompareTag("Enemy"))
            return;

        _enemyCount--;

        if (_enemyCount <= 0)
        {
            _wave++;
            LoadWave(_wave);
        }
    }
}