using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float movementPeriod = .75f;
    [SerializeField] ParticleSystem goalParticlePrefab;
    // Start is called before the first frame update
    void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(movementPeriod);
        }
        SelfDestruct();
    }

    private void SelfDestruct()
    {
        Transform deathFXPos = transform.Find("Body").transform;
        var deathFX = Instantiate(goalParticlePrefab, deathFXPos.position, Quaternion.identity);
        deathFX.gameObject.transform.parent = GameObject.Find("DeathFX").transform;
        deathFX.Play();

        float destroyDelay = deathFX.main.duration;

        Destroy(deathFX.gameObject, destroyDelay);
        Destroy(gameObject);
    }

}
