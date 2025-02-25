using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class shoot : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootFrom1;
    [SerializeField] private Transform shootFrom2;
    
    [SerializeField] private Transform shootFromBack1;
    [SerializeField] private Transform shootFromBack2;
    [SerializeField] private float speed;
    public float shootMulti = 1f;
    
    private Rigidbody rb1;
    private Rigidbody rb2;
    private Rigidbody rb3;
    private Rigidbody rb4;
    public int destroyBulletTime3 = 10;

    public AudioSource sound;

  

    public void Shoot1()
    {
        GameObject bullet1= Instantiate(bulletPrefab, shootFrom1.position, shootFrom1.rotation);
        rb1 = bullet1.GetComponent<Rigidbody>();
        rb1.AddForce(transform.forward * speed * shootMulti);
        Destroy(bullet1, destroyBulletTime3);
        
        GameObject bullet2= Instantiate(bulletPrefab, shootFrom2.position, shootFrom2.rotation);
        rb2 = bullet2.GetComponent<Rigidbody>();
        rb2.AddForce(transform.forward * speed * shootMulti);
        Destroy(bullet2, destroyBulletTime3);
        sound.Play();

        

    }
    
    public void Shoot3()
    {
        GameObject bullet3= Instantiate(bulletPrefab, shootFromBack1.position, shootFrom1.rotation);
        rb3 = bullet3.GetComponent<Rigidbody>();
        rb3.AddForce(transform.forward * speed * shootMulti);
        Destroy(bullet3, destroyBulletTime3);
        
        GameObject bullet4= Instantiate(bulletPrefab, shootFromBack2.position, shootFrom2.rotation);
        rb4 = bullet4.GetComponent<Rigidbody>();
        rb4.AddForce(transform.forward * speed * shootMulti);
        Destroy(bullet4, destroyBulletTime3);
        sound.Play();
    }
 
}
