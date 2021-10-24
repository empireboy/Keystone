public class KeystoneInventory : Inventory<IKeystoneItem>
{
	public override void Add(IKeystoneItem item)
	{
		item.Use();

		items.Add(item);
	}

	public override void Remove(IKeystoneItem item)
	{
		items.Remove(item);
	}
}