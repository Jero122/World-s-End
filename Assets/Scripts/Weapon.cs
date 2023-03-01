using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class Weapon
{
    [SerializeField] protected float damage;
    [SerializeField] protected float fireRate;
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected float bulletForce;

    [SerializeField] protected AudioClip shootSound;
    [SerializeField] protected AudioClip equipSound;
    
    [SerializeField] protected Animator muzzleFlash;

    protected float timeSinceLastShot = 0f;
    
    protected Weapon(float damage, float fireRate)
    {
        this.damage = damage;
        this.fireRate = fireRate;
    }

    public virtual void Shoot(Transform shootPos, Vector3 target)
    {
        throw new NotImplementedException();
    }

    public float Damage => damage;
    public float FireRate => fireRate;
    public AudioClip EquipSound => equipSound;
}

[Serializable]
public class Pistol : Weapon
{
    public Pistol(float damage, float fireRate) : base(damage, fireRate)
    {
        
    }
    
    public override void Shoot(Transform shootPos,Vector3 target)
    {
        timeSinceLastShot += Time.deltaTime;
        
        if (Input.GetButtonDown("Fire1"))
        {
            if (timeSinceLastShot > 0.6/fireRate)
            {
                var bullet = GameObject.Instantiate(projectile, shootPos.position, shootPos.rotation);
                bullet.GetComponent<Projectile>().damage = this.damage;
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                
                Vector2 direction = target - shootPos.position;
                direction.Normalize();
                rb.velocity = direction * bulletForce;
                timeSinceLastShot = 0f;
               
                float pitch = UnityEngine.Random.Range(0.9f, 1.1f);
                GameObject.FindObjectOfType<AudioManager>().play(shootSound,pitch);
                muzzleFlash.SetTrigger("shoot");

            }
        }

        
    }
    
}

[Serializable]
public class SMG : Weapon
{
    public SMG(float damage, float fireRate) : base(damage, fireRate)
    {
    }

    public override void Shoot(Transform shootPos,Vector3 target)
    {
        timeSinceLastShot += Time.deltaTime;
        
        if (Input.GetButton("Fire1"))
        {
            if (timeSinceLastShot > 0.6/fireRate)
            {
                var bullet = GameObject.Instantiate(projectile, shootPos.position, shootPos.rotation);
                bullet.GetComponent<Projectile>().damage = this.damage;
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                
                Vector2 direction = target - shootPos.position;
                direction.Normalize();
                rb.velocity = direction * bulletForce;
                
                timeSinceLastShot = 0f;
                float pitch = UnityEngine.Random.Range(0.9f, 1.1f);
                GameObject.FindObjectOfType<AudioManager>().play(shootSound,pitch);
                muzzleFlash.SetTrigger("shoot");
            }
        }
    }
}

[Serializable]
public class Shotgun : Weapon
{
    [SerializeField] private float spreadAngle;
    [SerializeField] private int numPellets;
    
    
    public Shotgun(float damage, float fireRate, float spread) : base(damage, fireRate)
    {
        this.spreadAngle = spread;
    }

    public override void Shoot(Transform shootPos,Vector3 target)
    {
        timeSinceLastShot += Time.deltaTime;
        
        if (Input.GetButtonDown("Fire1"))
        {
            if (timeSinceLastShot > 0.6/fireRate)
            {
                
                float rotIncrements = spreadAngle / numPellets;
                float rotation = (spreadAngle / 2);


                for (int i = 0; i <= numPellets; i++)
                {
                    var pos = shootPos.position;
                    var shootRot = shootPos.rotation;
                
                    Vector2 desiredDirection = target - shootPos.position;
                    desiredDirection.Normalize();
             
                    float rotateAngle = rotation + (Mathf.Atan2(desiredDirection.y, desiredDirection.x) * Mathf.Rad2Deg);
                
                    var MovementDirection = new Vector2(
                        Mathf.Cos(rotateAngle * Mathf.Deg2Rad),
                        Mathf.Sin(rotateAngle * Mathf.Deg2Rad)).normalized;

                    var bullet = GameObject.Instantiate(projectile, pos, shootRot);
                    bullet.GetComponent<Projectile>().damage = this.damage;
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.velocity = MovementDirection * bulletForce;
                    
                    rotation -= rotIncrements;
                }
                
                timeSinceLastShot = 0f;

                float pitch = UnityEngine.Random.Range(0.9f, 1.1f);
                GameObject.FindObjectOfType<AudioManager>().play(shootSound,pitch);
                muzzleFlash.SetTrigger("shoot");
            }
        }
    }
}