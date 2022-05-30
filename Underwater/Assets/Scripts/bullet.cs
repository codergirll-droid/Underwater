using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            other.gameObject.GetComponent<Enemy>().takeDamage(Random.Range(5, 15));
        }
    }



}
