using System.Linq;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class KeystoneEntitiesInitializer : MonoBehaviour
{
	[SerializeField]
	private GameObject[] _entities;

	private void Awake()
	{
		IKeystoneEntity[] entities = _entities.Select(entity => entity.GetComponent<IKeystoneEntity>()).ToArray();

		GetComponent<GameManager>().AddEntities(entities);

		Destroy(this);
	}
}