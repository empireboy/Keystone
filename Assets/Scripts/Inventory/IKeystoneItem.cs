using UnityEngine;

public interface IKeystoneItem
{
	string Name { get; }
	string Description { get; }
	bool AutoUse { get; }
	void Use();
}