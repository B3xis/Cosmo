using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class ExplosionController : MonoBehaviour {

        [Tooltip("Child game objects that should be destroyed during explosion. For that 'DestroyPart(x)' will be called from animation clip.")]
        public GameObject[] removeParts;
        [Tooltip("Array of children that have animation for explosion and should explode by calling from parent animation clip.")]
        public ExplosionController[] childrenExplosion;


        private float speed = 0.0051553f, shootTime = 1.8f, bossCord;
        private int bulletsRate = 0;
        public int hp = 100, bossShootingRate;

        public GameObject bossBullet;
        public Transform shootDirection;
        public AudioSource enemyShootOgg;
        public AudioClip clip1;


        LevelScript levelScript;
        BulletsScript bulletsScript;
        Animator animator;
        // Use this for initialization
        void Start()
        {
            Transform levels = GameObject.FindGameObjectWithTag("EventSyst").GetComponent<Transform>();

            levelScript = levels.GetComponent<LevelScript>();

      
        animator = GetComponent<Animator>();
            levelScript.enemyDown = true;
            
            Invoke("Shoot",2f);
            if(levelScript.levelId > 25)
            {
                Moove();
            }   
           
            
        }
        void randPos()
        {
            bossCord = Random.Range(-1.5f, 1.8f);
            Invoke("randPos", 3f);
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
                    levelScript.levelId++;
                    PlayerPrefs.SetInt("EndLvl", levelScript.levelId);
                    StartExplosion();
                    
                }
            }


        }

        public void Moove()
        {

            if (gameObject.transform.position.y> 5.5f)
            {
                 transform.Translate(Vector2.up * -speed);
                 randPos();
                if (gameObject.transform.position.y <= 5.5f)
                    {
                        gameObject.transform.position = new Vector2(0f,5.5f);
                    }
            }
            else if(gameObject.transform.position.y == 5.5f)
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
        public void Shoot()
        {
            bossShootingRate = Random.Range(0,2);
            Invoke("BulletFrame", 0f);
            Invoke("Shoot", shootTime);
        }
        void BulletFrame()
        {
            if (bossShootingRate == 0 || bossShootingRate == 2)
            {

                Instantiate(bossBullet, new Vector3(shootDirection.position.x, shootDirection.position.y - 0.5f, 0), Quaternion.identity);

                enemyShootOgg.Play();
            }
            else if (bossShootingRate == 1)
            {
                    bulletsRate++;
                    Instantiate(bossBullet, new Vector3(shootDirection.position.x, shootDirection.position.y - 0.5f, 0), Quaternion.identity);

                    enemyShootOgg.PlayOneShot(clip1);

                    Invoke("BulletFrame", 0.2f);
                if (bulletsRate >= 5)
                {
                    bulletsRate = 0;
                    CancelInvoke("BulletFrame");
                }
               
                
            }
            
        }

        public void DestroyPart(int index)
        {
            if (removeParts != null && index >= 0 && index < removeParts.Length)
                Destroy(removeParts[index]);
            else
                Debug.LogWarning("Index is out of range in DestroPart. index: " + index);
        }

        public void StartExplosion()
        {
            if (animator == null)
                animator = GetComponent<Animator>();
            animator.SetBool("expl", true);
            Invoke("DestroyObject", 1.5f);

        }

        public void DestroyObject()
        {
            Destroy(gameObject);
            levelScript.StartNextLevelPanel();
        }

        public void DestroyParentObject()
        {
            Destroy(transform.parent.gameObject);
        }

        public void ChildExplosion(int index)
        {
            if (childrenExplosion != null && index >= 0 && index < childrenExplosion.Length)
                childrenExplosion[index].StartExplosion();
        }

        public void DestroyChildren()
        {
            if (removeParts != null && removeParts.Length > 0)
                foreach (GameObject child in removeParts)
                    Destroy(child);
        }

      
    }

