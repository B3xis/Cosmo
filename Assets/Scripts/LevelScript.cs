using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public int levelId = 1, enemiesCount;
    public bool bossOn = false, enemyDown = false, shop = false, selectionBuf = false;
    public GameObject nextLevelMenu, bufPanel;
    UiScript uiScript;
    // Start is called before the first frame update
    void Start()
    {
        Transform uis = GameObject.FindGameObjectWithTag("Uix").GetComponent<Transform>();

        uiScript = uis.GetComponent<UiScript>();
        Time.timeScale = 1;
        levelId = PlayerPrefs.GetInt("EndLvl", levelId);
        
        switch (levelId)
        {
            case 1:
                {
                    enemiesCount = 6;
                    selectionBuf = true;
                    break;
                }
            case 2:
                {
                    enemiesCount = 8;
                    selectionBuf = true;
                    break;
                }
            case 3:
                {
                    enemiesCount = 8;
                    break;
                }
            case 4:
                {
                    enemiesCount = 9;
                    shop = true;
                    break;
                }
            case 5:
                {
                    bossOn = true;
                    enemiesCount = 6;
                    selectionBuf = true;
                    break;
                }
            case 6:
                {
                    enemiesCount = 12;
                    break;
                }
            case 7:
                {
                    enemiesCount = 14;
                    break;
                }
            case 8:
                {
                    enemiesCount = 14;
                    selectionBuf = true;
                    break;
                }
            case 9:
                {
                    enemiesCount = 16;
                    shop = true;
                    break;
                }
            case 10:
                {
                    enemiesCount = 10;
                    bossOn = true;
                    selectionBuf = true;
                    break;
                }
            case 11:
                {
                    enemiesCount = 12;
                   
                    break;
                }
            case 12:
                {
                    enemiesCount = 14;
                    selectionBuf = true;
                    break;
                }
            case 13:
                {
                    enemiesCount = 16;
                    
                    break;
                }
            case 14:
                {
                    enemiesCount = 15;
                    shop = true;
                    break;
                }
            case 15:
                {
                    enemiesCount = 12;
                    bossOn = true;
                    selectionBuf = true;
                    break;
                }
            case 16:
                {
                    enemiesCount = 8;
                   
                    break;
                }
            case 17:
                {
                    enemiesCount = 12;
                   
                    break;
                }
            case 18:
                {
                    enemiesCount = 18;
                   
                    break;
                }
            case 19:
                {
                    enemiesCount = 16;
                    shop = true;
                    break;
                }
            case 20:
                {
                    enemiesCount = 15;
                    bossOn = true;
                    selectionBuf = true;
                    break;
                }
            case 21:
                {
                    enemiesCount = 10;
                    
                    break;
                }
            case 22:
                {
                    enemiesCount = 17;
            
                    break;
                }
            case 23:
                {
                    enemiesCount = 12;
                    selectionBuf = true;
                    break;
                }
            case 24:
                {
                    enemiesCount = 15;
                    shop = true;
                    break;
                }
            case 25:
                {
                    enemiesCount = 20;
                    bossOn = true;
                    break;
                }
            
            default:
                {
                    
                    Debug.Log("Ass with saved levels!"); 
                }
            break;
        }
    }

   public void StartNextLevelPanel()
    {
        uiScript.Pauseall();
        nextLevelMenu.SetActive(true);
        if(selectionBuf == true)
        {
            bufPanel.SetActive(true);
        }
        else
        {
            bufPanel.SetActive(false);
        }
    }
    
}
