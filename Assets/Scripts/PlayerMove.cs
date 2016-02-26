using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour 
{
    //Access to the Rigidbody Component
    private Rigidbody m_Rigidbody;

    // Movement
    public float m_BaseSpeed;
    public float m_MaxSpeed;
    public float m_MinSpeed;
    public float m_Accel;
    public float m_Deccel;
    public Vector3 m_DisplacementDirection;

    public float m_ClampRotMax;
    public float m_AccelRot;
    public float m_ClampRotMin;

    public float m_CurrentSpeed = 0.0f;
    public float m_CurrentRotation = 0.0f;

    //Rotating speed
    public float m_RotateSpeed;

    //Is the player using GamePad (or Keyboard)
    public bool m_IsUsingGamePad;

    //Input
    private bool m_isInputDetected = true;
    private bool m_UpInput;
    private bool m_DownInput;
    private bool m_LeftInput;
    private bool m_RightInput;

    //Movement State
    public bool m_IsRotatingLeft;
    public bool m_IsRotatingRight;



    // Use this for initialization
    void Start () 
	{
        //Get the rigidbody
        m_Rigidbody = GetComponent<Rigidbody>();

        InitializeInput();
    }



    void InitializeInput()
    {
        m_UpInput = false;
        m_DownInput = false;
        m_LeftInput = false;
        m_RightInput = false;
    }



	// Update is called once per frame
	void Update () 
	{
		//Get the Input
		InputDetection();

		//Set the rotation
		RotateTheMower();

		//Move the mower
		m_CurrentSpeed = m_Accel;

		m_Rigidbody.velocity = transform.forward * m_CurrentSpeed;

		m_CurrentSpeed = Mathf.Clamp(m_CurrentSpeed, m_MinSpeed, m_MaxSpeed);
	}



    void InputDetection()
    {
        if (m_isInputDetected)
        {
            #region Gamepad
            if (Input.GetAxis("L_XAxis_" + 1) < 0)
            {
                m_LeftInput = true;
                if (m_IsUsingGamePad == false)
                {
                    m_IsUsingGamePad = true;
                }
            }

            else
            {
                m_LeftInput = false;

            }


            if (Input.GetAxis("L_XAxis_" + 1) > 0)
            {
                m_RightInput = true;
                if (m_IsUsingGamePad == false)
                {
                    m_IsUsingGamePad = true;
                }
            }

            else
            {
                m_RightInput = false;
            }


            if (Input.GetAxis("L_YAxis_" + 1) < 0)
            {
                m_UpInput = true;
                if (m_IsUsingGamePad == false)
                {
                    m_IsUsingGamePad = true;
                }
            }

            else
            {
                m_UpInput = false;
            }


            if (Input.GetAxis("L_YAxis_" + 1) > 0)
            {
                m_DownInput = true;
                if (m_IsUsingGamePad == false)
                {
                    m_IsUsingGamePad = true;
                }
            }

            else
            {
                m_DownInput = false;
            }
            #endregion

            #region Keyboard
            if (Input.GetKey("up") || Input.GetKey(KeyCode.Z))
            {
                m_UpInput = true;
                if (m_IsUsingGamePad == true)
                {
                    m_IsUsingGamePad = false;
                }
            }


            if (Input.GetKeyUp("up") && Input.GetKeyUp(KeyCode.Z))
            {
                m_UpInput = false;
            }


            if (Input.GetKey("down") || Input.GetKey(KeyCode.S))
            {
                m_DownInput = true;
                if (m_IsUsingGamePad == true)
                {
                    m_IsUsingGamePad = false;
                }
            }


            if (Input.GetKeyUp("down") && Input.GetKeyUp(KeyCode.S))
            {
                m_DownInput = false;
            }


            if (Input.GetKey("left") || Input.GetKey(KeyCode.Q))
            {
                m_LeftInput = true;
                if (m_IsUsingGamePad == true)
                {
                    m_IsUsingGamePad = false;
                }
            }


            if (Input.GetKeyUp("left") && Input.GetKeyUp(KeyCode.Q))
            {
                m_LeftInput = false;
            }


            if (Input.GetKey("right") || Input.GetKey(KeyCode.D))
            {
                m_RightInput = true;
                if (m_IsUsingGamePad == true)
                {
                    m_IsUsingGamePad = false;
                }
            }


            if (Input.GetKeyUp("right") && Input.GetKeyUp(KeyCode.D))
            {
                m_RightInput = false;
            }
            #endregion

            if(m_UpInput)
            {
                m_Accel = m_MaxSpeed;
            }

            else if(m_DownInput)
            {
                m_Accel = -m_MaxSpeed;
            }

            else
            {
                m_Accel = 0;
            }
		}
    }



    void RotateTheMower()
    {
        m_Rigidbody.angularVelocity = Vector3.zero;

        if (m_LeftInput ^ m_RightInput)
        {
                if (m_LeftInput && !m_RightInput)
                {
                    //Rotate Left
                    transform.Rotate(Vector3.down, m_RotateSpeed * Time.deltaTime);

                    m_IsRotatingLeft = true;
                    m_IsRotatingRight = false;
                }

                if (!m_LeftInput && m_RightInput)
                {
                    //Rotate Right
                    transform.Rotate(Vector3.up, m_RotateSpeed * Time.deltaTime);

                    m_IsRotatingLeft = false;
                    m_IsRotatingRight = true;
                }
        }

        else
        {
            //Is not rotating
            m_IsRotatingLeft = false;
            m_IsRotatingRight = false;
        }

        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y, 0);
    }
}