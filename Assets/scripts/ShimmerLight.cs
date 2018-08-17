using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShimmerLight : MonoBehaviour {


    public float minIntensity = 1f;
    public float maxIntensity = 1.5f;
    public float minRange = 10f;
    public float maxRange = 15f;    

    private Light light;

    float random;

    void Start() {
        random = Random.Range(0.0f , 65535.0f);
        light = GetComponent<Light>();
    }

    void Update() {
        float noise = Mathf.PerlinNoise(random , Time.time);
        light.intensity = Mathf.Lerp(minIntensity , maxIntensity , noise);
        light.range = Mathf.Lerp(minRange , maxRange , noise);
    }


    /*
     * 
     * 
     * 
    public float intensityShimmer = 0.1f;
    public float rangeShimmer = 2f;
    public float delay = 0.5f;
    public float intensityShimmerRate = 1f;
    public float rangeShimmerRate = 1f;

  
    private Light light; //<-fix this unity, Component.light is no longer accessible.
    private float maxIntensityChange;
    private float minIntensityChange;
    private float minRangeClamp;
    private float maxRangeClamp;


}
    
	// Use this for initialization
	void Start () {

        light = GetComponent<Light>();
        float initialIntensity = light.intensity;
        maxIntensityChange = initialIntensity + intensityShimmer;
        //   minIntensityChange = initialIntensity - intensityShimmer;
        minIntensityChange = initialIntensity;


        minRangeClamp = light.range;
        maxRangeClamp = light.range + rangeShimmer;

	}
	
	// Update is called once per frame
	void Update () {

        //StartCoroutine(Shimmer());
        Shimmer();
	}

    IEnumerator Flicker() {


        float intensityOffset = Random.Range(-intensityShimmer , intensityShimmer);
        light.intensity += intensityOffset;
        light.intensity = Mathf.Clamp(light.intensity , minIntensityChange , maxIntensityChange);


        float rangeOffset;
        if (intensityOffset > 0f)
            rangeOffset = Random.Range(0f , rangeShimmer);

        else
            rangeOffset = Random.Range(-rangeShimmer , 0f);

        light.range += rangeOffset;
        light.range = Mathf.Clamp(light.range , minRangeClamp , maxRangeClamp);
        //TODO: code repeated, turn into a function (attempted in Shimmer() below) further, light.intensity is a propery, cannot be used with out or ref.
        

        Debug.Log("Coroutine running");


        yield return new WaitForSeconds(1f);

    }

    void Shimmer () {
        if (light.intensity <= minIntensityChange || light.range <= minRangeClamp) {
            light.intensity = Mathf.SmoothStep(minIntensityChange , maxIntensityChange , Time.deltaTime * intensityShimmerRate);
            light.range = Mathf.SmoothStep(minRangeClamp , maxRangeClamp , Time.deltaTime * rangeShimmerRate);
        }

        if(light.intensity >= maxIntensityChange || light.range >= maxRangeClamp) {
            light.intensity = Mathf.SmoothStep(maxIntensityChange, minIntensityChange , Time.deltaTime * intensityShimmerRate);
            light.range = Mathf.SmoothStep(maxRangeClamp, minRangeClamp , Time.deltaTime * rangeShimmerRate);
        }

    }



  /*  
    IEnumerator Shimmer(ref float value,float intensity, float minClamp, float maxClamp) {
    // ITERATOR METHODS CANNOT HAVE REFERENCE ARGUMENTS

        float intensityOffset = Random.Range(-intensity , intensity);
        value += intensityOffset;

        value = Mathf.Clamp(light.intensity , minClamp , maxClamp);
        Debug.Log("Coroutine running" +value);
        yield return new WaitForSeconds(delay);

    }
    
    */
}
