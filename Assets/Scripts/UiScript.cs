using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{

    EnemyScript enemyScript;
    BulletsScript bulletsScript;
    public MainScript mainScript;
    LevelScript levelScript;
    ExplosionController explosionController;
    EnemyBullets enemyBullets;

    public Slider hpSlider;
    public GameObject muteImg, savingAdsPanel, player, endGamePanel;
    public Text levelLext, moneyText;
    public AudioSource playerBulletsAudio, enemyBulletsAudio;
    public int moneyCounter = 0;

    public bool isMute = false;
    private void Start()
    {
        Transform levels = GameObject.FindGameObjectWithTag("EventSyst").GetComponent<Transform>();

        levelScript = levels.GetComponent<LevelScript>();
        if (levelScript.levelId == 1)
        {
            moneyCounter = PlayerPrefs.GetInt("playerMoney");
        }
        else
        {
            moneyCounter = PlayerPrefs.GetInt("playerMoney") + 1;
        }
        
        moneyText.text = moneyCounter.ToString();
        levelLext.text = "L" + PlayerPrefs.GetInt("EndLvl").ToString();
        isMute = System.Convert.ToBoolean(PlayerPrefs.GetString("AudioSet"));
        if(isMute == true)
        {
            isMute = false;
            AudioMute();
        }
        else if(isMute == false)
        {
            isMute = true;
            AudioMute();
        }
        
    }
    public void Pauseall()
    {
        


        //------------------------------------------------------------------------

        GameObject[] targetRigidbody1 = GameObject.FindGameObjectsWithTag("Bullet");

        //------------------------------------------------------------------------
        
        GameObject[] enemyBullet = GameObject.FindGameObjectsWithTag("EnemyBullet");

        //------------------------------------------------------------------------
        
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

        //------------------------------------------------------------------------

        Transform playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        mainScript = playerRigidbody.GetComponent<MainScript>();

        //------------------------------------------------------------------------

        
            
                Time.timeScale = 0;
        
                if (levelScript.bossOn == true && levelScript.enemyDown == true)
                {
                     Transform bossObject = GameObject.FindGameObjectWithTag("BossEnemy").GetComponent<Transform>();
                    explosionController = bossObject.GetComponent<ExplosionController>();
                    
                    explosionController.CancelInvoke("Shoot");
                    explosionController.CancelInvoke("Moove");
                }

            
                 for (int i = 0; i < enemyObjects.Length; ++i)
                 {

                     enemyScript = enemyObjects[i].gameObject.GetComponent<EnemyScript>();
                     if (enemyScript.destroyedEnemies <= levelScript.enemiesCount)
                     {
                              enemyScript.CancelInvoke("Moove");
                              enemyScript.CancelInvoke("Shoot");
                     }
                 }
           
            
            for (int i = 0; i < targetRigidbody1.Length; ++i)
            {

                bulletsScript = targetRigidbody1[i].GetComponent<BulletsScript>();
                bulletsScript.CancelInvoke("Moove");
            }

            for (int i = 0; i < enemyBullet.Length; ++i)
            {

                enemyBullets = enemyBullet[i].GetComponent<EnemyBullets>();
                enemyBullets.CancelInvoke("Moove");
            }
            mainScript.mooveOn = false;
            mainScript.CancelInvoke("Shoot");
        
      
        
    }
    public void UnpauseAll()
    {
       

        //------------------------------------------------------------------------

        GameObject[] targetRigidbody1 = GameObject.FindGameObjectsWithTag("Bullet");

        //------------------------------------------------------------------------

        GameObject[] enemyBullet = GameObject.FindGameObjectsWithTag("EnemyBullet");

        //------------------------------------------------------------------------

        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

        //------------------------------------------------------------------------

        Transform playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        mainScript = playerRigidbody.GetComponent<MainScript>();

        //------------------------------------------------------------------------



        Time.timeScale = 1;

        if (levelScript.bossOn == true && levelScript.enemyDown == true)
        {
            Transform bossObject = GameObject.FindGameObjectWithTag("BossEnemy").GetComponent<Transform>();
            explosionController = bossObject.GetComponent<ExplosionController>();

            explosionController.Invoke("Shoot",0f);
            explosionController.Invoke("Moove",0f);
        }

        for (int i = 0; i < enemyObjects.Length; ++i)
        {

            enemyScript = enemyObjects[i].gameObject.GetComponent<EnemyScript>();
            if (enemyScript.destroyedEnemies <= levelScript.enemiesCount)
            {
                enemyScript.Invoke("Moove", 0f);
                enemyScript.Invoke("Shoot", 0f);
            }
            
        }
        for (int i = 0; i < targetRigidbody1.Length; ++i)
        {

            bulletsScript = targetRigidbody1[i].GetComponent<BulletsScript>();
            bulletsScript.Invoke("Moove", 0f);
        }
        for (int i = 0; i < enemyBullet.Length; ++i)
        {

            enemyBullets = enemyBullet[i].GetComponent<EnemyBullets>();
            enemyBullets.Invoke("Moove",0f);
        }
        mainScript.mooveOn = true;
        mainScript.Invoke("Shoot",0f);

    }

    public void AudioMute()
    {
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        if (isMute == false)
        {
            
            isMute = true;
            
            
            playerBulletsAudio.mute = true;

            for (int i = 0; i < enemyObjects.Length; ++i)
            {
                enemyBulletsAudio = enemyObjects[i].gameObject.GetComponent<AudioSource>();
                enemyBulletsAudio.mute = true;
            }
            enemyScript = Resources.Load<EnemyScript>("ship1");
            enemyScript.enemyShootOgg.mute = true;
            enemyScript = Resources.Load<EnemyScript>("ship2");
            enemyScript.enemyShootOgg.mute = true;
            PlayerPrefs.SetString("AudioSet", isMute.ToString());
            muteImg.SetActive(true);
        }
        else if(isMute == true)
        {
           
            isMute = false;
            playerBulletsAudio.mute = false;
            for (int i = 0; i < enemyObjects.Length; ++i)
            {
                enemyBulletsAudio = enemyObjects[i].gameObject.GetComponent<AudioSource>();
                enemyBulletsAudio.mute = false;
            }
            enemyScript = Resources.Load<EnemyScript>("ship1");
            enemyScript.enemyShootOgg.mute = false;
            enemyScript = Resources.Load<EnemyScript>("ship2");
            enemyScript.enemyShootOgg.mute = false;
            PlayerPrefs.SetString("AudioSet", isMute.ToString());
            muteImg.SetActive(false);
        }
    }

    public void RestartAll()
    {
        if (levelScript.levelId >= 26)
        {
            SceneManager.LoadScene("MenuScene");
            PlayerPrefs.SetInt("enemKill", 0);
        }
        else
        {
            SceneManager.LoadScene("FightingLevel");
            PlayerPrefs.SetInt("enemKill", 0);
        }
       
    }
    public void GameEnd()
    {
        PlayerPrefs.SetInt("EndLvl", 1);
        SceneManager.LoadScene("MenuScene");
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void SavingAds()
    {
       
        PlayerPrefs.SetFloat("PlayerHp",mainScript.maxHp);
        mainScript.hp = PlayerPrefs.GetFloat("PlayerHp");
        player.SetActive(true);
        hpSlider.value = PlayerPrefs.GetFloat("PlayerHp");
        savingAdsPanel.SetActive(false);
        PlayerPrefs.SetInt("SavedAds", 1);
        UnpauseAll();
        
    }
    public void NoSavingAds()
    {
        savingAdsPanel.SetActive(false);
        PlayerPrefs.SetInt("SavedAds", 0);
        endGamePanel.SetActive(true);
    }
    public void MoneyCount()
    {
        moneyCounter++;
        moneyText.text = moneyCounter.ToString();
    }
}
