using UnityEngine;

public class MergeManager : MonoBehaviour
{
    public static MergeManager Instance { get; private set; }

    [SerializeField] private MergeRecipe[] _recipes;
    [SerializeField] private PartsSystem[] _itemPrefabs;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public bool TryMerge(PartsSystem firstItem, PartsSystem secondItem)
    {
        if (firstItem == null || secondItem == null)
            return false;

        if (firstItem == secondItem)
            return false;

        int recipeIndex = FindRecipeIndex(
            firstItem.Definition,
            secondItem.Definition
        );

        if (recipeIndex == -1)
            return false;

        Transform targetSlot = secondItem.transform.parent;

        if (targetSlot == null)
            return false;

        PartsSystem result = CreateResult(
            _itemPrefabs[recipeIndex],
            targetSlot
        );

        if (result == null)
            return false;

        firstItem.SetDropped();

        Destroy(firstItem.gameObject);
        Destroy(secondItem.gameObject);

        return true;
    }

    private int FindRecipeIndex(
        ItemDefinition first,
        ItemDefinition second)
    {
        for (int i = 0; i < _recipes.Length; i++)
        {
            MergeRecipe recipe = _recipes[i];

            bool correctOrder =
                recipe.FirstItem == first &&
                recipe.SecondItem == second;

            bool reversedOrder =
                recipe.FirstItem == second &&
                recipe.SecondItem == first;

            if (correctOrder || reversedOrder)
                return i;
        }

        return -1;
    }

    private PartsSystem CreateResult(
        PartsSystem prefab,
        Transform slot)
    {
        PartsSystem result = Instantiate(
            prefab,
            slot
        );

        result.transform.localPosition = Vector3.zero;
        result.transform.localScale = Vector3.one;

        result.Initialize(prefab.Definition);

        return result;
    }
}