using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {
    [Header("References")]
    [SerializeField] private List<Image> inventoryListUI;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerMove playerMove;

    private void Start() {
        InventorySystem.Instance.OnActiveSlotChanged += InventorySystem_OnActiveSlotChanged;
        InventorySystem.Instance.OnInventoryChanged += InventorySystem_OnInventoryChanged;
        playerHealth.OnHealthChanged += PlayerHealth_OnHealthChanged;
        playerMove.OnSprint += PlayerMove_OnSprint;
    }

    private void OnDestroy() {
        InventorySystem.Instance.OnActiveSlotChanged -= InventorySystem_OnActiveSlotChanged;
        InventorySystem.Instance.OnInventoryChanged -= InventorySystem_OnInventoryChanged;
        playerHealth.OnHealthChanged -= PlayerHealth_OnHealthChanged;
        playerMove.OnSprint += PlayerMove_OnSprint;
    }

    private void InventorySystem_OnActiveSlotChanged(int newActiveSlotIndex) {
    }

    private void InventorySystem_OnInventoryChanged(ItemData itemData) {
        UpdateUI(itemData);
    }

    private void PlayerHealth_OnHealthChanged(float amount) {
        healthSlider.value -= amount;
        Debug.Log($"Health Slider Value: {healthSlider.value}");
    }

    private void PlayerMove_OnSprint(float amount) {
        staminaSlider.value -= amount;
         Debug.Log($"Stamina Slider Value: {staminaSlider.value}");
    }

    private void UpdateUI(ItemData itemData) {
        for (int i = 0; i < inventoryListUI.Count; i++) {
            inventoryListUI[i].sprite = itemData.itemSprite;
        }
    }
}