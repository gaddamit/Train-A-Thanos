using DG.Tweening;
using EasyTransition;
using UnityEngine;
using UnityEngine.UI;

public class HelmetController : MonoBehaviour
{
    public GameObject visor;
    public ParticleSystem[] mindControlParticles;
    public AudioClip mindControlSound;
    private AudioSource audioSource;
    public Button[] buttons;
    public TransitionManager transitionManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        foreach (ParticleSystem particle in mindControlParticles)
        {
            particle.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTargetFound()
    {
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
    }

    public void OnTargetLost()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
    }

    public void StartMindControl()
    {
        visor.transform.DOLocalRotate(new Vector3(0.0f, 0.0f, 0.0f), 1.0f).OnComplete(() =>
        {
            foreach (ParticleSystem particle in mindControlParticles)
            {
                particle.Play();
            }
            
            audioSource.PlayOneShot(mindControlSound);
            Invoke("InvokeTransition", 5.0f);
        });
    }

    private void InvokeTransition()
    {
        transitionManager.GetComponent<DemoLoadScene>().LoadScene("InBetween4");
    }
}
