public class ItemEntity : MovingEntity
{
	public float destroyDelay;

	public void Collect(KeystoneInventory inventory)
	{
		inventory.Add(new TestItem());

		gameManager.RemoveEntity(gameObject, destroyDelay);
	}
}