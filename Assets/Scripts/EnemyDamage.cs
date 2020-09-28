using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int hitPoints = 10;
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem deathParticlePrefab;
    [SerializeField] AudioClip enemyHitSFX;


    private void OnParticleCollision(GameObject other)
    {
        print("hit");
        ProcessHit();
        if (hitPoints<= 0)
        {
            KillEnemy();
        }
    }

    void ProcessHit()
    {
        hitPoints -= 1;
        hitParticlePrefab.Play();
        GetComponent<AudioSource>().PlayOneShot(enemyHitSFX);
    }

    private void KillEnemy()
    {
        Transform deathFXPos = transform.Find("Body").transform;
        var deathFX = Instantiate(deathParticlePrefab, deathFXPos.position, Quaternion.identity);
        deathFX.gameObject.transform.parent = GameObject.Find("DeathFX").transform;
        deathFX.Play();

        float destroyDelay = deathFX.main.duration;

        Destroy(deathFX.gameObject, destroyDelay);
        Destroy(gameObject);

    }

}
