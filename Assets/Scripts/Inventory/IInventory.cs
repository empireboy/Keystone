public interface IInventory<T>
{
	event ItemEvent<T> OnItemAdded;
	event ItemEvent<T> OnItemRemoved;

	bool Add(T item, out T removedItem, int[] groupIndexes = null);
	void Remove(T item);
}