using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

public CharacterController character_Controller;

private Vector3 move_Direction;
public float speed=5f;
private float gravity=20f;

public float Jump_force =10f;
private float Vertical_velocity;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        
    }

    void MovePlayer()
    {
        move_Direction =new Vector3(Input.GetAxis(Axis.HORIZONTAL),0f, Input.GetAxis(Axis.VERTICAL));

        move_Direction=transform.TransformDirection(move_Direction);

        move_Direction *=speed * Time.deltaTime;

        Apply_gravity();

        character_Controller.Move(move_Direction);

    }

    void Apply_gravity()
    {
        
        Vertical_velocity -= gravity * Time.deltaTime;
        playerjump();
        move_Direction.y= Vertical_velocity * Time.deltaTime;
        
    
    }
    void playerjump()
    {
        if(character_Controller.isGrounded&& Input.GetKeyDown(KeyCode.Space))
        {
            Vertical_velocity = Jump_force;
        }
    }
}
