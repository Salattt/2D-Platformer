using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider2D))]

public class Mushroom : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
       _audioSource= GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent(typeof(Player)))
        {
            _audioSource.Play();
            Destroy(gameObject, 0.6f);
        }
    }
}
