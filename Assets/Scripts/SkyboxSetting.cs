using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkyboxSetting : MonoBehaviour {

    public Material mat;
    void Start()
    {
		RenderSettings.skybox = mat;
		float step = mat.GetFloat("_StepSize");
        Debug.Log(step);
        Debug.Log(mat.GetFloat("_CamScroll"));
		mat.SetFloat("_CamScroll", 55 * Mathf.Sign(step));
    }
}