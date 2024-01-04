using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private AudioSource audioSource; // Reference to the AudioSource component

    // Initialize audio
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Play audio if player collides
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}
