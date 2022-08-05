using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerfootsteps : MonoBehaviour
{

    private AudioSource footstep_sound;
    private CharacterController Character_Controller;

    [SerializeField]
    private AudioClip[] FootstepClip;

    public float volume_min,volume_max;

    private float accumulated_distance;

    [HideInInspector]
    public float step_distance;


    void Awake() 
    {
        footstep_sound = GetComponent<AudioSource>();

        Character_Controller = GetComponentInParent<CharacterController>();
        
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checktoplayfootstepsound();
        
    }

    void checktoplayfootstepsound()
    {
        if(!Character_Controller.isGrounded)
        return;

        if(Character_Controller.velocity.sqrMagnitude>0)
        {
            accumulated_distance += Time.deltaTime;

            if(accumulated_distance > step_distance)
            {
                footstep_sound.volume = Random.Range(volume_min,volume_max);
                footstep_sound.clip=FootstepClip[Random.Range(0,FootstepClip.Length)];
                footstep_sound.Play();

                accumulated_distance = 0f;

            }

        }
         else
        {
        accumulated_distance=0f;

        }
    }
   
}
