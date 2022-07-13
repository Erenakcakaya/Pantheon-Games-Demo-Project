using UnityEngine;

public class PaintTrigger : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Animator animator;
    private CameraSettings cameraSettings;
    private Transform playerTransform;
    private PlayerMovement player;
    private float getSpeed;
    private Transform wall;
    private bool position;
    
    [HideInInspector] public bool startPainting;
    public Camera cam;
    public CanvasGroup canvasGroupRank;
    public GameObject sprayText;
   
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        cameraSettings = GameObject.FindGameObjectWithTag("CameraObject").GetComponent<CameraSettings>();
        getSpeed = player.forwardSpeed;
        wall = GameObject.FindGameObjectWithTag("PaintingWall").transform;
    }

    void Update()
    {
        if(position)
        {
            //Karakterin gideceði konum belirlenip oraya yavaþça götürülür
            Vector3 targetPos = new Vector3(target.position.x, playerTransform.position.y, target.position.z);
            Vector3 pos = Vector3.MoveTowards(playerTransform.position, targetPos, Time.deltaTime * getSpeed);
            playerTransform.position = pos;

            if (playerTransform.position == targetPos)
            {
                //Animasyonu durdurur ve kameranýn konumunu deðiþtirir
                animator.SetFloat("Speed", 0);
                cam.transform.rotation = Quaternion.RotateTowards(cam.transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 18);
                canvasGroupRank.alpha = 0;
                Vector3 targetcamPos = new Vector3(0.65f, 3.5f, -5f);
                Vector3 camPos = Vector3.MoveTowards(cameraSettings.offset, targetcamPos, Time.deltaTime * 10);
                cameraSettings.offset = camPos;

                if (cameraSettings.offset == camPos)
                {
                    //Duvarý oyuncunun önüne getirir.
                    Vector3 targetWallPos = new Vector3(wall.position.x, 3, wall.position.z);
                    Vector3 wallPos = Vector3.MoveTowards(wall.position, targetWallPos, Time.deltaTime * (getSpeed-1));
                    wall.position = wallPos;
                    
                    if (wall.position == targetWallPos)
                    {
                        startPainting = true; //Boyamaya izin verir
                        position = false; //Bir daha update fonksiyonuna girmez
                        sprayText.SetActive(true);
                    }
                }

            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.enabled = false; //Oyuncunun script'ini kapatýr
            position = true; //Update fonksiyonuna girer
        }
    }
}
