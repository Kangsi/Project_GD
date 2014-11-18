using UnityEngine;
using System.Collections;

public class InvisibleObject : MonoBehaviour
{

    void Start()
    {
        renderer.enabled = false;
    }
}
