using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{

    Camera camera = Camera.main;
    public Animator animator;
    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
        {
            GameObject objHit = hit.transform.gameObject;

            if (objHit.tag == "Door")
            {
                print("Looking at door");
            }
            else
                print("Not");

        }
    }

    private void OpenDoor()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
        {
            print(hit.transform.name);
            GameObject objHit = hit.transform.gameObject;

            if (objHit.tag == "Door")
            {
                print("Hit door");
                animator.SetBool("Open 0", true);

            }
        }
    }
}
