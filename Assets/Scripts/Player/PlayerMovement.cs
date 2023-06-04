using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _maxWalkSpeed;
    [SerializeField] private float _timeToMaxWalkSpeed;
    [SerializeField] private float _maxJumpForce;
    [SerializeField] private float _timeToMaxJumpForce;

    private GroundChecker _groundChecker;
    private Animator _animator;
    private Transform _transform;
    private Rigidbody2D _rb;
    private float _jumpForce = 0;
    private int _animatorYSpeed = Animator.StringToHash("YSpeed");
    private int _animatorIsOnGround = Animator.StringToHash("IsOnGround");
    private int _animatorIsJumpStartup = Animator.StringToHash("IsJumpStartup");
    private bool _isFaceRight = true;

    private void Awake()
    {
        _animator= GetComponent<Animator>();
        _transform= transform;
        _rb= GetComponent<Rigidbody2D>();
        _groundChecker= GetComponentInChildren<GroundChecker>();
    }

    private void OnEnable()
    {
        _groundChecker.HitEnemy += OnHit;
    }

    private void OnDisable()
    {
        _groundChecker.HitEnemy -= OnHit;
    }

    private void Update()
    {
        Walk();
        Jump();
        Reflect();

        _animator.SetFloat(_animatorYSpeed, _rb.velocity.y);
        _animator.SetBool(_animatorIsOnGround, _groundChecker.IsOnGround);
    }

    private void Walk()
    {
        float newSpeed;

        if (Input.GetKey(KeyCode.D) && _groundChecker.IsOnGround)
        {
            newSpeed = _rb.velocity.x + _maxWalkSpeed * Time.deltaTime / _timeToMaxWalkSpeed;

            if (newSpeed <= _maxWalkSpeed)
                _rb.velocity = new Vector2(newSpeed, _rb.velocity.y);
            else
                _rb.velocity = new Vector2(_maxWalkSpeed, _rb.velocity.y);
        }

        if (Input.GetKey(KeyCode.A) && _groundChecker.IsOnGround)
        {
            newSpeed = _rb.velocity.x - _maxWalkSpeed * Time.deltaTime / _timeToMaxWalkSpeed;

            if (newSpeed * -1 <= _maxWalkSpeed)
                _rb.velocity = new Vector2(newSpeed, _rb.velocity.y);
            else
                _rb.velocity = new Vector2(-1 * _maxWalkSpeed, _rb.velocity.y);
        }
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _jumpForce += _maxJumpForce * Time.deltaTime / _timeToMaxJumpForce;

            _animator.SetBool(_animatorIsJumpStartup, true);
        }
        else 
        {
            if(_jumpForce <= _maxJumpForce)
                _rb.AddForce(new Vector2(0 , _jumpForce));
            else
                _rb.AddForce(new Vector2(0 , _maxJumpForce));

            _jumpForce = 0;

            _animator.SetBool(_animatorIsJumpStartup, false);
        }
    }

    private void Reflect() 
    { 
        if((_rb.velocity.x > 0.1 && _isFaceRight == false) || (_rb.velocity.x < -0.1 && _isFaceRight))
        {
            _isFaceRight = !_isFaceRight;
            _transform.localScale *= new Vector2(-1, 1);
        }
    }

    private void OnHit()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        _rb.AddForce(new Vector2(0,_maxJumpForce));
    }
}
