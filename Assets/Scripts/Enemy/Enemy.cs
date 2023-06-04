using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyMovement))]

public class Enemy : MonoBehaviour
{
    private EnemyHitBox _hitBox;
    private Animator _animator;
    private EnemyMovement _enemyMovement;
    private int _animationDie = Animator.StringToHash("IsDiyng");
    private float _destroyDelay = 0.6f;

    private void Awake()
    {
        _animator= GetComponent<Animator>();
        _enemyMovement= GetComponent<EnemyMovement>();
        _hitBox= GetComponentInChildren<EnemyHitBox>();
    }

    private void OnEnable()
    {
        _hitBox.GetHit += Die;
    }

    private void OnDisable()
    {

        _hitBox.GetHit -= Die;
    }

    private void Die()
    {
        _animator.SetBool(_animationDie, true);
        _enemyMovement.Stop();
        Destroy(gameObject, _destroyDelay);    
    }
}
