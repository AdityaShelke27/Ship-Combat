using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float m_Sensitivity;
    float rotation = 0;
    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        float hor = Input.GetAxis("Mouse X") * m_Sensitivity * Time.deltaTime;
        rotation += hor;
        transform.rotation = Quaternion.Euler(0, rotation, 0);
    }
}
