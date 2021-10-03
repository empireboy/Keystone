using UnityEngine;

public class PrefabCreator : MonoBehaviour
{
	public GameObject prefab;

	public void Create()
	{
		Instantiate(prefab, transform.position, Quaternion.identity);
	}

	public GameObject CreateAt(Vector3 position)
	{
		return Instantiate(prefab, position, Quaternion.identity);
	}
}
