using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    //Access to the Rigidbody Component
    private Rigidbody m_Rigidbody;

    // Displacement
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

    //Imput
    private bool m_isInputDetected = true;
    private bool m_UpImput;
    private bool m_DownImput;
    private bool m_LeftImput;
    private bool m_RightImput;

    //Movement State
    public bool m_IsRotatingLeft;
    public bool m_IsRotatingRight;

    // Use this for initialization
    void Start () {

        //Get the rigidbody
        m_Rigidbody = GetComponent<Rigidbody>();

        InitializeInput();
    }

    void InitializeInput()
    {
        m_UpImput = false;
        m_DownImput = false;
        m_LeftImput = false;
        m_RightImput = false;
    }

    void InputDetection()
    {

        if (m_isInputDetected)
        {

            #region Gamepad
            if (Input.GetAxis("L_XAxis_" + 1) < 0)
            {
                m_LeftImput = true;
                if (m_IsUsingGamePad == false)
                {
                    m_IsUsingGamePad = true;
                }
            }
            else
            {
                m_LeftImput = false;

            }
            if (Input.GetAxis("L_XAxis_" + 1) > 0)
            {
                m_RightImput = true;
                if (m_IsUsingGamePad == false)
                {
                    m_IsUsingGamePad = true;
                }
            }
            else
            {
                m_RightImput = false;
            }
            if (Input.GetAxis("L_YAxis_" + 1) < 0)
            {
                m_UpImput = true;
                if (m_IsUsingGamePad == false)
                {
                    m_IsUsingGamePad = true;
                }
            }
            else
            {
                m_UpImput = false;
            }
            if (Input.GetAxis("L_YAxis_" + 1) > 0)
            {
                m_DownImput = true;
                if (m_IsUsingGamePad == false)
                {
                    m_IsUsingGamePad = true;
                }
            }
            else
            {
                m_DownImput = false;
            }
            #endregion

            #region Keyboard
            if (Input.GetKey("up") || Input.GetKey(KeyCode.Z))
            {
                m_UpImput = true;
                if (m_IsUsingGamePad == true)
                {
                    m_IsUsingGamePad = false;
                }
            }
            if (Input.GetKeyUp("up") && Input.GetKeyUp(KeyCode.Z))
            {
                m_UpImput = false;
            }

            if (Input.GetKey("down") || Input.GetKey(KeyCode.S))
            {
                m_DownImput = true;
                if (m_IsUsingGamePad == true)
                {
                    m_IsUsingGamePad = false;
                }
            }
            if (Input.GetKeyUp("down") && Input.GetKeyUp(KeyCode.S))
            {
                m_DownImput = false;
            }
            if (Input.GetKey("left") || Input.GetKey(KeyCode.Q))
            {
                m_LeftImput = true;
                if (m_IsUsingGamePad == true)
                {
                    m_IsUsingGamePad = false;
                }
            }
            if (Input.GetKeyUp("left") && Input.GetKeyUp(KeyCode.Q))
            {
                m_LeftImput = false;
            }

            if (Input.GetKey("right") || Input.GetKey(KeyCode.D))
            {
                m_RightImput = true;
                if (m_IsUsingGamePad == true)
                {
                    m_IsUsingGamePad = false;
                }
            }
            if (Input.GetKeyUp("right") && Input.GetKeyUp(KeyCode.D))
            {
                m_RightImput = false;
            }
            #endregion


            if(m_UpImput)
            {
                m_Accel = m_MaxSpeed;
            }
            else if(m_DownImput)
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
        if (m_LeftImput ^ m_RightImput)
        {
                if (m_LeftImput && !m_RightImput)
                {
                    //Rotate Left
                    transform.Rotate(Vector3.down, m_RotateSpeed * Time.deltaTime);

                    m_IsRotatingLeft = true;
                    m_IsRotatingRight = false;



                }

                if (!m_LeftImput && m_RightImput)
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

    // Update is called once per frame
    void Update () {
        //Get the Input
        InputDetection();

        //Set the rotation
        RotateTheMower();

        //Move the mower
        m_CurrentSpeed = m_Accel;

    
        m_Rigidbody.velocity = transform.forward * m_CurrentSpeed;

        m_CurrentSpeed = Mathf.Clamp(m_CurrentSpeed, m_MinSpeed, m_MaxSpeed);
    }
}
