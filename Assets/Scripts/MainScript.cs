using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    #region  public
    public GameObject bullet, deathMenu, saveAdsPanel;
    public Transform shootDirectionRight, shootDirectionLeft;
    public bool mooveOn;
    public Slider hpSlider;
    public AudioSource playerShootOgg;
    #endregion

    #region  private
    private float shootTime;
    private bool gunQueue;
    public float hp, maxHp = 30;
    EnemyBullets enemyBullets;
    UiScript uiScript;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gunQueue = false;
        mooveOn = true;
        transform.position = new Vector3(0, -3.65f, -0.6923036f);
        shootTime = 0.5f;


        int startLvl = PlayerPrefs.GetInt("EndLvl");
        if (startLvl == 1)
        {
            hp = maxHp;
            PlayerPrefs.SetFloat("PlayerHp", maxHp);
        }
        else
        {
            hp = PlayerPrefs.GetFloat("PlayerHp");
        }
        hpSlider.maxValue = maxHp;
        hpSlider.value = hp;

        Invoke("Shoot", 4f);

    }    


    public void Moove()
    {

        if (mooveOn == true)
        {


            var mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = new Vector3(mousePosition.x, this.transform.position.y, this.transform.position.z);



            Vector3 vec3 = new Vector3(mousePosition.x, 0f, 0f);
            var angle = Vector3.Angle(Vector3.up, vec3 - this.transform.position);


            if (transform.position.x > 0.5f)
            {
                transform.eulerAngles = new Vector3(-90f, 0f, mousePosition.x > 0 ? angle : -angle);
            }
            else if (transform.position.x < -0.5f)
            {
                transform.eulerAngles = new Vector3(-90f, 0f, mousePosition.x > 0 ? angle : -angle);
            }
            else if (transform.position.x > -0.5f && transform.position.x < 0.5f)
            {
                transform.eulerAngles = new Vector3(-90f, 0f, 0f);
            }
        }
        else
        {

        }
    }
    public void Shoot()
    {  
        if(gunQueue == false)
        {
            Instantiate(bullet, new Vector3(shootDirectionRight.position.x, shootDirectionRight.position.y +0.5f, 0), Quaternion.Euler(0,0,90));
            gunQueue = true;
        }
        else
        {
            Instantiate(bullet, new Vector3(shootDirectionLeft.position.x, shootDirectionLeft.position.y + 0.5f, 0), Quaternion.Euler(0, 0, 90));
            gunQueue = false;
        }
        playerShootOgg.Play();


        Invoke("Shoot", shootTime);
    }
 
    void Update()
    {
        Moove();
       
    }


    private void OnTriggerEnter(Collider collision)
    {
      
        if (collision.tag == "EnemyBullet")
        {
            Transform enemyRigitbody = GameObject.FindGameObjectWithTag("EnemyBullet").GetComponent<Transform>();

            enemyBullets = enemyRigitbody.GetComponent<EnemyBullets>();
            Debug.Log(collision.name);
            hp -= enemyBullets.damageEnemyBullet;
            PlayerPrefs.SetFloat("PlayerHp", hp);
            hpSlider.value = hp;
            if (hp <= 0)
            {
                Transform uis = GameObject.FindGameObjectWithTag("Uix").GetComponent<Transform>();

                uiScript = uis.GetComponent<UiScript>();

                
                uiScript.Pauseall();

                if(PlayerPrefs.GetInt("SavedAds") == 0)
                {
                    gameObject.SetActive(false);
                    saveAdsPanel.SetActive(true);
                }
                else
                {
                    PlayerPrefs.SetInt("SavedAds", 0);
                    Destroy(gameObject);
                    deathMenu.SetActive(true);
                }



            }
        }
        

    }

}
