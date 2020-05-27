using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Control : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sp;
    private Animator anim;

    [SerializeField]
    public GameObject preInteractObj;

    [SerializeField]
    private float pc_Mov_Spd = 1f;

    private Cam_Follow cf;
    private PC_Stats ps;

    private bool ableToMove = true;

    private void Awake()
    {
        #region Ref. Definition
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        cf = FindObjectOfType<Cam_Follow>();
        ps = FindObjectOfType<PC_Stats>();
        #endregion
    }
    private void Update()
    {
        pc_Movement();
        pc_Interact();
    }

    #region Trigger Events
    private void OnTriggerEnter2D(Collider2D collision)
    {
        pc_preInteractObjSet(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        pc_preInteractObjSet(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pc_preInteractObjUnset();
    }
    #endregion

    private void pc_Movement()
    {
        float spdAdjust = 1;
        if(ps.GetCurStamina() >= 0.8f * ps.GetMaxStamina())
        {
            spdAdjust = 1.2f;
        }else if(ps.GetCurStamina() >= 0.3f * ps.GetMaxStamina() && ps.GetCurStamina() < 0.8f * ps.GetMaxStamina())
        {
            spdAdjust = 1.0f;
        }else if(ps.GetCurStamina() < 0.3f * ps.GetMaxStamina())
        {
            spdAdjust = 0.7f;
        }

        float input_X = Input.GetAxis("Horizontal") * pc_Mov_Spd * spdAdjust;
        if(ps.GetCurStamina() > 0)
        {
            if(ableToMove)
            {
                pc_FlipSprite(input_X);
                pc_PlayAnimWalk(input_X);

                rb.MovePosition((Vector2)transform.position + new Vector2(input_X, 0) * Time.deltaTime);
            }
        }
    }

    private void pc_PlayAnimWalk(float x)
    {
        if(x > 0.2 || x < -0.2)
        {
            anim.SetBool("PC_walking", true);
        }
        else
        {
            anim.SetBool("PC_walking", false);
        }
    }

    private void pc_FlipSprite(float x)
    {
        if(x > 0)
        {
            sp.flipX = false;
            cf.CamToRight(true);
        }else if(x < 0)
        {
            sp.flipX = true;
            cf.CamToRight(false);
        }
    }

    private void pc_preInteractObjSet(Collider2D col)
    {
        if(preInteractObj == null)
        {
            preInteractObj = col.gameObject;
        }
        else
        {
            if(preInteractObj != col.gameObject)
            {
                preInteractObj = col.gameObject;
            }
        }
    }

    private void pc_preInteractObjUnset()
    {
        if(preInteractObj != null)
        {
            preInteractObj = null;
        }
    }

    private void pc_Interact()
    {
        if(preInteractObj != null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if(FindObjectOfType<PC_Stats>().GetCurStamina() > 0)
                {
                    //Open container
                    if (preInteractObj.GetComponent<Res_Con>())
                    {
                        if (!preInteractObj.GetComponent<Res_Con>().GetIfOpened())
                            ps.InteractionCost(5);
                        if (!preInteractObj.GetComponent<Res_Con>().isOpened)
                        {
                            preInteractObj.GetComponent<Res_Con>().Open_Con();
                        }
                    }

                    //Teleport
                    if (preInteractObj.GetComponent<Teleporter>())
                    {
                        ps.InteractionCost(2.5f);
                        preInteractObj.GetComponent<Teleporter>().TP();
                    }
                }
            }
        }
    }

    public void SetAbleToMove(bool b)
    {
        ableToMove = b;
    }
}
