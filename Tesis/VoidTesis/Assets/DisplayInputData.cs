using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using CommonUsages = UnityEngine.XR.CommonUsages;

[RequireComponent(typeof(InputData))]
public class DisplayInputData : MonoBehaviour
{
    private InputData _inputData;

    [Header("Nave Objects")] [SerializeField]
    private GameObject nave;

    [SerializeField] private Rigidbody cabinRigidbody;

    [Header("Speed & Vectors 3")] [SerializeField]
    public float rotSpeed = 0.002f;

    [SerializeField] private Vector3 relativeFwd;
    [NonSerialized] public float speed = 10f;
    [SerializeField] public float maxSpeedTurbo;
    [SerializeField] private float modifiedSpeed;

    [Header("Bullets")] [SerializeField] private Transform shootTransform;
    [SerializeField] private bool canShoot = true;
    [SerializeField] private PoolingSystem poolingSystem;
    [SerializeField] private bool advancing;



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
                modifiedSpeed = Mathf.Clamp(speed, 1, maxSpeedTurbo);
                cabinRigidbody.linearVelocity = relativeFwd * (modifiedSpeed * leftAxis.y);
            }
        }

        // Trigger Shoot Handler Left
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
    }

    public void Turbo(float multiplier, float turboDur)
    {
        StartCoroutine(StartTurbo(multiplier, turboDur));
    }

    private IEnumerator StartTurbo(float multiplier, float turboDuration)
    {
        speed *= multiplier;
        yield return new WaitForSeconds(turboDuration);
        speed = 10;
    }
    
    private IEnumerator Shoot()
    {
        var newBullet = poolingSystem.AskForObject(shootTransform);
        newBullet.transform.parent = null;

        canShoot = false;
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }
}
