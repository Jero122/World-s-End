using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform shootPos;
    
    [SerializeField] private Pistol pistol = new Pistol(10, 1);
    [SerializeField] private SMG smg = new SMG(10, 10);
    [SerializeField] private Shotgun shotgun = new Shotgun(10, 1, 5);
    [SerializeField] private Camera cam;
    
    [SerializeField] private AudioClip pistolPickup;
    [SerializeField] private AudioClip smgPickup;
    [SerializeField] private AudioClip shotGunPickup;
    
    private Weapon _currentWeapon;
    private AudioSource _audioSource;
    private PlayerMovement _playerMovement;
    
    void Start()
    {
        _currentWeapon = pistol;
        _audioSource = gameObject.GetComponent<AudioSource>();
        _playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            equipWeapon(pistol);
            _playerMovement.moveSpeedMultiplier = 1f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            equipWeapon(smg);
            _playerMovement.moveSpeedMultiplier = 0.5f;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            equipWeapon(shotgun);
            _playerMovement.moveSpeedMultiplier = 0.5f;
        }
        
        var target = cam.ScreenToWorldPoint( new Vector2(Input.mousePosition.x,  Input.mousePosition.y) );
        _currentWeapon.Shoot(shootPos, target);
    }

    private void equipWeapon(Weapon weapon)
    {
        if (_currentWeapon != weapon)
        {
            _currentWeapon = weapon;
            FindObjectOfType<AudioManager>().play(weapon.EquipSound);
        }
    }
    
}