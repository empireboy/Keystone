public class PresetInventory<T> : IInventory<T>
{
	public event ItemEvent<T> OnItemAdded;
	public event ItemEvent<T> OnItemRemoved;

	protected ItemSlot<T>[] itemSlots;

	public PresetInventory(int size)
	{
		itemSlots = new ItemSlot<T>[size];
	}

	public virtual bool Add(T item)
	{
		for (int i = 0; i < itemSlots.Length - 1; i++)
		{
			if (itemSlots[i] == null)
				itemSlots[i] = new ItemSlot<T>();

			if (itemSlots[i].GetType().GetGenericArguments()[0].Equals(typeof(T)))
			{
				itemSlots[i].Items.Push(item);

				OnItemAdded?.Invoke(i);

				return true;
			}
		}

		return false;
	}

	public virtual void Remove(T item)
	{

	}
}