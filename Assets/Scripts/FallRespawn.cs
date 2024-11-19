using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRespawn : MonoBehaviour
{
    [SerializeField]
    private Transform respawnPoint;
    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position = new Vector3(
                respawnPoint.position.x,
                respawnPoint.position.y + 2f,
                respawnPoint.position.z);
        }
    }
}
