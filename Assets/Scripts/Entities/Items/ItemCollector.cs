using UnityEngine;

public class ItemCollector
{
    public static readonly float destroyDelay = 0.1f;

    public void Collect(GameManager gameManager, KeystoneInventory inventory, IKeystoneItem item, GameObject itemObject, KeyCode key)
    {
        IKeystoneItem removedItem;

        inventory.Add(item, out removedItem);

        if (removedItem != null)
        {
            MovingEntity movingEntity = new EntityFactory(Object.FindObjectOfType<ObjectPoolManager>())
                .Create(removedItem.Type)
                .GetComponent<MovingEntity>();

            movingEntity.TeleportToKeystone(gameManager.GetKeystone(key));

            gameManager.AddEntity(movingEntity.gameObject);
        }

        gameManager.RemoveEntity(itemObject, destroyDelay);
    }
}