using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

public class GroundChecker : MonoBehaviour
{
    public event Action HitEnemy;

    public bool IsOnGround { get; private set; } = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent(typeof(Ground)))
            IsOnGround = true;

        if (collision.GetComponent(typeof(Enemy)))
            HitEnemy?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent(typeof(Ground)))
            IsOnGround = false;
    }
}
