using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    private Animator ani;
    private Rigidbody2D rBody;

    public float shiftSpeedMultiplier = 3f; // Adjust this value to your desired speed increase
    private bool isShiftPressed = false;

    void Start()
    {
        ani=GetComponent<Animator>();
        rBody=GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

    // Check if Shift key is being pressed or released
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            isShiftPressed = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            isShiftPressed = false;
        }


        if(horizontal !=0)
        {
            ani.SetFloat("Horizontal",horizontal);
            ani.SetFloat("Vertical", 0);
        }

        if(vertical !=0)
        {
            ani.SetFloat("Horizontal", 0);
            ani.SetFloat("Vertical",vertical);
        }
        Vector2 dir = new Vector2(horizontal,vertical);
        ani.SetFloat("Speed",dir.magnitude);

        float speedMultiplier = isShiftPressed ? shiftSpeedMultiplier : 1f;

        rBody.velocity = dir * 2f * speedMultiplier;
    }
}
