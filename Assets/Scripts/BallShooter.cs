using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallShooter : MonoBehaviour {

    public CamFollow cam;

    public Rigidbody ball;

    public Transform firePos;

    public Slider powerSlider;

    public AudioSource shootingAudio;

    public AudioClip fireClip;

    public AudioClip chargingClip;

    public float minForce = 15f;

    public float maxForce = 30f;

    public float chargingTime = 0.75f;

    private float currentForce;

    private float chargingSpeed;

    private bool fired;

    private void OnEnable()
    {
        currentForce = minForce;

        powerSlider.value = minForce;

        fired = false;


    }

    private void Start()
    {
        chargingSpeed = (maxForce - minForce) / chargingTime;


    }

    private void Update()
    {
        if(fired == true)
        {
            return;  // 한번 발사되면 안되도록 지정
        }

        powerSlider.value = minForce;

        if(currentForce >= maxForce && !fired)
        {
            currentForce = maxForce;
            Fire();
            //발사처리
        }
        else if(Input.GetButtonDown("Fire1"))
        {
            //연사하고 싶으면
            fired = false;
            
            currentForce = minForce;

            shootingAudio.clip = chargingClip;

            shootingAudio.Play();
            //발사버튼을 누르는 순간

        }
        else if(Input.GetButton("Fire1") && !fired)
        {
            currentForce = currentForce + chargingSpeed * Time.deltaTime;

            powerSlider.value = currentForce;
            //발사버튼을 누르고 있는 동안
        }
        else if(Input.GetButtonUp("Fire1") && !fired)
        {
            //발사버튼 손을 땔때
            Fire();
            // 발사처리
        }
    }

    private void Fire()
    {
        fired = true;

        Rigidbody ballInstance = Instantiate(ball, firePos.position, firePos.rotation);

        ballInstance.velocity = currentForce * firePos.forward; //힘 * 방향 속도지정

        shootingAudio.clip = fireClip;

        shootingAudio.Play();

        currentForce = minForce;

        cam.SetTarget(ballInstance.transform, CamFollow.State.Tracking);
    }
}
