using UnityEngine;

public class UpDownChanger : PlayerSettings
{
    public void OnValueChanged(bool value)
    {
        invertYAxis = value;
    }

}