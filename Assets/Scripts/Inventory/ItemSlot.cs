using System.Collections.Generic;

public class ItemSlot<T> where T : class
{
	public int[] ItemIndexesAllowed { get; set; }
	public int MaxStackSize { get; set; }
	public bool IsFull => _items.Count >= MaxStackSize;
	public bool SwapIfFull { get; set; }

	private List<T> _items;

	public ItemSlot(int maxStackSize = 1)
	{
		ItemIndexesAllowed = null;
		MaxStackSize = maxStackSize;
		SwapIfFull = false;

		_items = new List<T>();
	}

	public bool Add(T item, out T removedItem)
    {
		if (IsFull)
        {
			// Remove the first item in the list and add the new item as last
			if (SwapIfFull)
            {
				removedItem = _items[0];
				_items.RemoveAt(0);
				_items.Add(item);

				return true;
			}
            else
            {
				removedItem = null;

				return false;
            }
        }
		
		removedItem = null;
		_items.Add(item);

		return true;
	}
}