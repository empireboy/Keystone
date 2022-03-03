using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    public int slotIndex;

    [SerializeField]
    private Image _image;

    private KeystoneInventory _inventory;

    private void Awake()
    {
        _inventory = FindObjectOfType<KeystoneInventory>();

        _image.sprite = null;
    }

    private void Start()
    {
        _inventory.OnItemAdded += OnItemAdded;
    }

    private void OnItemAdded(IKeystoneItem item, int slotIndex)
    {
        if (slotIndex != this.slotIndex)
            return;

        _image.sprite = item.Sprite;
    }
}