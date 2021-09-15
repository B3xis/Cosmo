using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyScript : MonoBehaviour
{
    private float speed = 0.0025f, shootTime = 1.8f;
    public int hp, destroyedEnemies = 0;
    BulletsScript bulletsScript;
    public GameObject enemyBullet;
    public Transform shootDirection;
    public AudioSource enemyShootOgg;
 
    LevelScript levelScript;
    MoneyScript moneyScript;
    UiScript uiScript;
    private void Start()
    {
        

        Transform levels = GameObject.FindGameObjectWithTag("EventSyst").GetComponent<Transform>();

        levelScript = levels.GetComponent<LevelScript>();
        Transform uis = GameObject.FindGameObjectWithTag("Uix").GetComponent<Transform>();

        uiScript = uis.GetComponent<UiScript>();

        if (levelScript.levelId > 1)
        {
            
            int multip = hp * levelScript.levelId/10;
          
            hp = hp + multip;
        }
        else
        {
            hp = 10;
        }
        
        Invoke("Moove",0f);
        Invoke("Shoot", shootTime - 0.5f);
    }
    private void Update()
    {
        destroyedEnemies = PlayerPrefs.GetInt("enemKill", destroyedEnemies);
    }

    public void Moove()
    {
        transform.Translate(Vector2.up * -speed);
        Invoke("Moove", 0f);
        if(gameObject.transform.position.y <= -5.3)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 7f, gameObject.transform.position.z);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Bullet")
        {
            Transform targetRigidbody1 = GameObject.FindGameObjectWithTag("Bullet").GetComponent<Transform>();

            bulletsScript = targetRigidbody1.GetComponent<BulletsScript>();
            Debug.Log(collision.name);
            hp -= bulletsScript.damageBullet;
            if (hp <= 0)
            {
                destroyedEnemies++;
                moneyScript = Resources.Load<MoneyScript>("Coin");
                moneyScript.pos = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
                moneyScript.SpawnCoin();

                PlayerPrefs.SetInt("enemKill", destroyedEnemies);
                if (destroyedEnemies >= levelScript.enemiesCount && levelScript.bossOn == false)
                {
                    levelScript.levelId++;
                    PlayerPrefs.SetInt("EndLvl", levelScript.levelId);
                    PlayerPrefs.SetInt("playerMoney",uiScript.moneyCounter);
                    levelScript.StartNextLevelPanel();
                }
                Destroy(gameObject);
                
            }
        }
        
       
    }
  
    public void Shoot()
    {       
      Instantiate(enemyBullet, new Vector3(shootDirection.position.x, shootDirection.position.y - 0.5f, 0), Quaternion.identity);

        enemyShootOgg.Play();
       

        Invoke("Shoot", shootTime);

    }

}
