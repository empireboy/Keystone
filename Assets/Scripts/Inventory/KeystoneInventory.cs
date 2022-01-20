using UnityEngine;

public class KeystoneInventory : MonoBehaviour
{
	public int slotCount;

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

	public bool Add(IKeystoneItem item)
	{
		bool itemAdded = _inventory.Add(item, item.GroupIndexes);

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
}