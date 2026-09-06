using UnityEngine;

[CreateAssetMenu(menuName = "Merge/Item Definition")]
public class ItemDefinition : ScriptableObject
{
    [SerializeField] private string _itemName;
    [SerializeField] private Sprite _icon;

    public string ItemName => _itemName;
    public Sprite Icon => _icon;
}