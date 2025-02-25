using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPLayer : Bullet
{
    private void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

  //  private void OnTriggerEnter(Collider other)
  // {
  //      if (other.tag == "Enemy")
  //         Destroy(gameObject);
  // // }

    private void Update()
    {
        RB.AddForce(transform.up * Speed);
        Destroy(gameObject, Range);
    }
}
