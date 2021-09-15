using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomethingVeryImportantScript : MonoBehaviour
{   
    public GameObject backPref, finalBoss;
    public Transform mainCamera;
    private float spawnRate = 3.5f;

    public List<GameObject> enemiesList = new List<GameObject>();
    public List<GameObject> planetList = new List<GameObject>();
    public List<GameObject> bossList = new List<GameObject>();
    public List<GameObject> bossList2 = new List<GameObject>();
    public List<GameObject> backsList = new List<GameObject>();
    public LevelScript levelScript;

    private int cameraRot = 0, spawnEnemyCount = 0;
    // Start is called before the first frame update

   
    private void Awake()
    {

        int what = Random.Range(0, 5);
        float wherre = Random.Range(-3.8f, 3.8f);
        Instantiate(planetList[what], new Vector3(wherre, backPref.transform.position.y + 4.9f, backPref.transform.position.z - 3f), Quaternion.identity);
        Invoke("SpawnEnemies", spawnRate - 1f );
        Invoke("MainCameraRotation", 0.8f);
       // SpawnBackers();
    }
    void SpawnEnemies()
    {
        if (spawnEnemyCount < levelScript.enemiesCount)
        {
            Instantiate(enemiesList[Random.Range(0, 2)], new Vector2(Random.Range(-1.5f, 1.8f), 7f), Quaternion.identity);
            spawnEnemyCount++;
            Invoke("SpawnEnemies", spawnRate - 0.5f);
        }
        else if (levelScript.bossOn == true && levelScript.levelId <=10)
        {
            Instantiate(bossList[Random.Range(0, 2)], new Vector2(0f, 7.5f), Quaternion.identity);
        }
        else if (levelScript.bossOn == true && levelScript.levelId > 10 && levelScript.levelId <= 20)
        {
            Instantiate(bossList2[Random.Range(0, 2)], new Vector2(0f, 7.5f), Quaternion.identity);
        }
        else if (levelScript.bossOn == true && levelScript.levelId == 25)
        {
            Instantiate(finalBoss, new Vector2(0f, 7.5f), Quaternion.identity);
        }
        else
        {
            Debug.Log("Enemies missing");
        }
        
    }
    void MainCameraRotation()
    {
       if (cameraRot<34)
       {
            cameraRot++;
            mainCamera.eulerAngles += new Vector3(mainCamera.transform.rotation.x-1f,0f,0f);
            Invoke("MainCameraRotation", 0.05f); 
       }
       else if(cameraRot>=34)
       {
            mainCamera.eulerAngles = new Vector3(-30f, 0f, 0f);
            CancelInvoke("MainCameraRotation");
       }
        
    }

    void SpawnBackers()
    {
        Instantiate(backsList[Random.Range(0, 2)], new Vector2(Random.Range(-3f, 3f), 0f), Quaternion.identity);
    }
}
