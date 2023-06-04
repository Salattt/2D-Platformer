using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody2d;
    private EnemyGroundChecker _groundChecker;
    private EnemyWallChecker _wallChecker;

    public void Stop()
    {
        _speed= 0;
    }

    private void Awake()
    {
        _rigidbody2d= GetComponent<Rigidbody2D>();
        _groundChecker= GetComponentInChildren<EnemyGroundChecker>();
        _wallChecker= GetComponentInChildren<EnemyWallChecker>();
    }

    private void OnEnable()
    {
        _groundChecker.ThereNoGroundAhead += TurnAround;
        _wallChecker.ThereWallAhead+= TurnAround;
    }

    private void OnDisable()
    {
        _groundChecker.ThereNoGroundAhead -= TurnAround;
        _wallChecker.ThereWallAhead -= TurnAround;
    }

    private void Update()
    {
        _rigidbody2d.velocity = new Vector2(_speed, 0);
    }

    private void TurnAround()
    {
        _speed *= -1;
        transform.localScale *= new Vector2(-1, 1);
    }
}
