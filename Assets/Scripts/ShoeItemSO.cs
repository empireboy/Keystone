using UnityEngine;

[CreateAssetMenu(fileName = "New Shoe Item", menuName = "ScriptableObjects/Items/Shoe")]
public class ShoeItemSO : ItemSO
{
    public KeystonePositionsSO keystonePositionsSO;

    public override void Use()
    {
        FindObjectOfType<GameManager>()
            .GetEntity("Player")
            .GetComponent<PlayerInput>()
            .movablePositions = keystonePositionsSO;
    }
}