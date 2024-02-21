using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    [SerializeField, Min(0)] private float _movementSpeed;

    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void UpdateMovement(Vector2 movementDelta)
    {
        movementDelta = movementDelta * _movementSpeed;
        Vector3 velocity = new Vector3(movementDelta.x, Physics.gravity.y, movementDelta.y);
        velocity = transform.rotation * velocity;
        _characterController?.Move(velocity * Time.deltaTime);
    }
}