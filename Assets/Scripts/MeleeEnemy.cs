using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float health;
    [SerializeField] private float damageValue;
    [SerializeField] private float speed;
    

    [SerializeField] private AudioClip hitSound;
    
    
    private Rigidbody2D rb;
    private PlayerMovement player;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        Vector2 myPos = gameObject.transform.position;
        Vector2 playerPos = player.transform.position;
        Vector2 distanceDifference = playerPos - myPos;
        distanceDifference.Normalize();
        rb.velocity = ((distanceDifference) * speed * Time.fixedDeltaTime);
    }

    public void takeDamage(float value)
    {
        if (health <= 0) return;
            health -= value;
        FindObjectOfType<AudioManager>().play(hitSound);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<ScoreCounter>().incrementScore();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth)
        {
            playerHealth.takeDamage(damageValue);
        }
    }
}
