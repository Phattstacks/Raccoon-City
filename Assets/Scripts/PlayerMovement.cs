using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private BoxCollider2D _boxCollider2D;
    private Animator _animator;
    private AudioSource _audioSource;

    [Header("Audio")]
   [SerializeField] private AudioClip _jumpClip;
  


    [SerializeField] Vector2 _checkGroundDimensions;
    [SerializeField] LayerMask _platformLayer;
    

    [Header("Movement")]
    [SerializeField] float _jumpForce;
    [SerializeField] private float _topSpeed;
    [SerializeField] private float _defaultSpeed;

    [Header("Animation Strings")]
    private const string _horizontal = "Horizontal";
    private const string _inAir = "InAir";
    private const string _isRunning = "IsSprinting";


   
    private float _horizontalInput;
    public bool _isSprinting;

   
   

    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
       

    }
    private void Update()
    {
        AnimationChecks();
    }

    private void AnimationChecks()
    {
        if (_isSprinting)
        {
            _animator.SetBool(_isRunning, true);
        }
        else
        {
            _animator.SetBool(_isRunning, false);
        }
        if (IsGrounded())
        {
            _animator.SetBool(_inAir, false);
        }
        else
        {
            _animator.SetBool(_inAir, true);
        }
        
    }
    #region Movement
    private void FixedUpdate()
    {
        HorizontalMovement();
    }
    private void OnJump()
    {
        if (IsGrounded())
        {
            _rb.velocity += Vector2.up * _jumpForce;
            SoundManager.instance.PlayClip(_jumpClip, _audioSource);
            

        }
        
    }
    
    private void OnHorizontalMovement(InputValue axis)
    {
        _horizontalInput = axis.Get<float>();
        _animator.SetFloat(_horizontal, _horizontalInput);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, (Vector3)_checkGroundDimensions);
    }

    private void HorizontalMovement()
    {
        if (_isSprinting)
        {
            _rb.velocity = new Vector2(_topSpeed * _horizontalInput, _rb.velocity.y);
        }
        else
        {
            _rb.velocity = new Vector2(_defaultSpeed * _horizontalInput, _rb.velocity.y);
        }
        
        

    }

    private void OnSprint(InputValue input)
    {
        _isSprinting = input.isPressed;
      
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, 0f, Vector2.down, 1f, _platformLayer);
        return raycastHit2D.collider != null;
    }
    #endregion

}
