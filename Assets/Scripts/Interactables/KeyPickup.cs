using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class KeyPickup : MonoBehaviour, IInteractable
{
    [SerializeField] public GameObject Tag;

    [SerializeField] public Material[] _materials;

    [SerializeField] public Keys Key;
    public void OnEnable()
    {
        Renderer temp = Tag.GetComponent<Renderer>();
        switch (Key)
        {
            case Keys.Key1:
                if (_materials[0])
                    temp.material = _materials[1];
                break;
            case Keys.Key2:
                if (_materials[1])
                    temp.material = _materials[2];
                break;
            case Keys.Key3:
                if (_materials[2])
                    temp.material = _materials[3];
                break;
            case Keys.Key4:
                if (_materials[3])
                    temp.material = _materials[4];
                break;
        }
    }

    public void Interact(GameObject objAttemptingInteraction)
    {
        objAttemptingInteraction.GetComponent<CharacterInventory>()?.UpdateKeys(Key);
        Destroy(gameObject);
    }
}

[System.Flags]
public enum Keys
{
    Key1 = 1,
    Key2 = 2,
    Key3 = 4,
    Key4 = 8
}
