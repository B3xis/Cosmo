using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyScript : MonoBehaviour
{
    public Vector2 pos;
    float speed = 0.025f;
    public GameObject coinPlace;
    
  
    UiScript uiScript;
    // Start is called before the first frame update
    private void Start()
    {
       //var targetPosition = coinPlace.transform.position;
       //var angle = Vector2.Angle(targetPosition, transform.position);
       //transform.eulerAngles = new Vector3(0f, 0f, angle);
        //gameObject.transform.rotation = Quaternion.Euler(-30, coinPlace.transform.rotation.y, coinPlace.transform.rotation.z);
        Invoke("Moove",0f);
        Destroy(gameObject, 4.5f);
        Transform uis = GameObject.FindGameObjectWithTag("Uix").GetComponent<Transform>();

        uiScript = uis.GetComponent<UiScript>();

    }
    void Moove()
    {
        transform.Translate(Vector2.left * speed);
        gameObject.transform.LookAt(new Vector2(2.22f, 5.05f));
         
        
        

        Invoke("Moove", 0f);
        
    }

    private void OnDestroy()
    {
        uiScript.MoneyCount();
        

    }
    public void SpawnCoin()
    {
        Instantiate(gameObject, pos, Quaternion.Euler(-30,0,0));
    }
}
