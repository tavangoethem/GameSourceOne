using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.Shapes;

public class Interaction : MonoBehaviour
{
    private Transform _selection;
    public float distanceFromObject = 5f;

    void Update()
    {
        if (_selection != null)
        {
            if (_selection.GetComponent<Interactable>() != null)
            {
                _selection.GetComponent<Interactable>().HideInteractable();
            }
            _selection = null;
        }

        RaycastHit hit;

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 
            distanceFromObject, Color.red);

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward,
            out hit, distanceFromObject))
        {
            var selection = hit.transform;
            
            Debug.Log(selection.gameObject.name);

            if (selection.GetComponent<Interactable>() != null)
            {
                selection.GetComponent<Interactable>().DisplayInteractable();
            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                if (selection.GetComponent<DoorScript>() != null)
                {
                    selection.GetComponent<DoorScript>().DoorInteraction();
                }
            }

            _selection = selection;
        }
    }
}
