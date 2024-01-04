using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public float speed = 2.0f;
    public float distance = 2.3f;
    private Vector3 startPosition;
    private AudioSource audioSource; // Reference to the AudioSource component

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float screenMinY = Camera.main.ViewportToWorldPoint(Vector2.zero).y;
        float screenMaxY = Camera.main.ViewportToWorldPoint(Vector2.one).y;

        // Move the rock up and down at given speed over given distance
        float newY = Mathf.PingPong(Time.time * speed, distance * 2) - distance;

        // Clamp position
        newY = Mathf.Clamp(newY, screenMinY + 4, screenMaxY - 1);

        transform.position = new Vector3(transform.position.x, startPosition.y + newY, transform.position.z);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Play sound if player collides
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }



}
