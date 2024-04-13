using System;
using System.Collections;
using UnityEngine;

public class ShootCannon : MonoBehaviour
{
    [SerializeField] float m_Speed;
    [SerializeField] float m_Angle;
    [SerializeField] float m_DefaultAngle;
    [SerializeField] int m_Rounds;
    bool isFiring = false;
    [SerializeField] Transform m_CamPos;
    [SerializeField] GameObject m_Ball;
    [SerializeField] GameObject m_Marker;
    [SerializeField] Transform m_CannonPos;
    [SerializeField] Transform[] m_RightCannons;
    [SerializeField] Transform[] m_LeftCannons;
    private void Start()
    {
        m_Angle = m_DefaultAngle;
    }
    void Update()
    {
        if (!isFiring)
        {
            if (Input.GetMouseButton(0))
            {
                m_Angle += Input.GetAxis("Mouse Y");
                m_Angle = Mathf.Clamp(m_Angle, 20, 80);
                if (Input.GetAxis("Mouse Y") != 0)
                {
                    ShowMarker();
                }

            }
            else if (Input.GetMouseButtonUp(0))
            {
                for (int i = 0; i < m_CannonPos.childCount; i++)
                {
                    Destroy(m_CannonPos.GetChild(i).gameObject);
                }
                if(m_CamPos.rotation.y > 0)
                {
                    StartCoroutine(FireRight());
                }
                else
                {
                    StartCoroutine(FireLeft());
                }
            }
        }
    }

    void ShowMarker()
    {
        float xVel = m_Speed * Mathf.Cos(m_Angle * Mathf.Deg2Rad);
        float yVel = m_Speed * Mathf.Sin(m_Angle * Mathf.Deg2Rad);

        float time = yVel / -Physics.gravity.y;
        float xPos = xVel * time;
        if(m_CamPos.rotation.y > 0)
        {
            for (int x = 0; x <= 2 * xPos; x += 2)
            {
                float yCod = x * Mathf.Tan(m_Angle * Mathf.Deg2Rad) + (0.5f * Physics.gravity.y * ((x * x) / (xVel * xVel)));
                if (x / 2 < m_CannonPos.childCount)
                {
                    m_CannonPos.GetChild(x / 2).position = transform.position + new Vector3(x, yCod, 0);
                }
                else
                {
                    Instantiate(m_Marker, transform.position + new Vector3(x, yCod, 0), Quaternion.identity, m_CannonPos);
                }
            }
        }
        else
        {
            for (int x = 0; x <= 2 * xPos; x += 2)
            {
                float yCod = x * Mathf.Tan(m_Angle * Mathf.Deg2Rad) + (0.5f * Physics.gravity.y * ((x * x) / (xVel * xVel)));
                if (x / 2 < m_CannonPos.childCount)
                {
                    m_CannonPos.GetChild(x / 2).position = transform.position + new Vector3(-x, yCod, 0);
                }
                else
                {
                    Instantiate(m_Marker, transform.position + new Vector3(-x, yCod, 0), Quaternion.identity, m_CannonPos);
                }
            }
        }
        for(int i = 0; i < m_CannonPos.childCount - (int)xPos; i++)
        {
            Destroy(m_CannonPos.GetChild((int)xPos + i).gameObject);
        }
    }

    IEnumerator FireRight()
    {
        isFiring = true;
        for(int count = 0; count < m_Rounds; count++)
        {
            foreach(Transform point in m_RightCannons)
            {
                point.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Sin(m_Angle * Mathf.Deg2Rad) - 90);
                GameObject _ball = Instantiate(m_Ball, point.position, point.rotation);
                _ball.GetComponent<Rigidbody>().velocity = _ball.transform.up * m_Speed;

                yield return new WaitForSeconds(0.2f);
            }
        }
        isFiring = false;
    }
    IEnumerator FireLeft()
    {
        isFiring = true;
        for (int count = 0; count < m_Rounds; count++)
        {
            foreach (Transform point in m_LeftCannons)
            {
                point.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Sin(m_Angle * Mathf.Deg2Rad));
                GameObject _ball = Instantiate(m_Ball, point.position, point.rotation);
                _ball.GetComponent<Rigidbody>().velocity = _ball.transform.up * m_Speed;

                yield return new WaitForSeconds(0.2f);
            }
        }
        isFiring = false;
    }
}
