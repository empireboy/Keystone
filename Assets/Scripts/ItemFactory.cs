using UnityEngine;

public class ItemFactory
{
    private GameManager _gameManager;

    public ItemFactory(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public GameObject CreateItem(ItemTypes itemType)
    {
        GameObject itemPrefab = _gameManager.GetItemPrefab(itemType);

        return Object.Instantiate(itemPrefab);
    }
}