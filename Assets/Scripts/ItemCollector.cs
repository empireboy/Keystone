using UnityEngine;

public class ItemCollector
{
    public void Collect(GameManager gameManager, KeystoneInventory inventory, ItemEntity item, KeyCode key)
    {
        IKeystoneItem removedItem;

        inventory.Add(item.itemSO, out removedItem);

        if (removedItem != null)
        {
            MovingEntity movingEntity = Object.Instantiate(gameManager.GetItemPrefab(removedItem.Type))
                .GetComponent<MovingEntity>();

            movingEntity.TeleportToKeystone(gameManager.GetKeystone(key));

            gameManager.AddEntity(movingEntity.gameObject);
        }

        gameManager.RemoveEntity(item.gameObject, item.destroyDelay);
    }
}