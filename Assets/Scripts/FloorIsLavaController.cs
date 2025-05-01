using EasyTransition;
using TMPro;
using UnityEngine;

public class FloorIsLavaController : MonoBehaviour
{
    private AudioSource audioSource;
    public TMP_Text displayText;
    private int lavaCount = 0;
    public TransitionManager transitionManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddedLava()
    {
        lavaCount++;
        if(audioSource != null)
        {
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

        if(displayText != null)
        {
            if(lavaCount > 5)
            {
                displayText.text = "Toasty. Well done.";
                transitionManager.GetComponent<DemoLoadScene>().LoadScene("InBetween3");
            }
            else
            {
                displayText.text = "The floor is lava.\n Add more.";
            }
        }
    }
}
