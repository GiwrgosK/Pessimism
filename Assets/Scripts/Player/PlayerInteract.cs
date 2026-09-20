using UnityEngine;

public class PlayerInteract : MonoBehaviour {
    [Header("References")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private LayerMask interactLayerMask;

    private readonly float interactRange = 3f;

    private void OnEnable() {
        playerInput.OnInteract +=  PlayerInput_OnInteract;
    }

    private void OnDisable() {
        playerInput.OnInteract -=  PlayerInput_OnInteract;
    }

    private void PlayerInput_OnInteract() {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, interactRange, interactLayerMask)) {
            if (hit.collider.TryGetComponent(out IInteractable interactable)) {
                interactable.Interact();
            }
        }
    }
}