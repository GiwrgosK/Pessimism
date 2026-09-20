using UnityEngine;
using System;

public class PlayerMove : MonoBehaviour {
    public event Action<float> OnSprint;

    [Header("References")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private CharacterController characterController;

    private Vector3 velocity;
    private readonly float walkSpeed = 6f;
    private readonly float sprintMultiplier = 2f;
    private readonly float crouchMultiplier = 0.5f;
    private readonly float jumpHeight = 1.2f;
    private readonly float maxStamina = 100f;
    private readonly float staminaDrainRate = 30f;
    private readonly float staminaGainRate = 10f;
    private float currentStamina;

    private void Awake() {
        currentStamina = maxStamina;
    }

    private void OnEnable() {
        playerInput.OnJump += PlayerInput_OnJump;
    }

    private void OnDisable() {
        playerInput.OnJump -= PlayerInput_OnJump;
    }

    private void Update() {
        HandleMovement();
        HandleCrouch();
    }

    private void HandleMovement() {
        float currentSpeed = walkSpeed;
        bool isMoving = playerInput.MoveInput.sqrMagnitude > 0.01f;

        if (playerInput.IsCrouching) {
            currentSpeed *= crouchMultiplier;
        } else if (isMoving && playerInput.IsSprinting && currentStamina > 0) {
            currentSpeed *= sprintMultiplier;
            currentStamina = Mathf.Max(currentStamina - (staminaDrainRate * Time.deltaTime), 0f);
            OnSprint?.Invoke(currentStamina);
        }

        if (!playerInput.IsSprinting && currentStamina < maxStamina) {
            currentStamina = Mathf.Min(currentStamina + (staminaGainRate * Time.deltaTime), maxStamina);
        }

        Vector3 move = transform.right * playerInput.MoveInput.x + transform.forward * playerInput.MoveInput.y;

        if (characterController.isGrounded && velocity.y < 0) {
            velocity.y = -2f; 
        }
        velocity.y += Physics.gravity.y * Time.deltaTime;

        Vector3 finalMovement = (move * currentSpeed) + velocity;
        characterController.Move(finalMovement * Time.deltaTime);
    }

    private void HandleCrouch() {
        float targetHeight = playerInput.IsCrouching ? 1f : 2f;
        characterController.height = Mathf.Lerp(characterController.height, targetHeight, Time.deltaTime * 10f);
    }

    private void PlayerInput_OnJump() {
        if (characterController.isGrounded && !playerInput.IsCrouching) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }
    }
}