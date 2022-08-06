using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAndBow : MonoBehaviour
{

    private Rigidbody myRigidBody;

    public float speed = 30;

    public float deactivate_Timer = 3f;

    public float damage = 50f;

    void Awake()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }
    
    




    void Start()
    {
        Invoke("DeactivateGameObject",deactivate_Timer);
        
    }

 
    void Update()
    {
        
    }

    public void launch(Camera mainCamera)
    {
        myRigidBody.velocity = mainCamera.transform.forward*speed;
        transform.LookAt(transform.position + myRigidBody.velocity);
    }








    void DeactivateGameObject()
    {
        if(gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider target) {

        // after we touch an enemy deactivate game object
        if(target.tag == Tags.ENEMY_TAG) {

            target.GetComponent<HealthScript>().ApplyDamage(damage);

            gameObject.SetActive(false);

        }

    }









}//class
