using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownButton : PlayerSettings
{
    [SerializeField] public bool upPressed = false;
    [SerializeField] public bool downPressed = false;
    [SerializeField] public bool rightPressed = false;
    [SerializeField] public bool leftPressed = false;
    public UpDownChanger upDownChanger;
    
    [SerializeField] public float rotDegrees = 0.5f;
 
   

    // Update is called once per frame
    void Update()
    {
       
       
            if (upPressed)
            {
                if (upDownChanger.invertYAxis)
                {
                    Vector3 targetRot = new Vector3(
                        rotDegrees,
                        0,
                        0);
                    cabinRigidbody.transform.Rotate(targetRot);
                }
                else
                {
                    Vector3 targetRot = new Vector3(
                        -rotDegrees,
                        0,
                        0);
                    cabinRigidbody.transform.Rotate(targetRot);
                }
                
            }
            
            if (downPressed)
            {
                if (upDownChanger.invertYAxis)
                {
                    Vector3 targetRot = new Vector3(
                        -rotDegrees,
                        0,
                        0);
                    cabinRigidbody.transform.Rotate(targetRot);
                }
                else
                {
                    Vector3 targetRot = new Vector3(
                        rotDegrees,
                        0,
                        0);
                    cabinRigidbody.transform.Rotate(targetRot);
                }
                
            }
            
            if (rightPressed)
            {
                Vector3 targetRot = new Vector3(
                    0,
                    rotDegrees,
                    0);
                cabinRigidbody.transform.Rotate(targetRot);
               
            }
            
            if (leftPressed)
            {
                Vector3 targetRot = new Vector3(
                    0,
                    -rotDegrees,
                    0);
                cabinRigidbody.transform.Rotate(targetRot);
               
            }
            
            


    }

    public void UpNotPress()
    {


        upPressed = false;


    }
    
    public void UpPressed()
    {
       
        upPressed = true;
    }
    
    public void DownNotPress()
    {
        
        downPressed = false;

    }
    
    public void DownPressed()
    {
        
        downPressed = true;
    }
    
    public void RightNotPress()
    {
        
        rightPressed = false;

    }
    
    public void RightPressed()
    {
       
        rightPressed = true;
    }
    
    public void LeftNotPress()
    {
        
        leftPressed = false;

    }
    
    public void LeftPressed()
    {
       
        leftPressed = true;
    }
    
    
  


}
   

