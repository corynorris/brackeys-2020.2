using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Player player;

    public float attackRange = 1.5f;
    public float attackSize = 0.5f;
    
    public LayerMask enemyLayers;
    public float attackDamage = 3;
    void Awake()
    {
        player = GetComponent<Player>();
     
    }

    private void Start()
    {
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }



    }

    void Attack()
    {
        player.TriggerAllAnimators("Attack");

        // Detect enemies in range
        
        Vector3 pointInFrontOfPlayer = player.GetCenter() + player.GetForwardDirection() * attackRange;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(pointInFrontOfPlayer, attackSize, enemyLayers);


        //Debug.DrawLine(player.transform.position, pointInFrontOfPlayer, Color.black, 200, false);
        Vector3 point1 = new Vector3(pointInFrontOfPlayer.x - attackSize, pointInFrontOfPlayer.y + attackSize);
        Vector3 point2 = new Vector3(pointInFrontOfPlayer.x + attackSize, pointInFrontOfPlayer.y + attackSize);
        Vector3 point3 = new Vector3(pointInFrontOfPlayer.x + attackSize, pointInFrontOfPlayer.y - attackSize);
        Vector3 point4= new Vector3(pointInFrontOfPlayer.x - attackSize, pointInFrontOfPlayer.y - attackSize);

        Debug.DrawLine(point1, point2, Color.red, 1, false);
        Debug.DrawLine(point2, point3, Color.red, 1, false);
        Debug.DrawLine(point3, point4, Color.red, 1, false);
        Debug.DrawLine(point4, point1, Color.red, 1, false);


        // Do something with the resources
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit!: " + enemy.name);
            Health health = enemy.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(attackDamage);
            }
        }
    }



}


