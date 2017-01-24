using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    protected Snowball m_Snowball;
    [SerializeField]
    protected Transform m_CollectedObjects;
    protected Vector3 m_MoveVector = new Vector3();
    protected Vector3 m_TempVector = new Vector3();

    protected bool moveForward;
    protected bool moveBackward;
    protected bool moveLeft;
    protected bool moveRight;
    protected bool jump;
    protected bool burst;
    protected bool isSnowball = true;

    public Snowball Snowball { get { return m_Snowball; } }
    public Transform CollectedObjects { get { return m_CollectedObjects; } }
    public Vector3 MoveDir { get { return m_MoveVector; } }

    void Start()
    {
        m_Snowball = GetComponentInChildren<Snowball>();
    }

	void Update () 
    {
        GetInput();
        UpdateMovement();
	}

    protected void GetInput()
    {
        moveForward = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) ? true : false;
        moveBackward = (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) ? true : false;
        moveLeft = (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) ? true : false;
        moveRight = (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) ? true : false;
        jump = Input.GetKeyDown(KeyCode.Space) ? true : false;
        burst = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? true : false;
    }

    protected void UpdateMovement()
    {
        float x = Input.GetAxis("Horizontal") * 100 * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * 100 * Time.deltaTime;

        m_MoveVector = new Vector3(x, 0, z);

        if (!moveForward && !moveBackward && !moveLeft && !moveRight)
        {
            m_MoveVector = Vector3.zero;
        }

        if (isSnowball && m_Snowball)
        {
            m_Snowball.Move(m_MoveVector, jump, burst);
        }
    }
}
