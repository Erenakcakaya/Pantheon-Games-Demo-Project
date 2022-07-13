using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    void LateUpdate()
    {
        transform.position = target.position + offset; //Kamera belli bir uzaklýktan target'ý takip ediyor.
    }
}
