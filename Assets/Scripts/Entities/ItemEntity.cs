public class ItemEntity : MovingEntity
{
	public int index;
	public float destroyDelay;

	public void Collect(KeystoneInventory inventory)
	{
		switch (index)
        {
			case 0:

				inventory.Add(new TestItem());

				break;

			case 1:

				inventory.Add(new TestItem2());

				break;
        }

		gameManager.RemoveEntity(gameObject, destroyDelay);
	}
}