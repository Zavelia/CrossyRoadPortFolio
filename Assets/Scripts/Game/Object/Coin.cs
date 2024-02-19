using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Obstacle")||other.CompareTag("Water"))
        {
            Debug.Log("<color=red>코인 삭제</color>");
            Destroy(gameObject);
        }
    }
}
