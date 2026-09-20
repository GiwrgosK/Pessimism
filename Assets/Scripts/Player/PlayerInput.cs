using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInput : MonoBehaviour {
    public event Action OnJump;
    public event Action OnInteract;

    [Header("References")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference lookAction;
    [SerializeField] private InputActionReference sprintAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference crouchAction;
    [SerializeField] private InputActionReference interactAction;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool IsSprinting { get; private set; }
    public bool IsCrouching { get; private set; }

    private void OnEnable() {
        moveAction.action.performed += callbackContext => MoveInput = callbackContext.ReadValue<Vector2>();
        moveAction.action.canceled += callbackContext => MoveInput = Vector2.zero;

        lookAction.action.performed += callbackContext => LookInput = callbackContext.ReadValue<Vector2>();
        lookAction.action.canceled += callbackContext => LookInput = Vector2.zero;

        sprintAction.action.performed += callbackContext => IsSprinting = true;
        sprintAction.action.canceled += callbackContext => IsSprinting = false;

        jumpAction.action.started += callbackContext => OnJump?.Invoke();

        crouchAction.action.performed += callbackContext => IsCrouching = true;
        crouchAction.action.canceled += callbackContext => IsCrouching = false;

        interactAction.action.performed += callBackContext => OnInteract?.Invoke();
    }

    private void OnDisable() {
        moveAction.action.performed -= callbackContext => MoveInput = callbackContext.ReadValue<Vector2>();
        moveAction.action.canceled -= callbackContext => MoveInput = Vector2.zero;

        lookAction.action.performed -= callbackContext => LookInput = callbackContext.ReadValue<Vector2>();
        lookAction.action.canceled -= callbackContext => LookInput = Vector2.zero;

        sprintAction.action.performed -= callbackContext => IsSprinting = true;
        sprintAction.action.canceled -= callbackContext => IsSprinting = false;

        jumpAction.action.started -= callbackContext => OnJump?.Invoke();

        crouchAction.action.performed -= callbackContext => IsCrouching = true;
        crouchAction.action.canceled -= callbackContext => IsCrouching = false;

        interactAction.action.performed -= callBackContext => OnInteract?.Invoke();
    }
}