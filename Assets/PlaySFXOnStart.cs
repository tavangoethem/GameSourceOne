using UnityEngine;

public class PlaySFXOnStart : MonoBehaviour
{
    [SerializeField] private AudioClip _SFXToPlay;
    [SerializeField] private bool _IsLoop = false;
    [SerializeField] private int _MinDistanceHeard = 1;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlaySFX(_SFXToPlay, transform.position, 1, _IsLoop, _MinDistanceHeard);
    }
}
