using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class InputManagerXR : MonoBehaviour
{ 
    private InputData _inputData;
    public static InputManagerXR Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {

        _inputData = GetComponent<InputData>();
    }

    public bool Move()
    {
        return _inputData._rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 rightAxis);
    }

    public bool Velocity()
    {
        return _inputData._leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 leftAxis);
    }

}
