using System;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    private Vector3 initialPosition;
    private Rigidbody rb;
    public UnityEvent onBallDestroyed = new UnityEvent();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = rb.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.position.y < -10)
        {
            onBallDestroyed.Invoke();
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "KillZone")
        {
            onBallDestroyed.Invoke();
            Destroy(gameObject);
        }
    }
}
