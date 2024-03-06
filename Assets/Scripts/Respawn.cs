using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Respawn : MonoBehaviour
{
    private Vector3 checkPoint;

    private void Awake()
    {
        checkPoint = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Death Plane")
        {
            this.GetComponent<CharacterController>().enabled = false;
            transform.position = checkPoint;
            this.GetComponent<CharacterController>().enabled = true;
        }

        if (other.gameObject.tag == "Checkpoint")
        {
            checkPoint = other.transform.position;
            Destroy(other.gameObject);
        }
    }
}
