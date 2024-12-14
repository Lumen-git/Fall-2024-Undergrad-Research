using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject ball;

    public void spawnBall(){
        GameObject spawnedBall = Instantiate(ball, spawnPoint.position, Quaternion.identity);
        Rigidbody rb = spawnedBall.GetComponent<Rigidbody>();
        rb.velocity = spawnPoint.forward * 5.0f;
    }

}
