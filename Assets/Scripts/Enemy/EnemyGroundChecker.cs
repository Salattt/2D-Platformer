using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class EnemyGroundChecker : MonoBehaviour
{
    public event Action ThereNoGroundAhead;

    private void OnTriggerExit2D( Collider2D collision)
    {
        if(collision.GetComponent(typeof(Ground)))
            ThereNoGroundAhead?.Invoke();
    }
}
