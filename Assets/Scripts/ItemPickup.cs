using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable {
    [Header("References")]
    [SerializeField] private ItemData itemData;

    public void Interact() {
        Debug.Log("Picked up: " + itemData.itemName);
        InventorySystem.Instance.AddItem(itemData);
        Destroy(gameObject);
    }
}