using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MonoBehaviour
{
    Light fireLight;

    public float intensityRange;
    float maximumIntensity;
    float minimumIntensity;
    float medianIntensity;
    float currentIntensity;
    float targetIntensity;

    bool shiftingToTargetIntensity;
    bool targetIntensityChosen;
    bool warming;

    bool shouldRiseNext;

    [Range (0.1f, 25.0f)]
    public float frequency;

    // Start is called before the first frame update
    void Start()
    {
        fireLight = GetComponent<Light>();

        medianIntensity = fireLight.intensity;

        maximumIntensity = medianIntensity + intensityRange;
        minimumIntensity = medianIntensity - intensityRange;

        currentIntensity = medianIntensity;
    }

    
    void Update()
    {
        Debug.Log(medianIntensity);

        if (!shiftingToTargetIntensity)
        {
            targetIntensity = Random.Range(minimumIntensity, maximumIntensity);

 //          Debug.Log("The target intensity is " + targetIntensity + ".");

            if (targetIntensity <= currentIntensity)
            {
                warming = false;
            }

            if (targetIntensity > currentIntensity)
            {
                warming = true;
            }

            shiftingToTargetIntensity = true;
        }

        else
        {
            if (!warming)
            {
 //               Debug.Log("The light is cooling.");
                currentIntensity -= frequency * Time.deltaTime;
                fireLight.intensity = currentIntensity;
                if (currentIntensity <= targetIntensity)
                {
 //                   Debug.Log("The light has warmed to its target intensity.");
                    warming = true;
                    shouldRiseNext = true;
                    shiftingToTargetIntensity = false;
                }
            }
            else
            {
 //               Debug.Log("The light is warming.");
                currentIntensity += frequency * Time.deltaTime;
                fireLight.intensity = currentIntensity;
                if (currentIntensity >= targetIntensity)
                {
 //                   Debug.Log("The light has faded to its target intensity.");
                    warming = false;
                    shouldRiseNext = false;
                    shiftingToTargetIntensity = false;
                }
            }
        }
    }
}
