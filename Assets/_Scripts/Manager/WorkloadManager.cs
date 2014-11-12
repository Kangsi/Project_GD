using UnityEngine;
using System.Collections;

namespace Manager
{
    public class WorkloadManager : MonoBehaviour
    {
        public static Vector3 getMousePosition()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000))
            {
                return hit.point;
            }
            else
                return Vector3.zero;
        }

        public static Vector3 getMouseDirection(Vector3 position)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000))
            {
                return (hit.point - position).normalized;
            }
            else
                return Vector3.zero;
        }
    }
}
