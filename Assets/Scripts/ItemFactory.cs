using CM.Managers;
using UnityEngine;

public class ItemFactory
{
    public GameObject CreateItem(ItemTypes itemType)
    {
        return Object.Instantiate(AssetManager.Instance.GetAsset<GameObject>(itemType.ToString()));
    }
}