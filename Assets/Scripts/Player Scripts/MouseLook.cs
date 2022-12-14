using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private Transform playerRoot,loookroot;

    [SerializeField]
    private bool invert;

    [SerializeField]
    private bool can_Unlock = true;

    [SerializeField]
    private float sensitivity =5f;

    [SerializeField]
    private float smoothWeight=.4f;

    [SerializeField]
    private int smoothsteps=10;

    [SerializeField]
    private float rollAngle = 10f;

    [SerializeField]
    private float rollspeed = 3f;



    [SerializeField]
    private Vector2 default_look_limits = new Vector2(-70f,80f);

    private Vector2 look_Angles;

    private Vector2 current_mouse_look;
    private Vector2 smoothmove;

    private float current_rollangle;

    private int lastlookframe;





    // Start is called before the first frame update
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {

        lockandunlockcursor();

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            lookaround();
        }
        
    }


    void lockandunlockcursor()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible=false;
                
            }
        }
    }


    void lookaround()
    {
        current_mouse_look = new Vector2(Input.GetAxis(MouseAxis.MOUSE_Y), Input.GetAxis(MouseAxis.MOUSE_X));

        look_Angles.x += current_mouse_look.x * sensitivity * (invert ? 1f : -1f);
        look_Angles.y += current_mouse_look.y * sensitivity;


        look_Angles.x = Mathf.Clamp(look_Angles.x,default_look_limits.x,default_look_limits.y);

        current_rollangle = Mathf.Lerp(current_rollangle,Input.GetAxisRaw(MouseAxis.MOUSE_X)*rollAngle,Time.deltaTime*rollspeed);

        loookroot.localRotation= Quaternion.Euler(look_Angles.x,0f,current_rollangle);
        playerRoot.localRotation= Quaternion.Euler(0f,look_Angles.y,0f);
    }










}
