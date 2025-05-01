using EasyTransition;
using UnityEngine;

public class PortalGoal : MonoBehaviour
{
    public TransitionManager transitionManager;
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
            Destroy(other.gameObject);
            GetComponent<AudioSource>().Play();
            transitionManager.GetComponent<DemoLoadScene>().LoadScene("InBetween1");
        }
    }
}
