using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private GameObject sprayText;
    [SerializeField] private Slider slider;
    [SerializeField] float sliderSpeed;
    private bool decrease;
    private float sliderValue = 1;
    private CanvasGroup startPanel;
    
    private GameObject[] npcs;

    public CanvasGroup rank;
    public GameObject player;
    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;
    public TMP_Text text4;
    public TMP_Text text5;
    bool once = true;

    private void Start()
    {
        rank.alpha = 0;
        slider.maxValue = 1;
        slider.minValue = 0;
        decrease = false;
        startPanel = GameObject.FindGameObjectWithTag("StartPanel").GetComponent<CanvasGroup>();
        sprayText = GameObject.FindGameObjectWithTag("SprayText");
        sprayText.SetActive(false);
        npcs = GameObject.FindGameObjectsWithTag("Npc");
        
        //foreach(GameObject npc in npcs)
        //{

        //    Debug.Log(npc.gameObject.transform.localPosition.z);


        //}

    }
    void Update()
    {
        //Baþlangýçta buraya girerek oyuncuya nasýl oynayacaðýný gösteren Canvasý gösteriyor.
        Rank();
        if (!GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().canMove)
            BeforeStarting();
        else
            Play();
    }

        
    public void Play()
    {
        if (once)
        {
            StartCoroutine(WaitToAlpha(1));
            //Canvasýn alfasýný yavaþ sayýlabilecek bir hýzda düþürüyor
            startPanel.alpha -= Time.deltaTime * 5;
            rank.alpha = 1;
            if (startPanel.alpha == 0)
                once = false;
            
        }
    }

    IEnumerator WaitToAlpha(int sec)
    {
        //Alfa sýfýr olduktan sonra canvas'ý kapatýyor
        yield return new WaitForSeconds(sec);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().canMove = true;
        if (startPanel.alpha == 0)
            startPanel.gameObject.SetActive(false);
    }
    public void BeforeStarting()
    {
        //Slider ile oyuncunun yapmasý gereken hareket gösteriliyor
        if (sliderValue <= 1.1f && !decrease)
        {
            sliderValue += Time.deltaTime * sliderSpeed;
            slider.value = sliderValue;
            if (sliderValue >= 1)
                decrease = true;
        }
        else if (decrease)
        {
            sliderValue -= Time.deltaTime * sliderSpeed;
            slider.value = sliderValue;
            if (sliderValue <= 0)
                decrease = false;
        }
    }

    void Rank() //Rakiplerin sýralamasý
    {
        float n;
        float[] rank = new float[npcs.Length + 1];
        rank[npcs.Length] = player.transform.position.z;
        
        for (int i = 0; i < npcs.Length; i++)
        {
            rank[i] = npcs[i].transform.position.z;
        }

        for (int i = 0; i < rank.Length - 1; i++)
        {
            for (int j = i; j < rank.Length; j++)
            {
                if(rank[i] < rank[j])
                {
                    n = rank[j];
                    rank[j] = rank[i];
                    rank[i] = n;
                }
            }
        }

        foreach (GameObject a in npcs) //Sýralamanýn erkana yazýlmasý
        {
            if (a.transform.position.z == rank[0])
                text1.text = "1. " + a.gameObject.GetComponent<Npc>().npcName;
            else if (a.transform.position.z == rank[1])
                text2.text = "2. " + a.gameObject.GetComponent<Npc>().npcName;
            else if (a.transform.position.z == rank[2])
                text3.text = "3. " + a.gameObject.GetComponent<Npc>().npcName;
            else if (a.transform.position.z == rank[3])
                text4.text = "4. " + a.gameObject.GetComponent<Npc>().npcName;
            else if (a.transform.position.z == rank[4])
                text5.text = "5. " + a.gameObject.GetComponent<Npc>().npcName;
            else if (player.transform.position.z == rank[0])
                text1.text = "1. Player";
            else if (player.transform.position.z == rank[1])
                text2.text = "2. Player";
            else if (player.transform.position.z == rank[2])
                text3.text = "3. Player";
            else if (player.transform.position.z == rank[3])
                text4.text = "4. Player";
            else if (player.transform.position.z == rank[4])
                text5.text = "5. Player";
        }
    }

    public void LoadScene(int sceneIndex)
    {
        //Bir sonraki level
        SceneManager.LoadScene(sceneIndex);
    }
}
