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
            rb.velocity += Vector3.up * Physics.gravity.y * 5 * Time.deltaTime; //Oyuncular�n d��me h�z� 
        }
    }

    bool FallCheck()
    {
        return Physics.CheckSphere(transform.position, 0.5f, ground); //Oyuncular�n alt�ndaki trigger "Ground" layer'�na temas ediyorsa true d�nd�r�yor.
    }
}
