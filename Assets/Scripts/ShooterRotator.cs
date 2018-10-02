using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterRotator : MonoBehaviour {

	private enum RotateState
    {

        Idle,Vertical,Horizontal,Ready
    }
    private RotateState state = RotateState.Idle;

    public float verticalRotateSpeed = 360f;

    public float horizontalRotateSpeed = 360f;

    public BallShooter ballShooter;

    void Update()
    {
        switch(state)
        {
            case RotateState.Idle:

                if(Input.GetButtonDown("Fire1"))
            {
                    state = RotateState.Horizontal;
                }
                break;
            case RotateState.Horizontal:

                if (Input.GetButton("Fire1"))
                {
                    transform.Rotate(new Vector3(0, horizontalRotateSpeed * Time.deltaTime, 0));
                }
                else if (Input.GetButtonUp("Fire1"))
                {
                    state = RotateState.Vertical;
                }
                break;
            case RotateState.Vertical:

                if (Input.GetButton("Fire1"))
                {
                    transform.Rotate(new Vector3(-verticalRotateSpeed * Time.deltaTime, 0, 0));
                }
                else if (Input.GetButtonUp("Fire1"))
                {
                    state = RotateState.Ready;
                    ballShooter.enabled = true;
                }

                break;
            case RotateState.Ready:
            break;
        }
    }

    private void OnEnable()
    {
        transform.rotation = Quaternion.identity;

        state = RotateState.Idle;

        ballShooter.enabled = false;
    }
}


//if문으로 작성시
//ifで作成時
/*
if(state == RotateState.Idle)
{
    if(Input.GetButtonDown("Fire1"))
    {
        state = RotateState.Horizontal;
    }
}
else if(state == RotateState.Horizontal)
{
    if(Input.GetButton("Fire1"))
    {
        transform.Rotate(new Vector3(0, horizontalRotateSpeed * Time.deltaTime, 0));
    }else if(Input.GetButtonUp("Fire1"))
    {
        state = RotateState.Vertical;
    }
}
else if(state == RotateState.Vertical)
{
    if(Input.GetButton("Fire1"))
    {
        transform.Rotate(new Vector3(-verticalRotateSpeed * Time.deltaTime, 0, 0));
    }
    else if (Input.GetButtonUp("Fire1"))
    {
        state = RotateState.Ready;
    }
} */
