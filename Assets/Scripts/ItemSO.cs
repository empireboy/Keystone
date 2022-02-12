using UnityEngine;

public abstract class ItemSO : ScriptableObject, IKeystoneItem
{
    public ItemTypes Type => _type;
    public string Name => _name;
    public string Description => _description;
    public bool AutoUse => _autoUse;
    public int[] GroupIndexes => _groupIndexes;
    public Sprite Sprite => _sprite;

    [SerializeField]
    private ItemTypes _type;

    [SerializeField]
    private string _name;

    [SerializeField]
    private string _description;

    [SerializeField]
    private bool _autoUse;

    [SerializeField]
    private int[] _groupIndexes;

    [SerializeField]
    private Sprite _sprite;

    public abstract void Use();
}