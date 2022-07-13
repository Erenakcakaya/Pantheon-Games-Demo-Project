using UnityEngine;

public class Npc : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;

    public string npcName;
    [HideInInspector] public Vector3 originalPos;
    [HideInInspector] public bool stumble = false;
    [HideInInspector] public float dir; //Rakiplerin hareket y�n� ba�ka scriptten de�i�tiriliyor

    void Awake()
    {
        originalPos = transform.position;
        dir = 0;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().canMove == false) return;
        if (!stumble)
        {
            NPCMovement();
            animator.SetFloat("Speed", 1);
        }else
            animator.SetFloat("Speed", 0);

        if (transform.position.y <= -50) //D��erse ilk konumuna geri getiriliyor
        {
            transform.position = originalPos;
            transform.rotation = Quaternion.identity;
        }
    }

    public void NPCMovement()
    {
        float swerveAmount = 3 * Time.fixedDeltaTime * dir; //D�nme hareketinin y�n� ve h�z�
        swerveAmount = Mathf.Clamp(swerveAmount, -1, 1); //Fazladan de�ere ihtiyac�m�z yok
        int randomSpeed = Random.Range(3, 10);


        Vector3 move = new Vector3(transform.position.x + swerveAmount * 3 * Time.fixedDeltaTime, transform.position.y, transform.position.z + randomSpeed * Time.fixedDeltaTime);
        rb.MovePosition(move);

    }
}
