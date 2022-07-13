using System.Collections;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] float speed = 1;
    bool returnBool;
    Vector3 target;
    public float XPos; //Sadece x ekseninde deðiþiklik yapýlýyor
    Vector3 firstPos;


    private void Start()
    {
        firstPos = transform.localPosition;
        returnBool = false;
    }
    private void Update()
    {
        //Duvar engelini hareket ettiriyor.
        if (returnBool)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, firstPos, Time.deltaTime * speed);
            if (transform.localPosition == firstPos)
                StartCoroutine(WaitDownstairs());
        }
        else if (!returnBool)
        {
            target = new Vector3(XPos, transform.localPosition.y, transform.localPosition.z);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, Time.deltaTime * speed);
            if (transform.localPosition == target)
                StartCoroutine(WaitUpstairs());
        }
    }
    IEnumerator WaitDownstairs()
    {
        //Minik bir bekleme ekliyor.
        yield return new WaitForSeconds(0.6f);
        returnBool = false;
    }
    IEnumerator WaitUpstairs()
    {
        //Minik bir bekleme ekliyor.
        yield return new WaitForSeconds(0.6f);
        returnBool = true;
    }
}
