using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Destroy the fruit
            Destroy(gameObject);

            // Increment score
            ScoreKeeper.ScorePoints(1);
        }
    }
}
