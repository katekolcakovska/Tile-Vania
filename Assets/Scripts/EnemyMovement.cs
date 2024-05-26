using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D enemyRigidbody;
    [SerializeField] float enemyMoveSpeed = 1f;
    void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        enemyRigidbody.velocity = new Vector2(enemyMoveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        enemyMoveSpeed = -enemyMoveSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(enemyRigidbody.velocity.x)), 1f);
    }
}
