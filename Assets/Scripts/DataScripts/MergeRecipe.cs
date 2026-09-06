using UnityEngine;

[CreateAssetMenu(menuName = "Merge/Merge Recipe")]
public class MergeRecipe : ScriptableObject
{
    [SerializeField] private ItemDefinition _firstItem;
    [SerializeField] private ItemDefinition _secondItem;
    [SerializeField] private ItemDefinition _result;

    public ItemDefinition FirstItem => _firstItem;
    public ItemDefinition SecondItem => _secondItem;
    public ItemDefinition Result => _result;
}