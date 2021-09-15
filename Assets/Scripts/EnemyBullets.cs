using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullets : MonoBehaviour
{
    float speed = 0.035f;
    public float damageEnemyBullet = 5;
    LevelScript levelScript;
    // Start is called before the first frame update
    void Start()
    {
        Transform levels = GameObject.FindGameObjectWithTag("EventSyst").GetComponent<Transform>();

        levelScript = levels.GetComponent<LevelScript>();
        damageEnemyBullet = PlayerPrefs.GetFloat("enemyDamage");
        if(levelScript.levelId % 3 == 0)
        {
            
            float multip = damageEnemyBullet * levelScript.levelId / 10;
            damageEnemyBullet = damageEnemyBullet + multip;
            PlayerPrefs.SetFloat("enemyDamage", damageEnemyBullet);
        }

        Invoke("DestroyBullet", 4f);
        Invoke("Moove", 0f);
    }



    public void Moove()
    {
        transform.Translate(Vector2.up * -speed);
        Invoke("Moove", 0f);
    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider hit)
    {
        Debug.Log(hit.name);
        Destroy(gameObject);
    }
    
}
