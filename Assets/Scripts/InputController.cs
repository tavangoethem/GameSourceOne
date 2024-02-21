using UnityEngine;
using UnityEngine.InputSystem;

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

    private void UpdateRotation(Vector2 delta)
    {
        cameraLook?.UpdateRotation(delta);
    }

    private void OnDisable()
    {
        mainInput.Disable();
        mainInput.Player.Disable();
    }
}