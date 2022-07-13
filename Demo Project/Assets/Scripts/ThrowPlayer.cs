using UnityEngine;

public class ThrowPlayer : MonoBehaviour
{
    [SerializeField] private int throwValue;
    [SerializeField] private AudioSource hitAudio;

    public void OnCollisionEnter(Collision other) //Npclerin Trigger'larý onlarýn hareket yönlerini belirliyor. Trigger'lar çarpmalarýný kontol etmediði için çarpma ayarlarýný Collision'lardan yapabiliyorum
    {
        if(other.gameObject.tag == "Npc")
        {
            Vector3 n = transform.right + transform.up;
            other.gameObject.GetComponent<Rigidbody>().AddForce(n * throwValue);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 n = transform.right + transform.up;
            other.gameObject.GetComponent<Rigidbody>().AddForce(n * throwValue);
            hitAudio.Play();
        }
    }


}
