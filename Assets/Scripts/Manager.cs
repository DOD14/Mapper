using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        QualitySettings.shadows = ShadowQuality.Disable;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
