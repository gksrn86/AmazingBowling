using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public LayerMask whatIsProp;

    public ParticleSystem explosionParticle;

    public AudioSource explosionAudio;

    public float maxDamage = 100f;

    public float explosionForce = 1000f;

    public float lifeTime = 10f;

    public float explosionRadius = 20f;

    void Start()
    {
        Destroy(gameObject,lifeTime);    
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius,whatIsProp);

        for(int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            targetRigidbody.AddExplosionForce(explosionForce, transform.position ,explosionRadius);

            Prop targetProp = colliders[i].GetComponent<Prop>();

            float damage = CalculateDamage(colliders[i].transform.position);

            targetProp.TakeDamage(damage);

        }

        explosionParticle.transform.parent = null;

        explosionParticle.Play();
        explosionAudio.Play();


        Destroy(explosionParticle.gameObject,explosionParticle.duration);
        Destroy(gameObject);
    }
    public void OnDestroy()
    {
        GameManager.instance.OnBallDestroy();
    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;

        float distance = explosionToTarget.magnitude;

        float edgeToCenterDistance = explosionRadius - distance;  //distance 가 0이면 100퍼센트 데미지 1이면 0의 데미지

        float percentage = edgeToCenterDistance / explosionRadius;

        float damage = maxDamage * percentage;

        damage = Mathf.Max(0,damage);

        return damage;
    }
}
