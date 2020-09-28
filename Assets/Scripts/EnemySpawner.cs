using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [RangeAttribute(0.1f, 120f)]
    float secondsBetweenSpawns = 3f;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] Text spawnedEnemies;
    int score;

    [SerializeField] AudioClip spawnedEnemySFX;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RepeatedlySpawnEnemies());
        spawnedEnemies.text = "Score: " + score.ToString();
    }

   IEnumerator RepeatedlySpawnEnemies()
    {
        while (true)    //forever
        {
            GetComponent<AudioSource>().PlayOneShot(spawnedEnemySFX);
            var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.transform.parent = GameObject.Find("Enemies").transform;
            AddScore();
            if(secondsBetweenSpawns > 0.75f) secondsBetweenSpawns -= 0.05f;
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }

    private void AddScore()
    {
        score++;
        spawnedEnemies.text = "Score: " + score.ToString();
    }
}
