using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicSetter : MonoBehaviour
{
    public void QualitySetting(int level)
    {
        QualitySettings.SetQualityLevel(level);
    }
    public void LowSetting()
    {
        QualitySettings.SetQualityLevel(0);
    }
    public void MiddleSetting()
    {
        QualitySettings.SetQualityLevel(1);
    }
    public void HighSetting()
    {
        QualitySettings.SetQualityLevel(2);
    }
}
