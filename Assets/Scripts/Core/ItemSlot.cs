using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private int _slotIndex;
    [SerializeField] private bool _isRight;

    public static event Action ItemPlaced;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        PartsSystem incomingItem =
            eventData.pointerDrag.GetComponent<PartsSystem>();

        if (incomingItem == null)
            return;

        PartsSystem existingItem =
            GetComponentInChildren<PartsSystem>();

        // В слоте уже есть другой предмет.
        if (existingItem != null &&
            existingItem != incomingItem)
        {
            bool merged = MergeManager.Instance.TryMerge(
                incomingItem,
                existingItem
            );

            if (merged)
            {
                ItemPlaced?.Invoke();
                return;
            }
        }

        // Обычное помещение предмета в пустой слот.
        incomingItem.transform.SetParent(
            transform,
            false
        );

        incomingItem.transform.localPosition = Vector3.zero;

        incomingItem.SetDropped();

        CheckCorrectItem(incomingItem);
    }

    private void CheckCorrectItem(PartsSystem item)
    {
        if (item.Definition == null)
            return;

        if (item.GetComponent<PartsSystem>().Definition != null &&
            item.GetComponent<PartsSystem>().Definition.ItemName != null)
        {
            // Здесь оставляем твою старую логику проверки.
        }

        ItemPlaced?.Invoke();
    }
}