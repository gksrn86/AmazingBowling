using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour {

	public enum State
    {
        Idle, Ready, Tracking
    }

    private State state
    {
        set
        {
            switch(value)
            {
                case State.Idle:
                    targetZoomSize = roundReadyZoomSize;
                    break;
                case State.Ready:
                    targetZoomSize = readyShootZoomSize;
                    break;
                case State.Tracking:
                    targetZoomSize = trackingZoomSize;
                    break;
            }
        }
    }

    private Transform target;

    public float smoothTime = 0.2f;

    private Vector3 LastmovingVelocity;
    private Vector3 targetPosition;

    private Camera cam;
    private float targetZoomSize = 5f;

    private const float roundReadyZoomSize = 14.5f;
    private float readyShootZoomSize = 5f;
    private float trackingZoomSize = 10f;

    private float LastZoomSpeed;
   
    void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        state = State.Idle; 
    }

    private void Move()
    {
        targetPosition = target.transform.position;

        Vector3 smothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref LastmovingVelocity, smoothTime);

        transform.position = targetPosition;
    }

    private void Zoom()
    {
        float smoothZoomSize = Mathf.SmoothDamp(cam.orthographicSize,targetZoomSize,ref LastZoomSpeed,smoothTime);

        cam.orthographicSize = smoothZoomSize;
    }

    private void FixedUpdate()
    {
        if(target != null)
        {
            Move();
            Zoom();
        }
    }

    public void Reset()
    {
        state = State.Idle;
    }

    public void SetTarget(Transform newTarget, State newState)
    {
        target = newTarget;
        state = newState;
    }
}
