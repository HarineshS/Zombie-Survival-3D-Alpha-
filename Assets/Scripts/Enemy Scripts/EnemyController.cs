﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour
{

    private EnemyAnimator enemy_Anim;
    private NavMeshAgent navAgent;

    private EnemyState enemy_State;

    public float walk_Speed =0.5f;
    public float run_Speed =4f;

    public float chase_Distance = 7f;
    private float current_ChaseDistance;
    public float attack_Distance =1.8f;
    public float chase_After_Attack_Distance =2f;

    public float patrol_Radius_Min = 20f, patrol_Radius_Max = 60f;
    public float patrol_For_This_Time = 15f;
    public float patrol_Timer;

    private Transform target;

    public float wait_Before_Attack = 2f;
    private float attack_Timer;

    public GameObject attackPoint; //for boar attack point

    private EnemyAudio enemy_Audio;








    void Awake()
    {
        enemy_Anim = GetComponent<EnemyAnimator>();
        navAgent = GetComponent<NavMeshAgent>();

        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
        enemy_Audio = GetComponentInChildren<EnemyAudio>();

    }









    void Start()
    {
        enemy_State = EnemyState.PATROL;

        patrol_Timer = patrol_For_This_Time;

        attack_Timer = wait_Before_Attack;

        current_ChaseDistance = chase_Distance; 
        
    }


    void Update()
    {

        if(enemy_State == EnemyState.PATROL)
        {
            Patrol();

        }
        if(enemy_State == EnemyState.CHASE)
        {
            Chase();

        }
        if(enemy_State == EnemyState.ATTACK)
        {
            Attack();

        }






        
    }



    void SetNewRandomDestination()    //************************************************************Ye Nahi Samjha ******************************************************************
    {
        float rand_Radius = Random.Range(patrol_Radius_Min, patrol_Radius_Max);

        Vector3 ranDir = Random.insideUnitSphere* rand_Radius;
        ranDir += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(ranDir,out navHit, rand_Radius,-1);
        navAgent.SetDestination(navHit.position);









    }

    void Patrol()
    {

        navAgent.isStopped = false;
        navAgent.speed = walk_Speed;

        patrol_Timer += Time.deltaTime;

        if(patrol_Timer > patrol_For_This_Time)
        {
            SetNewRandomDestination();

            patrol_Timer= 0f;
        }

        if(navAgent.velocity.sqrMagnitude > 0)
        {
            enemy_Anim.Walk(true);
        }
        else
        {
            enemy_Anim.Walk(false);
        }
        //test distance b/w player and enemy

        if(Vector3.Distance(transform.position,target.position)<= chase_Distance)
        {

            enemy_Anim.Walk(false);
            enemy_State = EnemyState.CHASE;

            //player spotted audio

            enemy_Audio.Play_ScreamSound();

            

        }

         




    }
    void Chase()
    {
        navAgent.isStopped= false;
        navAgent.speed= run_Speed;

        navAgent.SetDestination(target.position);

         if(navAgent.velocity.sqrMagnitude > 0)
        {
            enemy_Anim.Run(true);
        }
        else
        {
            enemy_Anim.Run(false);
        }

        if(Vector3.Distance(transform.position,target.position)<= attack_Distance)
        {
            enemy_Anim.Run(false);
            enemy_Anim.Walk(false);
            enemy_State = EnemyState.ATTACK;

            if(chase_Distance != current_ChaseDistance)
            {
                chase_Distance = current_ChaseDistance;
            }


        }
        else if(Vector3.Distance(transform.position,target.position) > chase_Distance)
        {
            //player run away from enemy
            //stop running
            enemy_Anim.Run(false);
            enemy_State = EnemyState.PATROL;
            //reset patrol timer
            patrol_Timer= patrol_For_This_Time;

            if(chase_Distance != current_ChaseDistance)
            {
                chase_Distance= current_ChaseDistance;
            }

            









        }







    }

    void Attack()
    {

        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        attack_Timer += Time.deltaTime;

        if(attack_Timer> wait_Before_Attack)
        {
            enemy_Anim.Attack();
            attack_Timer = 0f;

            //play attack sound

            enemy_Audio.Play_AttackSound();
        }

        if (Vector3.Distance(transform.position,target.position)> attack_Distance + chase_After_Attack_Distance)  //try removing chase_after_Attack_Distance
        {
            enemy_State = EnemyState.CHASE;
 


        }
     

    }
    void Turn_On_AttackPoint()
    {
        attackPoint.SetActive(true);
    }
    void Turn_Off_AttackPoint()
    {
       if( attackPoint.activeInHierarchy)
       {
        attackPoint.SetActive(false);

       }
    }


    public EnemyState Enemy_State
    {
        get;set;
    }













}
