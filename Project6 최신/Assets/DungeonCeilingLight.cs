using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCeilingLight : MonoBehaviour {

	
	public GameObject SpotLight;

	public float MaxLightIntensity;
	public float IntensityLight;



	void Start()
	{
		SpotLight.GetComponent<Light>().intensity = IntensityLight;
		

	}


	void Update()
	{
		if (IntensityLight < 0) IntensityLight = 0;
		if (IntensityLight > MaxLightIntensity) IntensityLight = MaxLightIntensity;

		SpotLight.GetComponent<Light>().intensity = IntensityLight / 2f + Mathf.Lerp(IntensityLight - 0.1f, IntensityLight + 0.1f, Mathf.Cos(Time.time * 30));

		SpotLight.GetComponent<Light>().color = new Color(Mathf.Min(IntensityLight / 1.5f, 1f), Mathf.Min(IntensityLight / 2f, 1f), 0f);
	

	}
}
