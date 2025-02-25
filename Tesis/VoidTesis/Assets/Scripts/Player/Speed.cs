using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Speed : PlayerSettings
{
    [SerializeField] private Slider mySlider;
    [SerializeField] private Vector3 relativeFwd;
    public Turbo turbo;
    public int turboMulti;
   
    public ParticleSystem fast;
    public ParticleSystem faster;
    public ParticleSystem motores;
    public ParticleSystem motores2;

   private void Start()
    {
       
        mySlider = GetComponent<Slider>();
        if (mySlider == null) Debug.Log("No Slider");
    }

    private void Update()
    {

        if (turbo.isTurbo)
        {
            relativeFwd = cabinRigidbody.transform.TransformDirection(Vector3.forward);
        
            cabinRigidbody.linearVelocity = relativeFwd * (speed * maxSpeed * turboMulti);
        }
        else
        {
            relativeFwd = cabinRigidbody.transform.TransformDirection(Vector3.forward);
        
            cabinRigidbody.linearVelocity = relativeFwd * (speed * maxSpeed);
        }
        
        if (speed>=0.3f)
        {
           fast.Play();
           faster.Stop();
        }
        switch (speed)
        {
            case >= 0.7f:
                fast.Stop();
                faster.Play();
                break;
            case <= 0.2f:
                turbo.isTurbo = false;
                fast.Stop();
                faster.Stop();
                motores.Stop();
                motores2.Stop();
                break;
        }

        if (!(speed > 0.1f)) return;
        motores.Play();
        motores2.Play();

    }
    
    public void OnValueChanged(float value)
    {
        speed = value;
    }

}
