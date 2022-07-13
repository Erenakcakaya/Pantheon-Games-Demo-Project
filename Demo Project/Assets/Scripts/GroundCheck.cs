using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Rigidbody rb;

    void Update()
    {
        if (FallCheck())
            animator.SetBool("FallCheck", false);
        else
        {
            animator.SetBool("FallCheck", true);
            rb.velocity += Vector3.up * Physics.gravity.y * 5 * Time.deltaTime; //Oyuncularýn düþme hýzý 
        }
    }

    bool FallCheck()
    {
        return Physics.CheckSphere(transform.position, 0.5f, ground); //Oyuncularýn altýndaki trigger "Ground" layer'ýna temas ediyorsa true döndürüyor.
    }
}
