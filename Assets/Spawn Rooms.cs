using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRooms : MonoBehaviour
{
    public LayerMask WhatisRoom;
    public LevelGeneration LevelGen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // creates a collider to detect if there are any rooms at that position
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, WhatisRoom);
        // if there are no rooms at the position and the generation has stopped a random room is spawned there
        if (roomDetection == null && LevelGen.stopgeneration == true)
        {
            int rand = Random.Range(0, LevelGen.rooms.Length);
            Instantiate(LevelGen.rooms[rand],transform.position,Quaternion.identity);
            // spawnpoint is destroyed as it is no longer needed and would keep spawning rooms
            Destroy(gameObject);
        }
    }
}
