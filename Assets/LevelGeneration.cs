using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingpositions;
    public GameObject[] rooms;
    public GameObject spawnObject;
    public int randStartpos;
    public int direction;
    public float moveAmountX;
    public float moveAmountY;
    public float timeBetweenRoom;
    public float startTimeBetweenRoom = 0.25f;
    public float minX;
    public float maxX;
    public float minY;
    public bool stopgeneration;
    public LayerMask room;
    private int downcounter;
    // Start is called before the first frame update
    void Start()
    {
        // setting the starting position to a random position on the top row of my grid
        randStartpos = Random.Range(0, startingpositions.Length);
        transform.position = startingpositions[randStartpos].position;
        // spawns room at the start point
        Instantiate(rooms[0],transform.position,Quaternion.identity);
        // generates a random integer used in the move function to determine which was the generation will move
        direction = Random.Range(1, 6);
        if (direction == 5)
        {
            downcounter++;
        }
    }

    private void Update()
    {
        if (timeBetweenRoom<=0 && stopgeneration == false)
        {
            // the move function is called when the time reaches zero
            Move();
            // time is reset back to the starting time
            timeBetweenRoom = startTimeBetweenRoom;
        }
        else
        {
            // decreasing time as time passes
            timeBetweenRoom -= Time.deltaTime;
        }
    }

    private void Move()
    {
        if (direction == 1 || direction == 2)
        {
            // resets the downcounter to zero as there are no longer consecutive down paths
            downcounter = 0;
            // checking the generation can move to the right
            if (transform.position.x < maxX)
            {
                // if the direction is 1 or 2 the generation will move right
                Vector2 newPos = new Vector2(transform.position.x + moveAmountX, transform.position.y);
                transform.position = newPos;
                // generates a random room from all room types as all have left and right opening
                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
                // randomises the direction and changes 3 and 4 so the generation cannot move left and overwrite previous generation
                direction = Random.Range(1, 6);
                if (direction == 3)
                {
                    direction = 2;
                }
                else if (direction == 4)
                {
                    direction = 5;
                }
            }
            else
            {
                // if max X is reached and generation cannot move right it moves down
                direction = 5;
            }
            
        }
        else if (direction == 3||direction == 4)
        {
            // resets down counter to zero as there are no longer consecutive down paths
            downcounter = 0;
            // checks generation can move to the left
            if (transform.position.x > minX)
            {
                // if the direction is 3 or 4 the generation will move left
                Vector2 newPos = new Vector2(transform.position.x - moveAmountX, transform.position.y);
                transform.position = newPos;
                // generates a random room from all room types as all have left and right opening
                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
                // direction can only be 3-5 as 1 and 2 would make it move right and overwrite previous generation
                direction = Random.Range(3, 6);
             
            }
            else
            {
                // if generation cannot move left it moves down
                direction = 5;
            }
            
        }
        else if (direction == 5)
        {
            // increments a counter that keeps track of how many times the generation has gone down
            downcounter++;
            // checks the generation can move down
            if (transform.position.y > minY)
            {
                // this creates a collider at the position of the generation to determine which rooms are surrounding the one about to be generated
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                // this if statement checks that the room above the one about to be created has a bottom opening
                if(roomDetection.GetComponent<RoomType>().type !=1 && roomDetection.GetComponent<RoomType>().type != 3)
                {
                    // checking the downcounter to see if consecutive down paths have happened
                    if (downcounter >= 2)
                    {
                        // destroys the current room above where the generation is and replaces it with a room with all 4 openings
                        roomDetection.GetComponent<RoomType>().roomdestruction();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    }
                    else {
                        // this calls the destruction method if the room above does not have a bottom opening 
                        roomDetection.GetComponent<RoomType>().roomdestruction();
                        // this spawns a new room in replacement with a bottom opening
                        int randBottomRoom = Random.Range(1, 4);
                        if (randBottomRoom == 2)
                        {
                            randBottomRoom = 1;
                        }
                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                    }
                    
                }

                // if the direction is 5 the generation will move down
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmountY);
                transform.position = newPos;
                // generates a random room from 2-3 as these are the only rooms with a top opening
                int rand = Random.Range(2, 4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
                // if the generation has moved down the next direction can be anything so a random direction is generated with all options
                direction = Random.Range(1, 6);
            }
            else
            {
                // if the bottom has been reached and the program tries to move down again 
                stopgeneration = true;
                // spawns the boss in the centre of the last room
                GameObject newObject = Instantiate(spawnObject, new Vector3(transform.position.x -2, transform.position.y +1, transform.position.z), Quaternion.identity);
            }
        }
        
    }
}
