using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    private int whatLvl;
    public GameObject selectStartGamePanel;
    public void GameStart()
    {
        whatLvl = PlayerPrefs.GetInt("EndLvl");
        if( whatLvl > 1)
        {
            selectStartGamePanel.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("EndLvl", 1);
            PlayerPrefs.SetInt("enemKill", 0);
            SceneManager.LoadScene("FightingLevel");
            PlayerPrefs.SetFloat("enemyDamage", 5);
            PlayerPrefs.SetInt("SavedAds", 0);
            PlayerPrefs.SetInt("playerMoney", 0);
        }
        
        
        
    }
    public void NewGameStart()
    {
       
        PlayerPrefs.SetInt("EndLvl", 1);
        PlayerPrefs.SetInt("enemKill", 0);
        SceneManager.LoadScene("FightingLevel");
        PlayerPrefs.SetFloat("enemyDamage", 5);
        PlayerPrefs.SetInt("SavedAds", 0);
        PlayerPrefs.SetInt("playerMoney", 0);
    }
    public void ContinueGameStart()
    {
        PlayerPrefs.SetInt("enemKill", 0);
        SceneManager.LoadScene("FightingLevel");
    }


}
