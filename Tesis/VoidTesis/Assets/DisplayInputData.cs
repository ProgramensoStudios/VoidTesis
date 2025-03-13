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

    [Header("Nave Objects")][SerializeField]
    private GameObject nave;

    [SerializeField] private Rigidbody cabinRigidbody;

    [Header("Speed & Vectors 3")][SerializeField]
    public float rotSpeed = 0.002f;

    [SerializeField] private Vector3 relativeFwd;
     public float speed;
    [SerializeField] public float maxSpeedTurbo;
    [SerializeField] private float turboMultiplier;
    [SerializeField] private float modifiedSpeed;
    [SerializeField] private float baseSpeed;
    public bool isTurboPressed = true;
    [SerializeField] private float maxSpeedGripTurbo;
    public Coroutine turboCoroutine;

    [Header("Bullets")] [SerializeField] private Transform shootTransform;
    [SerializeField] private bool canShoot = true;
    [SerializeField] private PoolingSystem poolingSystem;
    [SerializeField] private bool advancing;



    private void Start()
    {
        _inputData = GetComponent<InputData>();
        speed = baseSpeed;
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

        //Turbo
        if(_inputData._leftController.TryGetFeatureValue(CommonUsages.gripButton, out var leftGrip) && (_inputData._rightController.TryGetFeatureValue(CommonUsages.gripButton, out var rightGrip)))
        {
            if (leftGrip && rightGrip)
            {
                isTurboPressed = true;
                TurboGrip();
            }
            else if (!leftGrip && !rightGrip) 
            { 
                isTurboPressed = false; 
                TurboExit();
            }
        }
    }

    public void Turbo(float multiplier)
    {
        speed *= multiplier;
    }
    public void TurboGrip()
    {
        speed = maxSpeedGripTurbo;
    }

    public void TurboExit()
    {
        speed = baseSpeed;

    }
    
    private IEnumerator Shoot()
    {
        var newBullet = poolingSystem.AskForObject(shootTransform);
        newBullet.transform.parent = null;

        canShoot = false;
        yield return new WaitForSeconds(0.3f);
        canShoot = true;
    }
}
