public class PresetInventory<T> : IInventory<T> where T : class
{
	public event ItemEvent<T> OnItemAdded;
	public event ItemEvent<T> OnItemRemoved;

	protected ItemSlot<T>[] itemSlots;

	public PresetInventory(int size)
	{
		itemSlots = new ItemSlot<T>[size];

		// Initialize item slots
		for (int i = 0; i < itemSlots.Length; i++)
			itemSlots[i] = new ItemSlot<T>();
	}

	public virtual bool Add(T item, out T removedItem, int[] groupIndexes = null)
	{
		removedItem = null;

		for (int i = 0; i < itemSlots.Length; i++)
		{
			bool isItemAllowed = IsItemAllowedInGroup(i, groupIndexes);

			if (IsItemAllowedInGroup(i, groupIndexes))
			{
				itemSlots[i].Add(item, out removedItem);

				OnItemAdded?.Invoke(item, i);

				if (removedItem != null)
					OnItemRemoved?.Invoke(removedItem, i);

				return true;
			}
		}

		return false;
	}

	public virtual void Remove(T item)
	{

	}

	public void SetMaxStackSize(int slotIndex, int maxStackSize)
    {
		itemSlots[slotIndex].MaxStackSize = maxStackSize;
    }

	public void SetItemIndexesAllowed(int slotIndex, int[] itemIndexes = null)
    {
		itemSlots[slotIndex].ItemIndexesAllowed = itemIndexes;
    }

	public void SetSwapIfFull(int slotIndex, bool swapIfFull)
    {
		itemSlots[slotIndex].SwapIfFull = swapIfFull;
    }

	public int GetMaxStackSize(int slotIndex)
    {
		return itemSlots[slotIndex].MaxStackSize;
    }

	public int[] GetItemIndexesAllowed(int slotIndex)
	{
		return itemSlots[slotIndex].ItemIndexesAllowed;
	}

	public bool GetSwapIfNull(int slotIndex)
	{
		return itemSlots[slotIndex].SwapIfFull;
	}

	private bool IsItemAllowedInGroup(int slotIndex, int[] groupIndexes = null)
    {
		bool isItemAllowed = false;

		// Always allow item if the group is null
		if (groupIndexes == null)
			return true;

		foreach (int groupIndex in groupIndexes)
        {
			foreach (int slotGroupIndex in itemSlots[slotIndex].ItemIndexesAllowed)
            {
				if (groupIndex == slotGroupIndex)
                {
					isItemAllowed = true;
					break;
                }
            }

			if (isItemAllowed)
				break;
        }

		return isItemAllowed;
	}
}