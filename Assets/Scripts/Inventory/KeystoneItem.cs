using UnityEngine;

public interface IKeystoneItem
{
	string Name { get; }
	string Description { get; }
	void Use();
}

public class TestItem : IKeystoneItem
{
	public string Name => "Test";
	public string Description => "This is an item used for testing purposes.";

	public void Use()
	{
		Object.FindObjectOfType<GameManager>().GetEntity("Player").GetComponent<PlayerInput>().movablePositions =
			Resources.Load<KeystonePositionsSO>(Name);
	}
}