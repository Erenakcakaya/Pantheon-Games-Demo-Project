using UnityEngine;

public class NpcObstacleInteract : MonoBehaviour
{
    private Animator animator;
    private Npc npc;

    [HideInInspector] public bool playerTurnAround = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        npc = GetComponent<Npc>();
    }

    void Update()
    {
        if(playerTurnAround)
        {
            //Animasyon sonlandýktan sonra geri doðuyor
            if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Stumble") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                transform.position = npc.originalPos;
                animator.SetBool("StumbleCheck", false);
                npc.stumble = false; 
            }
        }
    }

    public void OnCollisionEnter(Collision other) //Collision ile temas ederse de yanýyor
    {
        if(other.gameObject.tag == "Obstacle")
        {
            animator.SetBool("StumbleCheck", true);
            npc.stumble = true;
            playerTurnAround = true;
        }
    }

    void OnTriggerEnter(Collider other) //Trigger ile çarpmadan önce yapacaðý hareketi rastgele seçiyor
    {
        if (other.gameObject.tag == "Obstacle")
        {
            int randomDir = Random.Range(1, 6);
            if (randomDir <= 2)
                npc.dir = -500;
            else if(randomDir == 3)
                npc.dir = 0;
            else if(randomDir >= 4)
                npc.dir = 500;
        }
    }

    void OnTriggerExit(Collider other)// Trigger'dan çýktýktan sonra düz hareket etmesi için yönü 0 olarak ayarlanýyor
    {
        if (other.gameObject.tag == "Obstacle")
        {
            npc.dir = 0;
        }
    }

}

