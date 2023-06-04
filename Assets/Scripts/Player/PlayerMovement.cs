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
    private Rigidbody2D _rigidbody2d;
    private float _jumpForce = 0;
    private int _animatorYSpeed = Animator.StringToHash("YSpeed");
    private int _animatorIsOnGround = Animator.StringToHash("IsOnGround");
    private int _animatorIsJumpStartup = Animator.StringToHash("IsJumpStartup");
    private bool _isFaceRight = true;
    private float _epsilon = 0.1f;

    private void Awake()
    {
        _animator= GetComponent<Animator>();
        _transform= transform;
        _rigidbody2d= GetComponent<Rigidbody2D>();
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

        _animator.SetFloat(_animatorYSpeed, _rigidbody2d.velocity.y);
        _animator.SetBool(_animatorIsOnGround, _groundChecker.IsOnGround);
    }

    private void Walk()
    {
        float newSpeed;

        if (Input.GetKey(KeyCode.D) && _groundChecker.IsOnGround)
        {
            newSpeed = _rigidbody2d.velocity.x + _maxWalkSpeed * Time.deltaTime / _timeToMaxWalkSpeed;

            if (newSpeed <= _maxWalkSpeed)
                _rigidbody2d.velocity = new Vector2(newSpeed, _rigidbody2d.velocity.y);
            else
                _rigidbody2d.velocity = new Vector2(_maxWalkSpeed, _rigidbody2d.velocity.y);
        }

        if (Input.GetKey(KeyCode.A) && _groundChecker.IsOnGround)
        {
            newSpeed = _rigidbody2d.velocity.x - _maxWalkSpeed * Time.deltaTime / _timeToMaxWalkSpeed;

            if (newSpeed * -1 <= _maxWalkSpeed)
                _rigidbody2d.velocity = new Vector2(newSpeed, _rigidbody2d.velocity.y);
            else
                _rigidbody2d.velocity = new Vector2(-1 * _maxWalkSpeed, _rigidbody2d.velocity.y);
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
                _rigidbody2d.AddForce(new Vector2(0 , _jumpForce));
            else
                _rigidbody2d.AddForce(new Vector2(0 , _maxJumpForce));

            _jumpForce = 0;

            _animator.SetBool(_animatorIsJumpStartup, false);
        }
    }

    private void Reflect() 
    { 
        if((_rigidbody2d.velocity.x > _epsilon && _isFaceRight == false) || (_rigidbody2d.velocity.x < _epsilon && _isFaceRight))
        {
            _isFaceRight = !_isFaceRight;
            _transform.localScale *= new Vector2(-1, 1);
        }
    }

    private void OnHit()
    {
        _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, 0);
        _rigidbody2d.AddForce(new Vector2(0,_maxJumpForce));
    }
}
