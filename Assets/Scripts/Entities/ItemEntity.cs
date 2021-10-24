public class ItemEntity : MovingEntity
{
	public float destroyDelay;

	public void Collect(KeystoneInventory inventory)
	{
		inventory.Add(new TestItem());

		Destroy(gameObject, destroyDelay);
	}
}