using CM.Managers;
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
            MovingEntity movingEntity = Object.Instantiate(AssetManager.Instance.GetAsset<GameObject>(removedItem.ToString()))
                .GetComponent<MovingEntity>();

            movingEntity.TeleportToKeystone(gameManager.GetKeystone(key));

            gameManager.AddEntity(movingEntity.gameObject);
        }

        gameManager.RemoveEntity(itemObject, destroyDelay);
    }
}