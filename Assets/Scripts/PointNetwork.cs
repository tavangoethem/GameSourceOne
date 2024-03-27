using UnityEngine;

public class PointNetwork : MonoBehaviour
{
    [SerializeField] private Transform[] _points;   //Stores all of the points within this network

    [SerializeField] private Color _gizmoColour;    //What colour will be used in engine to show the path of points

    /// <summary>
    /// Loop through each element of the network
    /// If the next index does not go out of bounds, draw a line between the current and the next
    /// If the next index does go out of bounds, draw a line between the current and the first
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmoColour;

        if(_points == null || _points.Length < 2)
        {
            return;
        }

        for (int i = 0; i < _points.Length; i++)
        {
            if(i < _points.Length - 1)
            {
                Gizmos.DrawLine(_points[i].position, _points[i + 1].position);
            }
            else
            {
                Gizmos.DrawLine(_points[i].position, _points[0].position);
            }
        }
    }
}
