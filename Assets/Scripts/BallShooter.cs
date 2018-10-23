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
            return;  // 一度発射されてはならないように指定 
                     
        }

        powerSlider.value = minForce;

        if(currentForce >= maxForce && !fired)
        {
            currentForce = maxForce;
            Fire();
            //発射処理
        }
        else if(Input.GetButtonDown("Fire1"))
        {
            //連射したければ
            fired = false;
            
            currentForce = minForce;

            shootingAudio.clip = chargingClip;

            shootingAudio.Play();
            //発射ボタンを押す瞬間

        }
        else if(Input.GetButton("Fire1") && !fired)
        {
            currentForce = currentForce + chargingSpeed * Time.deltaTime;

            powerSlider.value = currentForce;
            //発射ボタンを押している間
        }
        else if(Input.GetButtonUp("Fire1") && !fired)
        {
            //発射ボタンの手を出す時
            Fire();
            // 発射処理
        }
    }

    private void Fire()
    {
        fired = true;

        Rigidbody ballInstance = Instantiate(ball, firePos.position, firePos.rotation);

        ballInstance.velocity = currentForce * firePos.forward; //力 * 方向 = 速度 指定

        shootingAudio.clip = fireClip;

        shootingAudio.Play();

        currentForce = minForce;

        cam.SetTarget(ballInstance.transform, CamFollow.State.Tracking);
    }
}
