public interface IKeystoneItem
{
	string Name { get; }
	string Description { get; }
	bool AutoUse { get; }
	int[] GroupIndexes { get; }
	void Use();
}