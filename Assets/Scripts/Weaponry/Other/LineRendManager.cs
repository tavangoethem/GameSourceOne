using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendManager : MonoBehaviour
{
    public static LineRendManager Instance;

    [SerializeField] private Material _defaultLineMat;

    private void Awake()
    {
        Instance = this;
    }

    public void CreateRenederer(Vector3 startPos, Vector3 EndPos, float duration)
    {
        LineRenderer temp = gameObject.AddComponent<LineRenderer>();
        print(temp.transform.name);
        temp.startWidth = .01f;
        temp.endWidth= .01f;
        temp.material = _defaultLineMat;
        temp.positionCount = 2;
        temp.startColor = Color.clear;
        temp.endColor = Color.yellow;
        temp.SetPosition(0, startPos);
        temp.SetPosition(1, EndPos);
        StartCoroutine(DestoryAfterTime(temp, duration));
    }

    private IEnumerator DestoryAfterTime(UnityEngine.Object objToDestroy, float duration)
    {
        yield return new WaitForSeconds(duration);

        Destroy(objToDestroy);
    }
}
