using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float destoryAfterSeconds;
    public float damage;
    
    public Vector3 direction;


    private void Start()
    {
        Destroy(gameObject,destoryAfterSeconds);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        var enemy = other.gameObject.GetComponent<MeleeEnemy>();
        if (enemy)
        {
            enemy.takeDamage(damage);
        }
        Destroy(gameObject);
    }
}
