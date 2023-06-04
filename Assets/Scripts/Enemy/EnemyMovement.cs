using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rb;
    private EnemyGroundChecker _groundChecker;
    private EnemyWallChecker _wallChecker;

    public void Stop()
    {
        _speed= 0;
    }

    void Awake()
    {
        _rb= GetComponent<Rigidbody2D>();
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

    void Update()
    {
        _rb.velocity = new Vector2(_speed, 0);
    }

    private void TurnAround()
    {
        _speed *= -1;
        transform.localScale *= new Vector2(-1, 1);
    }
}
