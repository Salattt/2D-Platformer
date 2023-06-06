using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class EnemyHitBox : MonoBehaviour
{
    public event Action GettingHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent(typeof(Player)))
            GettingHit?.Invoke();
    }
}
