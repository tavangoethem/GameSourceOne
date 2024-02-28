using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Application.Quit();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<InputController>() != null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
