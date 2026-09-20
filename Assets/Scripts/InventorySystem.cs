using UnityEngine;
using System;
using System.Collections.Generic;

public class InventorySystem : MonoBehaviour {
    public static InventorySystem Instance { get; private set; }

    public event Action<int> OnActiveSlotChanged;
    public event Action<ItemData> OnInventoryChanged;

    private List<ItemData> inventoryList;
    private int activeSlotIndex = 0;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        inventoryList = new List<ItemData>();
    }

    public void AddItem(ItemData item) {
        if (inventoryList.Count == 4) return;
        inventoryList.Add(item);
        OnInventoryChanged?.Invoke(item);
        Debug.Log($"Added: {item.itemName}");
    }

    public void RemoveItem(ItemData item) {
        if (inventoryList.Count == 0) return;
        inventoryList.Remove(item);
        OnInventoryChanged?.Invoke(item);
        Debug.Log($"Removed: {item.itemName}");
    }

    public void SetActiveSlot(int newActiveSlotIndex) {
        activeSlotIndex = newActiveSlotIndex;
        OnActiveSlotChanged?.Invoke(activeSlotIndex);
    }
}