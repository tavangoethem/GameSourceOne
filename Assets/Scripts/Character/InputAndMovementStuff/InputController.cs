using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MovementController))]
public class InputController : MonoBehaviour
{
    [SerializeField] private MainInput mainInput;
    [SerializeField] private MovementController movementController;
    [SerializeField] private CameraLook cameraLook;
    private void Awake()
    {
        mainInput = new MainInput();
    }

    private void OnEnable()
    {
        mainInput.Enable();
        mainInput.Player.Enable();
        mainInput.Player.Crouch.started += StartCrouch;
        mainInput.Player.Crouch.canceled += EndCrouch;
        mainInput.Player.Jump.performed += JumpAction;
    }
    private void Update()
    {
        UpdateRotation(mainInput.Player.Look.ReadValue<Vector2>());
        UpdateMovement(mainInput.Player.Movement.ReadValue<Vector2>());
    }

    private void UpdateMovement(Vector2 delta)
    {
        movementController?.UpdateMovement(delta);
    }

    private void StartCrouch(InputAction.CallbackContext context)
    {
        movementController?.StartCrouchControl();
    }    
    private void EndCrouch(InputAction.CallbackContext context)
    {
        movementController?.AttemptExitCrouch();
    }

    private void JumpAction(InputAction.CallbackContext context)
    {
        movementController?.jumpNow();
    }

    private void UpdateRotation(Vector2 delta)
    {
        cameraLook?.UpdateRotation(delta);
    }

    private void OnDisable()
    {
        mainInput.Disable();
        mainInput.Player.Disable();
        mainInput.Player.Crouch.Disable();
        mainInput.Player.Crouch.started -= StartCrouch;
        mainInput.Player.Crouch.canceled -= EndCrouch;
        mainInput.Player.Jump.performed -= JumpAction;
    }
}