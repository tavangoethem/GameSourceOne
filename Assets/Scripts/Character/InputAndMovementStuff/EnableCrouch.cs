using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class EnableCrouch : MonoBehaviour
{
    private MovementController controller;
    [SerializeField] private GameObject _particleSystem;

    public void Start()
    {
        controller = GetComponent<MovementController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ActivateAbility")
        {
            controller.canCrouch = true;
            Instantiate(_particleSystem, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
        else if (other.tag == "DeactivateAbility")
        {
            controller.canCrouch = false;
        }
    }
}