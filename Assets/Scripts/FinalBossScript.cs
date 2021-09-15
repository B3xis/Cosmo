using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossScript : MonoBehaviour
{
    private float speed = 0.0051553f, bossCord;
    public GameObject boss1, boss2;
    private bool bossQueue = false,fstStage = false;
    // Start is called before the first frame update
    void Start()
    {
        Moove();
        Invoke("RotationBosses",5f);
    }

   
    void randPos()
    {
        bossCord = Random.Range(-1.5f, 1.8f);
        Invoke("randPos", 3f);
    }
    void RotationBosses()
    {
        if (bossQueue == false)
        {   

            if (boss1.transform.localPosition.y <= 0 && boss1.transform.localPosition.y > -2 && fstStage == false)
            {
                boss1.transform.Translate(Vector2.down * speed);
                Invoke("RotationBosses", 0f);
            }
            else if(boss1.transform.localPosition.y <= -2 && boss1.transform.localPosition.x < 1)
            {
                boss1.transform.Translate(Vector2.right * speed);
                boss2.transform.Translate(Vector2.right * -speed);
                fstStage = true;
                Invoke("RotationBosses", 0f);
            }
            else if(fstStage == true && boss1.transform.localPosition.y < 0)
            {
               
                    boss1.transform.Translate(Vector2.up * speed);
                    Invoke("RotationBosses", 0f);

            }
            else
            {
               
                    bossQueue = true;
                    fstStage = false;
                    Invoke("RotationBosses", 5f);
                
                
            }
           

        }
        else if(bossQueue == true)
        {
            if (boss2.transform.localPosition.y <= 0 && boss2.transform.localPosition.y > -2 && fstStage == false)
            {
                boss2.transform.Translate(Vector2.down * speed);
                Invoke("RotationBosses", 0f);
            }
            else if (boss2.transform.localPosition.y <= -2 && boss2.transform.localPosition.x < 1)
            {
                boss2.transform.Translate(Vector2.right * speed);
                boss1.transform.Translate(Vector2.right * -speed);
                fstStage = true;
                Invoke("RotationBosses", 0f);
            }
            else if (fstStage == true && boss2.transform.localPosition.y < 0)
            {

                boss2.transform.Translate(Vector2.up * speed);
                Invoke("RotationBosses", 0f);

            }
            else
            {
                    fstStage = false;
                    bossQueue = false;
                    Invoke("RotationBosses", 5f);
                

            }

        }
        
    }
    public void Moove()
    {

        if (gameObject.transform.position.y > 5.5f)
        {
            transform.Translate(Vector2.up * -speed);
            randPos();
            if (gameObject.transform.position.y <= 5.5f)
            {
                gameObject.transform.position = new Vector2(0f, 5.5f);
            }
        }
        else if (gameObject.transform.position.y == 5.5f)
        {
            if (gameObject.transform.position.x > bossCord)
            {
                transform.Translate(Vector2.right * -speed);
            }
            else if (gameObject.transform.position.x < bossCord)
            {
                transform.Translate(Vector2.right * speed);
            }

        }

        Invoke("Moove", 0f);
    }
}
