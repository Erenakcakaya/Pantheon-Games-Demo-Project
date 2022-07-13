using UnityEngine;

public class SpinningObstacle : MonoBehaviour
{
    private Rigidbody rb;
    private float angle = 45f;
    [SerializeField] private float speed;
    public Vector3 axis;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        //Dönmesi gereken engelleri kendi etrafýnda döndürüyor
        Quaternion q = Quaternion.AngleAxis(angle, axis);
        q.ToAngleAxis(out angle, out axis);
        rb.angularVelocity = axis * speed * angle * Mathf.Deg2Rad;
    }
}
