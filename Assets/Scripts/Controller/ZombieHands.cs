using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHands : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage();
            }
        }
    }
}
