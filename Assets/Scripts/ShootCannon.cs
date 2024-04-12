using System;
using UnityEngine;

public class ShootCannon : MonoBehaviour
{
    [SerializeField] float m_Speed;
    [SerializeField] float m_Angle;
    [SerializeField] float m_DefaultAngle;
    [SerializeField] GameObject m_Marker;
    [SerializeField] Transform m_CannonPos;
    private void Start()
    {
        m_Angle = m_DefaultAngle;
    }
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            m_Angle += Input.GetAxis("Mouse Y");
            m_Angle = Mathf.Clamp(m_Angle, 20, 89);
            if(Input.GetAxis("Mouse Y") != 0)
            {
                ShowMarker();
            }
            
        }
        else if(Input.GetMouseButtonUp(0))
        {
            for(int i = 0; i < m_CannonPos.childCount; i++)
            {
                Destroy(m_CannonPos.GetChild(i).gameObject);
            }
        }
    }

    void ShowMarker()
    {
        float xVel = m_Speed * Mathf.Cos(m_Angle * Mathf.Deg2Rad);
        float yVel = m_Speed * Mathf.Sin(m_Angle * Mathf.Deg2Rad);

        float time = yVel / -Physics.gravity.y;
        float xPos = yVel * time;
        for (int x = 0; x <= 2 * xPos; x+=2)
        {
            float yCod = x * Mathf.Tan(m_Angle * Mathf.Deg2Rad) + (0.5f * Physics.gravity.y * ((x * x) / (xVel * xVel)));
            if (x < m_CannonPos.childCount)
            {
                m_CannonPos.GetChild(x/2).position = transform.position + new Vector3(x, yCod, 0);
            }
            else
            {
                Instantiate(m_Marker, transform.position + new Vector3(x, yCod, 0), Quaternion.identity, m_CannonPos);
            }
        }
    }
}
