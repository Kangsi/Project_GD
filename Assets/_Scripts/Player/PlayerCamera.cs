// John van den Berg

/*=============================================================================
	PlayerCamera 
=============================================================================*/
using UnityEngine;
using System.Collections;
/**
 * The PlayerCamera class
 * 
 * The PlayerCamera is a simple static camera.
 * 
 * The camera follows the player at a fixed angle
 * and can zoom in and out by using the MouseWheel.
 * 
 * The position of the camera is set to it's position 
 * in world space upon run-time. 
 * 
 * The angle is determined by its position relative to
 * the player, transform.LookAt() is used to aim the
 * camera in the direction of the player.
 * 
 * The distance is used by using linear interpolation
 * between the position of the player and the starting
 * position of the camera. 
 */
public class PlayerCamera : MonoBehaviour {

    public Transform target;
    public float zoomMin = 0.2f;
    public float zoomMax = 1f;
    public float distance = 0.5f;

    private Vector3 startPosition;
    private float offsetY;
    private float offsetZ;

    void Awake()
    {
        startPosition = transform.position;
        offsetY = transform.position.y;
        offsetZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (target)
        {
            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * .5f, zoomMin, zoomMax);
            Vector3 position = new Vector3(target.transform.position.x, target.transform.position.y+offsetY, target.transform.position.z+offsetZ);
            transform.position = Vector3.Lerp(target.position, position, distance);
            transform.LookAt(target);
        }
    }
}
