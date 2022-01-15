public interface IInventory<T>
{
	event ItemEvent<T> OnItemAdded;
	event ItemEvent<T> OnItemRemoved;

	bool Add(T item);
	void Remove(T item);
}