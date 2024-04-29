using UnityEngine;

public class CrateMovingSound : MonoBehaviour
{
    public AudioSource ScrapingMetalSound;

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Rigidbody>().velocity.magnitude >= 0.2f)
        {
            ScrapingMetalSound.Play();
        }
    }
}
