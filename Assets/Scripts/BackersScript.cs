using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackersScript : MonoBehaviour
{
    float speed = 0.02f;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.LookAt(player.transform);
        //Quaternion.Euler(-30, transform.rotation.y, transform.rotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed);
    }
}
