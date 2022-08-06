using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public float SprintSpeed =10f;
    public float MoveSpeed =5f;
    public float CrouchSpeed =2f;

    private Transform look_Root;
    private float stand_Height = 1.6f;
    private float crouch_Height =1; 

    private bool is_crouching;






    private playerfootsteps player_footsteps;
    private float sprintvolume = 1f;
    private float crouchvolume = .1f;
    private float walkvolmin=0.2f,walkvolmax=0.6f;

    private float walk_step_distance =0.4f;
    private float sprint_step_distance =0.25f;
    private float crouch_step_distance =0.5f;

    private PlayerStats player_Stats;

    private float sprint_Value = 100f;

    public float sprint_Threshold = 20f;






    void Awake() 
    {
        playerMovement = GetComponent<PlayerMovement>();

        look_Root = transform.GetChild(0);


        player_footsteps = GetComponentInChildren<playerfootsteps>();

        player_Stats = GetComponent<PlayerStats>();

        
    }






    // Start is called before the first frame update
    void Start()
    {
        player_footsteps.volume_min =walkvolmin;
        player_footsteps.volume_max =walkvolmax;
        player_footsteps.step_distance =walk_step_distance;
    }

    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();





        
    }

    void Sprint()
    {
       if(sprint_Value>0f)
       {
         if(Input.GetKeyDown(KeyCode.LeftShift) && !is_crouching)
        {
            playerMovement.speed=SprintSpeed;

            player_footsteps.step_distance=sprint_step_distance;
            player_footsteps.volume_min=sprintvolume;
            player_footsteps.volume_max=sprintvolume;
        }
       }

        if(Input.GetKeyUp(KeyCode.LeftShift) && !is_crouching)
        {
            playerMovement.speed=MoveSpeed;

            player_footsteps.step_distance=walk_step_distance;
            player_footsteps.volume_min=walkvolmin;
            player_footsteps.volume_max=walkvolmax;
        }

        if(Input.GetKey(KeyCode.LeftShift) && !is_crouching ) // ye kar raha tha
        {
            print("Goes Here!");
            
            sprint_Value-=sprint_Threshold*Time.deltaTime;
            

            if(sprint_Value <= 0f)
            {
                //sprint_Value-=sprint_Threshold*Time.deltaTime;


                //print("Goes Here!");
                playerMovement.speed=MoveSpeed;
                player_footsteps.step_distance=walk_step_distance;
                player_footsteps.volume_min=walkvolmin;
                player_footsteps.volume_max=walkvolmax;


            }
            player_Stats.Display_StaminaStats(sprint_Value);
        }
        


        else
        {
            if(sprint_Value !=100f)
            {
                sprint_Value += (sprint_Threshold/2f)*Time.deltaTime;

                player_Stats.Display_StaminaStats(sprint_Value);

                if(sprint_Value>100f)
                {
                    sprint_Value=100f;
                }


            }

        }
        

    }

    void Crouch()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            if(is_crouching)
            {
                look_Root.localPosition=new Vector3(0f, stand_Height,0f);
                playerMovement.speed=MoveSpeed;
                is_crouching=false;

                player_footsteps.step_distance=walk_step_distance;
                player_footsteps.volume_min=walkvolmin;
                player_footsteps.volume_max=walkvolmax;
                

            }
            else
            {
                look_Root.localPosition=new Vector3(0f, crouch_Height,0f);
                playerMovement.speed=CrouchSpeed;
                is_crouching=true;

                player_footsteps.step_distance=crouch_step_distance;
                player_footsteps.volume_min=crouchvolume;
                player_footsteps.volume_max=crouchvolume;

            }

        }
    }










}
