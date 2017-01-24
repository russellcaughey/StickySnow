using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private GameObject m_FollowObject;
    [SerializeField]
    private Vector3 m_Offset;
    private Vector3 m_NewOffset;

    public Vector3 Offset { set { m_Offset = value; } }

    void OnEnable()
    {
        gameObject.transform.position = m_FollowObject.transform.position + m_Offset;
        m_NewOffset = m_Offset;
        transform.LookAt(m_FollowObject.transform.position);
        Snowball.GrowSize += UpdateOffset;
    }

	void Update () 
    {
        transform.position = m_FollowObject.transform.position + m_Offset;
        transform.LookAt(m_FollowObject.transform.position);
        if (m_Offset != m_NewOffset)
        {
            m_Offset = Vector3.Lerp(m_Offset, m_NewOffset, Time.deltaTime);
        }
	}

    void UpdateOffset(int num)
    {
        m_NewOffset.y = num - 1;
        m_NewOffset.z = -num;
    }

    void OnDisable()
    {
        Snowball.GrowSize -= UpdateOffset;
    }
}
