using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager weapon_Manager;

    public float firerate =15f;
    private float nextTimeToFire;
    public float damage=20f;

    private Animator zoomCameraAnim;
    private bool zoomed;
    private Camera mainCam;
    private GameObject crosshair;
    private bool is_Aiming;

    [SerializeField]
    private GameObject arrow_prefab,spear_prefab;

    [SerializeField]
    private Transform arrow_bow_StartPosition;




    void Awake()
    {
        weapon_Manager = GetComponent<WeaponManager>();

        zoomCameraAnim = transform.Find(Tags.LOOK_ROOT).transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();

        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);

        mainCam = Camera.main;





    }

    void Start()
    {
        
    }

   
    void Update()
    {
        WeaponShoot();
        ZoomInAndOut();
        
    }

    void WeaponShoot()
    {
        if(weapon_Manager.GetCurrentSelectedWeapon().fireType== WeaponFireType.MULTIPLE) //AR 
        {
            if(Input.GetMouseButton(0) && Time.time>nextTimeToFire) //press and hold left mouse button & time > nextTimeToFire
            {
                nextTimeToFire = Time.time + 1f/firerate;
                weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                BulletFired();
            }

        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(weapon_Manager.GetCurrentSelectedWeapon().tag == Tags.AXE_TAG)
                {
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();

                }
                if(weapon_Manager.GetCurrentSelectedWeapon().bulletType== WeaponBulletType.BULLET)
                {
                weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                BulletFired();

                }
                else
                {
                    //arrow or spear

                    if(is_Aiming)
                    {
                        weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();

                        if(weapon_Manager.GetCurrentSelectedWeapon().bulletType== WeaponBulletType.ARROW)
                        {
                            //throw arrow
                            ThrowArrowOrSpear(true);
                            

                        }
                        else if(weapon_Manager.GetCurrentSelectedWeapon().bulletType== WeaponBulletType.SPEAR)
                        {
                            //throw spear
                            ThrowArrowOrSpear(false);
                        }
                    }
                }
            }
            
            
        }
    }

    void ZoomInAndOut()
    {
        if(weapon_Manager.GetCurrentSelectedWeapon().weapon_aim==WeaponAim.AIM)
        {
            if(Input.GetMouseButtonDown(1))
            {
                zoomCameraAnim.Play(AnimationTags.ZOOM_IN_ANIM);

                crosshair.SetActive(false);

            }

            if(Input.GetMouseButtonUp(1))
            {
                zoomCameraAnim.Play(AnimationTags.ZOOM_OUT_ANIM);

                crosshair.SetActive(true);

            }


        }


        if(weapon_Manager.GetCurrentSelectedWeapon().weapon_aim==WeaponAim.SELF_AIM)
        {
            if(Input.GetMouseButtonDown(1))
            {
                 weapon_Manager.GetCurrentSelectedWeapon().Aim(true);
                is_Aiming = true;


            }
            if(Input.GetMouseButtonUp(1))
            {
                 weapon_Manager.GetCurrentSelectedWeapon().Aim(false);
                is_Aiming = false;


            }

           
        }
    


    } //zoom

    void ThrowArrowOrSpear(bool ThrowArrow)
    {
        if(ThrowArrow)
        {
            GameObject arrow = Instantiate(arrow_prefab);
            arrow.transform.position = arrow_bow_StartPosition.position;

            arrow.GetComponent<ArrowAndBow>().launch(mainCam);
        }
        else
        {
            GameObject spear = Instantiate(spear_prefab);
            spear.transform.position = arrow_bow_StartPosition.position;

            spear.GetComponent<ArrowAndBow>().launch(mainCam);

        }

    }

    void BulletFired()
    {
        RaycastHit hit;
        if(Physics.Raycast(mainCam.transform.position,mainCam.transform.forward,out hit))
        {
            print("WE HIT:"+ hit.transform.gameObject.name);

            if(hit.transform.tag == Tags.ENEMY_TAG)
            {
                hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
            }
        }
    }







}//class 
