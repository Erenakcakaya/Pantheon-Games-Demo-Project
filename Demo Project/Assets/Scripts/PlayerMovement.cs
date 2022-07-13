using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float swerveSpeed = 0.05f;
    private Rigidbody rb;
    private Animator animator;
    private float posX;
    private float moveX;
    private float maxSwerve=0.5f;

    public float forwardSpeed = 5f;
    [HideInInspector] public bool canMove;
    [HideInInspector] public Vector3 originalPos;
    [HideInInspector] public bool stumble = false;

    public void Awake()
    {
        originalPos = transform.position;
    }
    void Start()
    {
        canMove = false;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            posX = Input.mousePosition.x;
            canMove = true;
        }
        else if (Input.GetButton("Fire1"))
        {
            moveX = Input.mousePosition.x - posX;
            posX = Input.mousePosition.x;
            animator.SetFloat("Speed", 1);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            moveX = 0f;
            animator.SetFloat("Speed", 0);
        }

        if (transform.position.y <=-50)
        {
            transform.position = originalPos;
            transform.rotation = Quaternion.identity;
            rb.velocity = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        if (canMove == false) return;

        if (!stumble)
        {
            if (Input.GetButton("Fire1"))
                Movement();
        }
    }

    public void Movement()
    {
        float swerveAmount = swerveSpeed * (moveX/30) * Time.fixedDeltaTime; //Karakterin dönme hareketinin yönü ve hýzý
        swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerve, maxSwerve); //Fazladan deðere ihtiyacýmýz yok

        Vector3 move = new Vector3(transform.position.x + swerveAmount * swerveSpeed * Time.fixedDeltaTime, transform.position.y, transform.position.z + forwardSpeed * Time.fixedDeltaTime);
        rb.MovePosition(move);

    }
}