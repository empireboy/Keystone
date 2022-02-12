using UnityEngine;

public interface IKeystoneItem
{
	ItemTypes Type { get; }
	string Name { get; }
	string Description { get; }
	bool AutoUse { get; }
	int[] GroupIndexes { get; }
	Sprite Sprite { get; }
	void Use();
}