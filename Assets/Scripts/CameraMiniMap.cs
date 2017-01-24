using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMiniMap : MonoBehaviour {

    [SerializeField]
    private Transform m_FollowTarget;

	void Update () {
        transform.position = new Vector3(m_FollowTarget.position.x, transform.position.y, m_FollowTarget.position.z);
	}
}
