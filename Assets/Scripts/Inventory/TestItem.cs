using UnityEngine;

public class TestItem : IKeystoneItem
{
	public ItemTypes Type => ItemTypes.TestItem1;
	public string Name => "Test";
	public string Description => "This is an item used for testing purposes.";
	public int[] GroupIndexes => new int[] { 0 };
	public bool AutoUse => true;

    public void Use()
	{
		Object.FindObjectOfType<GameManager>().GetEntity("Player").GetComponent<PlayerInput>().movablePositions =
			Resources.Load<KeystonePositionsSO>(Name);
	}
}

public class TestItem2 : IKeystoneItem
{
	public ItemTypes Type => ItemTypes.TestItem2;
	public string Name => "Test 2";
	public string Description => "This is an item used for testing purposes.";
	public int[] GroupIndexes => new int[] { 0 };
	public bool AutoUse => true;

    public void Use()
	{
		Object.FindObjectOfType<GameManager>().GetEntity("Player").GetComponent<PlayerInput>().movablePositions =
			Resources.Load<KeystonePositionsSO>(Name);
	}
}