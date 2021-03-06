using System.Collections;
using UnityEngine;

public class KeystoneInventory : MonoBehaviour
{
	public int slotCount;

	public ShoeItemSO defaultBoots;

	public event ItemEvent<IKeystoneItem> OnItemAdded
	{
		add => _inventory.OnItemAdded += value;
		remove => _inventory.OnItemAdded -= value;
	}

	public event ItemEvent<IKeystoneItem> OnItemRemoved
	{
		add => _inventory.OnItemRemoved += value;
		remove => _inventory.OnItemRemoved -= value;
	}

	private PresetInventory<IKeystoneItem> _inventory;

	public bool Add(IKeystoneItem item, out IKeystoneItem removedItem)
	{
		bool itemAdded = _inventory.Add(item, out removedItem, item.GroupIndexes);

		if (itemAdded && item.AutoUse)
			item.Use();

		return itemAdded;
	}

	public void Remove(IKeystoneItem item)
	{
		_inventory.Remove(item);
	}

	private void Awake()
	{
		_inventory = new PresetInventory<IKeystoneItem>(slotCount);

		_inventory.SetItemIndexesAllowed(0, new int[] { 0 });
		_inventory.SetSwapIfFull(0, true);
	}

    private void Start()
    {
		StartCoroutine(AddFirstItemsRoutine());
    }

    private IEnumerator AddFirstItemsRoutine()
    {
		yield return new WaitForEndOfFrame();

		_inventory.Add(defaultBoots, out IKeystoneItem removedItem, defaultBoots.GroupIndexes);
	}
}