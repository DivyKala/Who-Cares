using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
public class PlayerObsolete : MonoBehaviour
{

    public Texture b_up, b_down, b_left, b_right, b_down_p, b_up_p, b_left_p, b_right_p;
    public float rotateSpeed = 3f;



    private enum ControlMode
    {
        Tank,
        Direct
    }

    [SerializeField]
    private float m_moveSpeed = 2;
    [SerializeField]
    private float m_turnSpeed = 200;
    public Animator m_animator;
    [SerializeField]
    private ControlMode m_controlMode = ControlMode.Direct;
    private float m_currentV = 0;
    private float m_currentH = 0;
    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;
    private readonly float m_backwardsWalkScale = 0.16f;
    private readonly float m_backwardRunScale = 0.66f;
    private Vector3 m_currentDirection = Vector3.zero;
    static float v = 0, h = 0, vp = 0, vn = 0, hp = 0, hn = 0;
    private List<Collider> m_collisions = new List<Collider>();
    private static bool pressed;
    private bool isLitBool = false;
    private PlayerHealth playerHealth;
    private Shooter shooter;
  //  private Vector3 lookDir;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        shooter = GetComponent<Shooter>();
        pressed = false;
        camera_switch.scean1 = true;
      //  lookDir = new Vector3(0f , 0f , 0f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //yatin's part
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if (validSurfaceNormal)
        {
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        }
        else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
        }
 
      
    }



    private void OnCollisionExit(Collision collision)
    {
        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (CrossPlatformInputManager.GetButtonDown("Use")) {
            //   Debug.Log("attempt");

            if (other.gameObject.CompareTag("Lamp")) {
                other.gameObject.GetComponent<ToggleLight>().Use();
            }
       /* TURRETS WERE REMOVED
        *     else if (other.gameObject.CompareTag("Turret")) {
                other.gameObject.transform.FindChild("Head").FindChild("Fire point").GetComponent<Turret>().Fire();
            }
            */
        }
    } 




    void FixedUpdate()
    {
            switch (m_controlMode)
            {
                case ControlMode.Direct:
                    DirectUpdate();
                    break;

                case ControlMode.Tank:
                    TankUpdate();
                    break;

                default:
                    Debug.LogError("Unsupported state");
                    break;
            }
    }
    private void Update() {
        if (CrossPlatformInputManager.GetButton("Shoot") && !playerHealth.isPlayerDead() && camera_switch.scean1 == true) {
            
            m_animator.SetTrigger("shoot");
        }
    } 
    private void TankUpdate()
    {/*
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        if (v > 0f)
        {
            m_animator.SetBool("running", true);
        }
        else
            m_animator.SetBool("running", false);
        bool walk = Input.GetKey(KeyCode.LeftShift);

        if (v < 0)
        {
            if (walk) { v *= m_backwardsWalkScale; }
            else { v *= m_backwardRunScale; }
        }
        else if (walk)
        {
            v *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
        transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);*/
    }

    private void DirectUpdate()
    {
        if (vp != 0)
            v = vp;
        else if (vn != 0)
            v = vn;
        if (hp != 0)
            h = hp;
        else if (hn != 0)
            h = hn;
        if (vp == 0 && vn == 0)
            v = 0;
        if (hp == 0 && hn == 0)
            h = 0;
        if (v != 0 || h != 0)
        {
            m_animator.SetBool("running", true);
        }
        else
        {
            m_animator.SetBool("running", false);
        }
        Transform camera = Camera.main.transform;
        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        //check if player is being rotated by joystick 
        
        Vector3 lookDir = new Vector3(CrossPlatformInputManager.GetAxis("HorizontalLook") , 0f , CrossPlatformInputManager.GetAxis("VerticalLook"));
        if (lookDir != Vector3.zero) {
            lookDir = new Vector3(CrossPlatformInputManager.GetAxis("HorizontalLook") , 0f , CrossPlatformInputManager.GetAxis("VerticalLook")) * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(lookDir);
        }
        else if (direction != Vector3.zero)
        {
            m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

            transform.rotation = Quaternion.LookRotation(m_currentDirection);
            transform.position += m_currentDirection * m_moveSpeed * Time.deltaTime;

        }
        
        

    }
    private void OnGUI()
    {
        GUI.backgroundColor = Color.clear;
        if (GUI.RepeatButton(new Rect(Screen.width / 20, Screen.height / 1.4f, Screen.height / 7, Screen.height / 7), b_right) && pressed == false)
        {
            hn = -1; pressed = true;
            GUI.RepeatButton(new Rect(Screen.width / 20, Screen.height / 1.4f, Screen.height / 7, Screen.height / 7), b_right_p);
        }
        else
        { hn = 0; pressed = false; }
        if (GUI.RepeatButton(new Rect(Screen.width /5f, Screen.height / 1.4f, Screen.height / 7, Screen.height / 7), b_left) && pressed == false)
        {
            hp = 1; pressed = true;
            GUI.RepeatButton(new Rect(Screen.width /5f, Screen.height / 1.4f, Screen.height / 7, Screen.height / 7), b_left_p);
        }
        else
        { hp = 0; pressed = false; }
        if (GUI.RepeatButton(new Rect( Screen.width /8, Screen.height / 2 + Screen.height / 10, Screen.height / 7, Screen.height / 7), b_up) && pressed == false)
        {
            vp = 1; pressed = true;
            GUI.RepeatButton(new Rect(Screen.width / 8, Screen.height / 2 + Screen.height / 10, Screen.height / 7, Screen.height / 7), b_up_p);
        }
        else
        { vp = 0; pressed = false; }
        if (GUI.RepeatButton(new Rect(Screen.width / 8, Screen.height - Screen.height / 6, Screen.height / 7, Screen.height / 7), b_down) && pressed == false)
        {
            GUI.RepeatButton(new Rect(Screen.width / 8, Screen.height - Screen.height / 6, Screen.height / 7, Screen.height / 7), b_down_p);
            vn = -1; pressed = true;
        }
        else
        { vn = 0; pressed = false; }
    }
    
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Litzone"))
        {
            isLitBool = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Litzone"))
        {
            isLitBool = false;
        }
    }
    public bool isLit()
    {
        return isLitBool;
    }

}
