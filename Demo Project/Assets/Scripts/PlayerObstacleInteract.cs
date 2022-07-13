using UnityEngine;

public class PlayerObstacleInteract : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;

    [HideInInspector] public bool playerTurnAround = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (playerTurnAround)
        {
            if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Stumble") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) //Animasyon sonlandýktan sonra geri doðuyor
            {
                transform.position = playerMovement.originalPos;
                animator.SetBool("StumbleCheck", false);
                playerMovement.stumble = false;
            }
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            animator.SetBool("StumbleCheck", true);
            playerMovement.stumble = true;
            playerTurnAround = true;

        }
    }
}

