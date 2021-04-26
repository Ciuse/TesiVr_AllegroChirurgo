using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnandOff : MonoBehaviour
{
 

    public GameObject nosePatient;

    private Color colorDefault;
    // Start is called before the first frame update
    void Start()
    {
        colorDefault = nosePatient.GetComponent<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLight()
    {
        //nosePatient.GetComponent<MeshRenderer>().material.color=Color.red;
        nosePatient.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
    }
    
    public void OffLight()
    {
       // nosePatient.GetComponent<MeshRenderer>().material.color=colorDefault;
        nosePatient.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
    }

    
}
