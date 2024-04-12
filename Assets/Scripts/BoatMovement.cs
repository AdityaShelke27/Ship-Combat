using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [SerializeField] float m_FloatForce;
    [SerializeField] Transform m_Water;
    [SerializeField] float m_WaterPos;
    [SerializeField] float m_BoatOffset;
    [SerializeField] float m_Speed;
    Rigidbody m_Rb;
    private void Awake()
    {
        m_Rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        m_Water.transform.position = new Vector3(0, m_WaterPos, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if(IsInWater())
        {
            ApplyForce();
        }
    }

    void Movement()
    {
        float hor = Input.GetAxisRaw("Horizontal") * Time.deltaTime * m_Speed;
        float ver = Input.GetAxisRaw("Vertical") * Time.deltaTime * m_Speed;

        m_Rb.AddForce(transform.forward * ver);
        m_Rb.AddTorque(transform.up * hor);
    }
    private void ApplyForce()
    {
        float appliedForce = m_FloatForce * (m_WaterPos - transform.position.y - m_BoatOffset); 
        m_Rb.AddForce(transform.up * appliedForce * Time.deltaTime);
        
    }
    bool IsInWater()
    {
        return transform.position.y + m_BoatOffset < m_WaterPos;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.up * m_BoatOffset);
    }
}
