using UnityEngine;
using CommonUsages = UnityEngine.XR.CommonUsages;

public class Vibration : MonoBehaviour
{
    private InputData _inputData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputData._leftController.TryGetFeatureValue(CommonUsages.trigger, out var leftTrigger))
        {
            if ((leftTrigger >= 1))
            {
                Debug.Log("sistemaFuncionando");
            }
        }
    }
}
