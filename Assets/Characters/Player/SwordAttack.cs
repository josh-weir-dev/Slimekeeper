using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Collider2D SwordCollider;
    public float damage = 3f;
    public float knockbackforce = 16f;
    // defining variables to be used 
    


    // Start is called before the first frame update
    private void Start()
    {
        // initialsing variables I have defined

       
    }
    // method for attacking facing right

    public void AttackRight()
    {
        SwordCollider.enabled = true;
        

    }
    // method for attacking facing left
    public void AttackLeft()
    {
        SwordCollider.enabled = true;
    }
    // method to stop attacking
    public void StopAttack()
    {
        SwordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // this is an if statement that determines if the sword hitbox is hitting another hitbox that belongs to an enemy
        
        if (other.tag == "Enemy")
        { // deal damage to enemy
            Enemy enemy = other.GetComponent<Enemy>();
            Boss boss = other.GetComponent<Boss>();
            if(enemy != null){
                enemy.Start();
                // taking the position of the sword and the enemy to work out the direction of knockback
                Vector2 direction = (enemy.transform.position - transform.position).normalized;
                enemy.rb.AddForce(direction * knockbackforce, ForceMode2D.Impulse);
                // if the sword hits an enemy then do damage
                enemy.Health -= damage;

                
            }
            else if (boss!=null)
            {
                boss.Start();
                // taking the position of the sword and the enemy to work out the direction of knockback
                Vector2 direction = (boss.transform.position - transform.position).normalized;
                boss.rb.AddForce(direction * knockbackforce, ForceMode2D.Impulse);
                // if the sword hits an enemy then do damage
                boss.Health -= damage;

            }
        }
            
        
    }
}



