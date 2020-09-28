using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] float attackRange = 10f;
    [SerializeField] GameObject bullets;
    AudioSource audioSource;
    SceneLoader sceneLoader;

    public Waypoint baseWaypoint;
    Transform targetEnemy;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sceneLoader = FindObjectOfType<Canvas>().GetComponentInChildren<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneLoader.IsPlaying())
        {
            SetTargetEnemy();
            FireAtEnemy();
        }
    }

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if (sceneEnemies.Length == 0) { return; }

        Transform closestEnemy = sceneEnemies[0].transform;

        foreach(EnemyDamage testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }

        targetEnemy = closestEnemy;
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        float distToA = Vector3.Distance(transform.position, transformA.position);
        float distToB = Vector3.Distance(transform.position, transformB.position);
        if (distToB < distToA) { return transformB; }
        else return transformA;
    }

    private void FireAtEnemy()
    {
        if (targetEnemy)
        {
            objectToPan.LookAt(targetEnemy.Find("Body"));
            float distanceToEnemy = Vector3.Distance(targetEnemy.position, gameObject.transform.position);
            //print("distanceToEnemy " + distanceToEnemy);
            if (distanceToEnemy <= attackRange)
            {
                Shoot(true);
                //print("Shooting because" + distanceToEnemy + " < " + attackRange);
            }
            else
            {
                Shoot(false);
            }
        }
        else
        {
            Shoot(false);
        }
    }

    private void Shoot(bool isActive)
    {
        if (isActive)
        {
            if(!audioSource.isPlaying)
            audioSource.Play();
        }
        else audioSource.Stop();
        var emissionModule = bullets.GetComponent<ParticleSystem>().emission;
        emissionModule.enabled = isActive;

    }
}
