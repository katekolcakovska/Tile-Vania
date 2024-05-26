using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D arrowRigidBody;
    PlayerMovement player;
    float xSpeed;
    [SerializeField] float arrowSpeed = 20f;
    void Awake()
    {
        
       
    }

    void Start()
    {
        arrowRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * arrowSpeed;
    }
    void Update()
    {
        arrowRigidBody.velocity = new Vector2(xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            
        }
        Destroy(gameObject);
    }
}
