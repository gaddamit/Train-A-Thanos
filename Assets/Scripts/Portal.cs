using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal portalPartner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            Vector3 inVector = other.transform.position - transform.position;
            if(GetComponent<AudioSource>() != null)
            {
                GetComponent<AudioSource>().Play();
            }
            if(portalPartner != null)
            {
                float linearVelocity = other.GetComponent<Rigidbody>().linearVelocity.magnitude;
                other.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                other.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                other.transform.position = portalPartner.transform.position + inVector;
                //other.GetComponent<Rigidbody>().linearVelocity = portalPartner.transform.up * 1.5f;    
                other.GetComponent<Rigidbody>().AddForce(portalPartner.transform.up * 3.0f, ForceMode.VelocityChange);
            }
        }
    }
}
