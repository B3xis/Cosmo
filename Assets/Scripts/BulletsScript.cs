using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsScript : MonoBehaviour
{
    float speed = 0.05f;
    public int damageBullet = 5; //5
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet", 4f);
        Invoke("Moove", 0f);
    }



    public void Moove()
    {
        transform.Translate(Vector2.right * speed);
        Invoke("Moove", 0f);
    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }

   private void OnTriggerEnter2D(Collider2D hit)
   {
       Debug.Log(hit.name);
       Destroy(gameObject);
   }
 
    
}
