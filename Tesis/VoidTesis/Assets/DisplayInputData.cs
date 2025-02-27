using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.XR;
using TMPro;
using UnityEngine.Serialization;

[RequireComponent(typeof(InputData))]
public class DisplayInputData : MonoBehaviour
{
    private InputData _inputData;
    [Header ("Nave Objects")]
    [SerializeField] private GameObject nave;
    [SerializeField] private Rigidbody cabinRigidbody;
    
    [Header ("Speed & Vectors 3")]
    [SerializeField] public float rotSpeed = 0.002f;
    [SerializeField] private Vector3 relativeFwd;
    [SerializeField] public float speed;
    
    [Header("Bullets")]
    [SerializeField] private Transform shootTransform;
    [SerializeField] private bool canShoot = true;

    [SerializeField] private PoolingSystem poolingSystem;
    
    private void Start()
    {
        _inputData = GetComponent<InputData>();
    }
    
    // Update is called once per frame
    void Update()
    {

        //Rotation
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out var rightAxis))
        {
            Quaternion spinMovement = new Quaternion(rightAxis.y * rotSpeed * -1, rightAxis.x * rotSpeed, 0, 1);

            nave.transform.rotation = nave.transform.rotation * spinMovement;
        }

        //Movement

        if (_inputData._leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out var leftAxis))
        {
            if (leftAxis.y >= 0f)
            {
                relativeFwd = cabinRigidbody.transform.TransformDirection(Vector3.forward);
                cabinRigidbody.linearVelocity = relativeFwd * (speed * leftAxis.y);
            }
        }
        

        // Trigger Shoot Handler

        if (_inputData._leftController.TryGetFeatureValue(CommonUsages.trigger, out var leftTrigger))
        {
            if ((leftTrigger >= 1) && canShoot)
            {
                StartCoroutine(Shoot());
            }
        }

        // Trigger Shoot Handler

        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.trigger, out var rightTrigger))
        {
            if ((rightTrigger >= 1) && canShoot)
            {
                StartCoroutine(Shoot());
            }
        }

        // Shoot
        IEnumerator Shoot()
        {
            var newBullet = poolingSystem.AskForObject(shootTransform);
            newBullet.transform.parent = null;  
            canShoot = false;
            yield return new WaitForSeconds(0.5f);
            canShoot = true;
        }
    }
}
