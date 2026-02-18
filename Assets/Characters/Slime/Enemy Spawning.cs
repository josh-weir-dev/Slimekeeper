using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawning : MonoBehaviour
{
    public GameObject objecttospawn;
    public Transform[] spawnpoints;
    private int randomspawnpoint;
    int maxenemies = 5;
    int enemycount = 0;
    bool onetime = false;
    public LevelGeneration level;

    // Start is called before the first frame update
    void Start()
    {
        GameObject Generator = GameObject.FindWithTag("Generation");
        level = level = Generator.GetComponent<LevelGeneration>();
    }

    void SpawnObject()
    {
        // ensuring that the max enemies had been spawned and extra are not
        if (enemycount < maxenemies)
        {
            // randomizes where the enemy spawns
            randomspawnpoint = Random.Range(0, spawnpoints.Length);
            // enemy spawns at the chosen spawn location
            GameObject newObject = Instantiate(objecttospawn, spawnpoints[randomspawnpoint]);
            // increments enemy count by 1
            enemycount++;
        }
 
    }

    // Update is called once per frame
    void Update()
    {
        if (!SceneManager.GetSceneByName("Loading Scene").isLoaded)
        {
            if (!onetime)
            {
                // adds delay to enemies being spawned
                InvokeRepeating("SpawnObject", 0, 3);
                onetime = true;
            }
        }
       
    }

}
