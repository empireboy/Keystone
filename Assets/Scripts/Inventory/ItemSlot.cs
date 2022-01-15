using System.Collections.Generic;

public class ItemSlot<T>
{
	public Stack<T> Items { get; set; }
	public int[] ItemIndexesAllowed { get; set; }
	public int MaxStackSize { get; set; }

	public ItemSlot(int maxStackSize = 1)
	{
		Items = new Stack<T>();
		ItemIndexesAllowed = null;
		MaxStackSize = maxStackSize;
	}
}