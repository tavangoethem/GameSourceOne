using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathandRespawn : MonoBehaviour
[SerializeField]
    private Vector3 checkPoint;
{
   private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Death Plane")
            {
                this.GetComponent<CharacterController>().enabled = false;
                transform.position = checkPoint;
                this.GetComponent<CharacterController>().enabled = true;
            }
        }
}
