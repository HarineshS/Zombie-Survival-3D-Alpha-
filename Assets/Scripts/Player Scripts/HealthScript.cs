using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{

    private EnemyAnimator enemyAnim;
    private NavMeshAgent navAgent;
    private EnemyController enemy_Controller;

    public float health = 100f;

    public bool is_Player,is_Boar,is_Cannibal;

    private bool is_Dead;

    private EnemyAudio enemyAudio; 

    private PlayerStats player_Stats;


    void Awake()
    {

        if(is_Boar || is_Cannibal)
        {
            enemyAnim = GetComponent<EnemyAnimator>();
            enemy_Controller = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();

            enemyAudio = GetComponentInChildren<EnemyAudio>();
        }

        if(is_Player)
        {
            player_Stats= GetComponent<PlayerStats>();

        }
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void ApplyDamage(float damage)
    {
        if(is_Dead)
        return;

        health-= damage;

        if(is_Player) //show stats UI
        {

            player_Stats.Display_HealthStats(health);

        }

        if(is_Boar || is_Cannibal)
        {
            if(enemy_Controller.Enemy_State == EnemyState.PATROL)
            {
                enemy_Controller.chase_Distance=50f;
            }
        }
        if(health<=0)
            {
                PlayerDied();
                is_Dead=true;
              
            }







    }

    void PlayerDied()
    {
        if(is_Cannibal)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger= false;
            //GetComponent<Rigidbody>().enabled=true;
            GetComponent<Rigidbody>().AddTorque(-transform.forward*50f);
            GetComponent<Rigidbody>().useGravity = true;

            enemy_Controller.enabled=false;
            navAgent.enabled=false;
            enemyAnim.enabled=false;

            //StartCoroutine\
            StartCoroutine(DeadSound());
            

            //EnemyManager spawn more enemies
            EnemyManager.Instance.EnemyDied(true);


        }

        if(is_Boar)
        {
            navAgent.velocity=Vector3.zero;
            navAgent.isStopped=true;
            enemy_Controller.enabled=false;

            enemyAnim.Dead();

            StartCoroutine(DeadSound());

        }

        if(is_Player)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);

            for(int i=0; i< enemies.Length;i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled=false;
            }

            GetComponent<PlayerMovement>().enabled=false;
            GetComponent<PlayerAttack>().enabled=false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);

            EnemyManager.Instance.StopSpawning();
            
        }

        if(tag == Tags.PLAYER_TAG)
        {
            Invoke("RestartGame",3f);

        }
        else
        {
            Invoke("TurnOffGameObject",3f);
        }





    } //playerDied

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");

    }

    void TurnOffGameObject()
    {
        gameObject.SetActive(false);

    }

    
    IEnumerator DeadSound()
    {
        yield return new WaitForSeconds(0.3f);
        enemyAudio.Play_DeadSound();
    }







}//class
