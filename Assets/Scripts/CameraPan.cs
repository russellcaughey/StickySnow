using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour {

    [SerializeField]
    private Vector3 m_StartLocation;
    [SerializeField]
    private Vector3 m_EndLocation;
    [SerializeField]
    private GameObject m_LookAtObject;
    [SerializeField]
    private float m_PanSpeed = 0.02f;
    private bool isForward = true;

	void Start () {
        transform.position = m_StartLocation;
	}
	
	void Update () {
        if (isForward)
        {
            transform.LookAt(m_LookAtObject.transform.position);
            if (Vector3.Distance(transform.position, m_EndLocation) >= 0.5f)
            {
                transform.position = Vector3.Lerp(transform.position, m_EndLocation, Time.deltaTime * m_PanSpeed);
            }
            else
            {
                isForward = false;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, m_StartLocation) >= 0.5f)
            {
                transform.position = Vector3.Lerp(transform.position, m_StartLocation, Time.deltaTime * m_PanSpeed);
            }
            else
            {
                isForward = true;
            }
        }
        
	}
}
