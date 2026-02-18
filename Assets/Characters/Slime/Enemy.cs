using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // this line declares the animator variable
    
    public Animator animator;
    public Rigidbody2D rb;
    bool isAlive = true;
    public float damage = 3;
    public DetectionZone detectionZone;
    public float moveSpeed = 500f;
    
    
    // Here i create a public variable for the enemy's health
    public float Health{
        set
        {

            if (value < health)
            {
                animator.SetTrigger("hit");
            }

            // the health is being set to the correct value
            health = value;

            
            if (health <= 0)
            {
                // if the health is at or below zero then the defeated function is called
          
                defeated();

            }
        }
        get
        {
            // returns the health of the enemy
           return health;
        }
    }

    // starting enemy health
    public float health = 1;

  public void Start()
    {
        //initialising needed variables
        animator = GetComponent<Animator>();
        animator.SetBool("isAlive", isAlive);
        rb = GetComponent<Rigidbody2D>();


    }

    void FixedUpdate()
    {
        // here we are checking that the list of detected objects in not empty
        if (detectionZone.detectedObjs.Count > 0)
        {
            // if the list is not empty the slime is moving and moves in the direction of the player
            animator.SetBool("isMoving", true);
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;
            rb.AddForce(direction * moveSpeed * Time.deltaTime);

        }
        else
        {
            // if the list is empty the slime is not moving
            animator.SetBool("isMoving", false);
        }
    }
  

    public void defeated()
    {
        // enables me to use a trigger to set animations
        animator.SetBool("isAlive", false);
    }

    public void destroyenemy()
    {
         //this is called at the end of the death animation and destroys the game object
        Destroy(gameObject);
    }
}
