using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Serializable]
public class PauseEvent : UnityEvent { }
[RequireComponent(typeof(MovementController))]
public class InputController : MonoBehaviour
{
    [SerializeField] private MainInput mainInput;
    [SerializeField] private MovementController movementController;
    [SerializeField] private CameraLook cameraLook;

    public bool IsPaused;

    public PauseEvent startPause;
    public PauseEvent stopPause;

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
            mainInput.Player.Pause.performed += PauseAction;
    }
    private void Update()
    {
        if (IsPaused == false)
        {
            UpdateRotation(mainInput.Player.Look.ReadValue<Vector2>());
            UpdateMovement(mainInput.Player.Movement.ReadValue<Vector2>());
        }
    }

    private void UpdateMovement(Vector2 delta)
    {
        if (IsPaused == false)
        {
            movementController?.UpdateMovement(delta);
        }
    }

    private void StartCrouch(InputAction.CallbackContext context)
    {
        if (IsPaused == false)
        {
            movementController?.StartCrouchControl();
        }
    }    
    private void EndCrouch(InputAction.CallbackContext context)
    {
        if (IsPaused == false)
        {
            movementController?.AttemptExitCrouch();
        }
    }

    private void JumpAction(InputAction.CallbackContext context)
    {
        if (IsPaused == false)
        {
            movementController?.jumpNow();
        }
    }

    private void UpdateRotation(Vector2 delta)
    {
        if (IsPaused == false)
        {
            cameraLook?.UpdateRotation(delta);
        }
    }

    private void PauseAction(InputAction.CallbackContext context)
    {
        if (IsPaused == false)
        {
            IsPaused = true;
            startPause.Invoke();
            Time.timeScale = 0;
        }
        else if (IsPaused == true)
        {
            IsPaused = false;
            stopPause.Invoke();
            Time.timeScale = 1;
        }
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
    
    public void UnPause()
    {
        IsPaused = false;
        Time.timeScale = 1;
    }
}