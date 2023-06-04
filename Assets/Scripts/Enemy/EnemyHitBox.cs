using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class EnemyHitBox : MonoBehaviour
{
    public event Action GetHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent(typeof(Player)))
            GetHit?.Invoke();
    }
}
