using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using EasyTransition;

public class TimerController : MonoBehaviour
{
    public TMP_Text timerText;
    public Button stopButton;
    public AudioSource audioSource;

    public float startTime = 25.0f;
    private bool isTimerRunning = false;
    public ParticleSystem explosion;
    public AudioClip explosionSound;
    public TransitionManager transitionManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerText.text = "<mspace=0.50em>" + startTime.ToString("F2") + "</mspace>";
        stopButton.interactable = false;
        explosion.Stop();
    }

    private void ShakeTimer()
    {
        timerText.transform.DOShakePosition(20.0f, 15.0f, 15, 90.0f, false, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning)
        {
            UpdateTimer();
        }
    }

    public void StartTimer()
    {
        Invoke("ShakeTimer", 15.0f);
        stopButton.interactable = true;
        audioSource.Play();
        timerText.text = startTime.ToString();
        isTimerRunning = true;
    }

    private void UpdateTimer()
    {
        float currentTime = Time.deltaTime;
        if(startTime <= 0)
        {
            StopTimer();
            return;
        }

        startTime -= currentTime;
        string time = "<mspace=0.50em>" + startTime.ToString("F2") + "</mspace>";
        if(startTime < 10.0f)
        {
            time = "<mspace=0.50em>0" + startTime.ToString("F2") + "</mspace>";
        }
        timerText.text = time;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        stopButton.interactable = false;
        audioSource.Stop();
        CancelInvoke("ShakeTimer");
        timerText.transform.DOKill();

        if(startTime >= 1.0f)
        {
            timerText.text = "Too Early!\n\nBye bye";
            timerText.fontSize = 170;
            explosion.Play();
            audioSource.PlayOneShot(explosionSound);
            transitionManager.GetComponent<DemoLoadScene>().LoadScene("Scene_TimeStone");
        } 
        else if(startTime <= 0)
        {
            timerText.text = "Too Late!\n\nBye bye";
            timerText.fontSize = 170;
            explosion.Play();
            audioSource.PlayOneShot(explosionSound);

            transitionManager.GetComponent<DemoLoadScene>().LoadScene("Ending");
        }
        else
        {
            timerText.text = " Great timing. ";
            timerText.fontSize = 170;
            transitionManager.GetComponent<DemoLoadScene>().LoadScene("Scene_TimeStone");
        }
    }
}
