using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class LimbDamageHandler : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> _rbs;

    private void Start()
    {
        SetRBsKinematic(true);
        GetComponent<CharacterBase>().Heath.OnDeath += OnDeath;
    }

    public void OnDeath()
    {
        SetRBsKinematic(false);
    }

    public void GetAllRBs()
    {
        _rbs = new List<Rigidbody>();
        var temp = GetComponentsInChildren<Rigidbody>();

        foreach (var rb in temp)
        {
            if(rb != GetComponent<Rigidbody>())
                _rbs.Add(rb);
        }        
    }

    public void SetRBsKinematic(bool isKinematic)
    {
        if(_rbs.Count  <= 0)
        {
            GetAllRBs();
        }

        foreach (var rb in _rbs)
        {
            rb.isKinematic = isKinematic;
        }
    }

    public void AddComponentsToLimbs(ArmorType armorLevel)
    {
        GetAllRBs();
        foreach (var rb in _rbs)
        {
            if(rb.GetComponent<Limb>() != null)
                DestroyImmediate(rb.GetComponent<Limb>());
            rb.gameObject.AddComponent<Limb>().Owner = GetComponent<CharacterBase>();
            rb.GetComponent<Limb>().StupidArmorType = armorLevel;
        }
    }
}

[CustomEditor(typeof(LimbDamageHandler))]
class LimbDamageHandlerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Set Limbs"))
        {
            var myScript = target as LimbDamageHandler;
            myScript.AddComponentsToLimbs(ArmorType.light);
        }
            
    }

}
