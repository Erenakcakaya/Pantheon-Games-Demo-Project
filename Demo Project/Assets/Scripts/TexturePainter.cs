using System.Collections;
using UnityEngine;
using TMPro;

public class TexturePainter : MonoBehaviour
{

	public GameObject brushContainer;
	public Camera canvasCam;
	public RenderTexture canvasTexture;
	public GameObject brush;
	public Camera mainCam;
	public AudioSource audioSource1;
	public AudioSource audioSource2;
	public Animator animator;
	public TMP_Text text;
	public CanvasGroup nextLevelButton;

	private PaintTrigger trigger;
	private LayerMask paintLayer;
	private int brushCounter = 0;
	private int maxBrush = 300;
	private bool once = true;

	void Start()
	{
		nextLevelButton.alpha = 0;
		trigger = GameObject.FindGameObjectWithTag("Finish").GetComponent<PaintTrigger>();
		paintLayer = LayerMask.NameToLayer("Painting Wall");
	}
	void Update()
	{
		if (trigger.startPainting)
        {
			if (Input.GetButtonDown("Fire1") && brushCounter < maxBrush)
				audioSource1.Play();
			else if (Input.GetButton("Fire1") && brushCounter < maxBrush)
				Paint();
			else if (Input.GetButtonUp("Fire1") && once)
			{
				audioSource1.Stop();
				AllPainted();
			}
		}
	}

	void Paint()
	{
		text.text ="Kalan Boya: "+(maxBrush - brushCounter) * 100 / maxBrush;
		Vector3 uvWorldPosition = Vector3.zero;

		Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
		Ray ray = mainCam.ScreenPointToRay(mousePos);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 200))
		{
			if (hit.transform.gameObject.layer == paintLayer)
			{
				Vector2 textureCoord = new Vector2(hit.textureCoord.x, hit.textureCoord.y); //Dokunulan yerin texturdaki konumu
				uvWorldPosition.x = textureCoord.x - canvasCam.orthographicSize;
				uvWorldPosition.y = textureCoord.y - canvasCam.orthographicSize;
				uvWorldPosition.z = 0;
				GameObject brushObj;

				brushObj = Instantiate(brush);
				brushObj.transform.parent = brushContainer.transform;
				brushObj.transform.localPosition = uvWorldPosition;
				brushObj.transform.localScale = Vector3.one;

				brushCounter++;
				if (brushCounter >= maxBrush)
				{
					brush = null;
					audioSource1.Stop();
				}
			}
		}
	}

	private void AllPainted()
	{
		if (brushCounter < maxBrush) return;

		animator.SetBool("Dance", true);
		audioSource2.Play();
		once = false;
		StartCoroutine(End());
	}

	IEnumerator End()
    {
		yield return new WaitForSeconds(2);
		nextLevelButton.alpha = 1;
    }

}
