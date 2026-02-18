// Importing libraries
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    // Defining variables
    public float moveSpeed = 150f;
    float time = 0;
    public float maxSpeed = 2f;
    public bool takingdamage = false;
    public float idleFriction = 0.9f;
    public ScreenFlash screenflash;
    public Image image;
    public Collider2D collided;

    public HealthHeartSystem hearts;
    public float knockback = 16f;
    // here there is a health variable and it comes with a set and get so that it can easily be accessed by other scripts and the value can be changed or retrieved easier
    public float Health
    {
        set
        {
            health = value;
            hearts.DrawHearts();

            if (health <= 0)
            {
                defeated();
            }
        }
        get
        {
            return health;
        }
    }
    public float health = 6;
    public float maxhealth = 6f;

    

    Vector2 moveInput;

    Rigidbody2D rb;

    Animator animator;

    bool canmove = true;

    public SwordAttack swordAttack;

    public float duration = 3f;

    bool isMoving = false;

    public LevelGeneration levelgeneration;

    bool IsMoving
    {
        set
        {
            isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }



    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        transform.position = levelgeneration.startingpositions[levelgeneration.randStartpos].position + transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        time += 1;
        
    }

    void defeated()
    {
        animator.SetTrigger("death");
        
        SceneManager.LoadScene("Death Screen", LoadSceneMode.Single);

    }
    private void FixedUpdate()
    {
        // checking if the player can move and if the player is trying to move
        if (canmove && moveInput != Vector2.zero)
        {

            // here if the player can move a force is being added to the player so that it can move
            rb.AddForce(moveInput * moveSpeed * Time.deltaTime);
            // here the script is checking the way the player is facing and then flipping the sprite depending on the value
            if (moveInput.x > 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (moveInput.x < 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            IsMoving = true;

        }
        else
        {
            // slowing the player down once the pplayer stops trying to move
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);
            IsMoving = false;
        }


    }
    // when the player tries to move their movment is converted into a vector 2 I can use in other methods
    void OnMove(InputValue movementValue)
    {
        moveInput = movementValue.Get<Vector2>();

    }
    // when left mouse button is clicked the trigger to attack is set
    void OnFire()
    {
        animator.SetTrigger("Sword Attack");
    }


    // here the player is attacking with the sword, the movment is locked and the direction of attack is obtained
    public void SwordAttack()
    {
        LockMovement();
        if (moveInput.x < 0)
        {
            swordAttack.AttackLeft();
        }
        else
        {
            swordAttack.AttackRight();
        }
    }

    // Method that restricts the player from moving
    public void LockMovement()
    {
        canmove = false;
    }

    // Method that allows the player to move again
    public void UnlockMovement()
    {
        canmove = true;
        swordAttack.StopAttack();
    }
    // this method is called when the player collides with an object
    void OnCollisionEnter2D(Collision2D collision)
    {
        
      
        // here we are checking if the player collided with an enemy
        
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        Boss boss = collision.collider.GetComponent<Boss>();
        if (enemy != null)
        {
            

            // if the player collided with an enemy the player takes damage and knockback
            if (health > 0)
            {
                enemy.Start();
                LockMovement();
                Vector2 direction = (transform.position - enemy.transform.position).normalized;
                rb.AddForce(direction * knockback, ForceMode2D.Impulse);
                UnlockMovement();
                Health -= enemy.damage;
                screenflash.setactive();
                screenflash.Invoke("deactive", 0.3f);
            }
          
            
            
        }
        else if (boss!=null)
        {
            if (health > 0)
            {
                boss.Start();
                LockMovement();
                Vector2 direction = (transform.position - boss.transform.position).normalized;
                rb.AddForce(direction * knockback, ForceMode2D.Impulse);
                UnlockMovement();
                Health -= boss.damage;
                screenflash.setactive();
                screenflash.Invoke("deactive", 0.3f);
            }
        }

    }

 
}