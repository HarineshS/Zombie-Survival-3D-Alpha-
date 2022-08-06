using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [SerializeField]
    private GameObject boar_Prefab,cannibal_Prefab;

    public Transform[] cannibal_SpawnPoint,boar_SpawnPoint;

    [SerializeField]
    private int cannibal_EnemyCount,boar_EnemyCount;

    private int initial_Cannibal_Count, initial_Boar_Count;

    public float wait_Before_Spawn_Time= 10f;





    void Awake()
    {
        MakeInstance();
        
    }

    void Start() 
    {
        initial_Cannibal_Count = cannibal_EnemyCount;
        initial_Boar_Count= boar_EnemyCount;

        SpawnEnemies();

        StartCoroutine("CheckToSpawnEnemies");

        
    }

    void SpawnEnemies()
    {
        SpawnCannibals(); 
        SpawnBoars();

    }

    void SpawnCannibals()
    {
        int index=0;

        for (int i =0;i<cannibal_EnemyCount;i++)
        {
            if (index >= cannibal_SpawnPoint.Length) 
            {
                index = 0;
            }

            

            Instantiate(cannibal_Prefab, cannibal_SpawnPoint[index].position,Quaternion.identity);
            index ++;
        }
        cannibal_EnemyCount=0;

    }

    void SpawnBoars()
    {
        int index=0;

        for (int i =0;i<boar_EnemyCount;i++)
        {
            if (index >= boar_SpawnPoint.Length) 
            {
                index = 0;
            }

            

            Instantiate(boar_Prefab, boar_SpawnPoint[index].position,Quaternion.identity);
            index ++;
        }
        boar_EnemyCount=0;

    }

    IEnumerator CheckToSpawnEnemies()
    {
        yield return new WaitForSeconds(wait_Before_Spawn_Time);
        SpawnCannibals();
        SpawnBoars();
        StartCoroutine("CheckToSpawnEnemies");
    }

    public void EnemyDied(bool cannibal)
    {
        if(cannibal)
        {
            cannibal_EnemyCount++;
            if(cannibal_EnemyCount> initial_Cannibal_Count)
            {
                cannibal_EnemyCount=initial_Cannibal_Count;
            }
        }
        else
        {
            boar_EnemyCount++;
            if(boar_EnemyCount>initial_Boar_Count)
            {
                boar_EnemyCount=initial_Boar_Count;
            }

        }
    }
    




    void MakeInstance()
    {
        if(Instance == null)    
        {
            Instance = this;

        }
    }




    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopSpawning()
    {
        StopCoroutine("CheckToSpawnEnemies");
    }
}//class
