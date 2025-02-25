using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamToggle : MonoBehaviour
{
  public GameObject planeUp;
  public Text textUi;

  public void OnValueChanged(bool upOn)
  {
    if (upOn)
    {
      planeUp.SetActive(true);
      textUi.text = "See Down";
    }
    else
    {
      planeUp.SetActive(false);
      textUi.text = "See Up";
    }
    
  }
}
