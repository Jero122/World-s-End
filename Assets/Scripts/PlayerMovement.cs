using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float moveSpeed;
    [HideInInspector] public float moveSpeedMultiplier;
    [SerializeField] private GameObject feet;
    [SerializeField] private Camera _cam;
    
    private Vector2 _direction;
    private Vector2 _mousePos;
    
    private Rigidbody2D _rb;
 
    private Animator _animator;
    private Animator _feetAnimator;
   
    private static readonly int Walking = Animator.StringToHash("walking");

    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponent<Animator>();
        _feetAnimator = feet.GetComponent<Animator>();
        moveSpeedMultiplier = 1;
    }
    
    // Update is called once per frame
    private void Update()
    {
        _direction = GetDirection();

        if (_direction.x != 0 || _direction.y != 0)
        {
            _animator.SetBool(Walking, true);
            _feetAnimator.SetBool(Walking, true);
        }
        else
        {
            _animator.SetBool(Walking,false);
            _feetAnimator.SetBool(Walking, false);
        }
        _mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

    }

    private Vector2 GetDirection()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _direction * (moveSpeed * moveSpeedMultiplier * Time.fixedDeltaTime));
        
        var lookDir = _mousePos - _rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        _rb.rotation = angle;


    }
}
