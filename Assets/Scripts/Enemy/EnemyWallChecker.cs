using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class EnemyWallChecker : MonoBehaviour
{
    public event Action ThereWallAhead;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent(typeof(Ground)))
            ThereWallAhead?.Invoke();
    }
}
