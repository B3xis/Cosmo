using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPlanetsScript : MonoBehaviour
{

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.0008f, transform.position.z);
    }
}
