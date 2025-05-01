using EasyTransition;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private bool isAI = false;
    private Animator animator;
    public AudioClip punchSoundLeft;
    public AudioClip punchSoundRight;
    public AudioClip[] takeDamageSound;
    public AudioClip victorySound;
    private AudioSource audioSource;
    public Scrollbar healthBar;
    [SerializeField]
    private float health = 1.0f;
    private bool isAlreadyPunching = false;
    private bool isGameOver = false;
    public TransitionManager transitionManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        healthBar.size = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPunching()
    {
        if(isAI && !isAlreadyPunching)
        {
            isAlreadyPunching = true;
            InvokeRepeating("RandomPunch", 2.0f, 2.0f);
        }
    }

    public void StopPunching()
    {
        isAlreadyPunching = false;
        CancelInvoke();
    }

    public void RandomPunch()
    {
        if(isGameOver)
        {
            return;
        }

        if(Random.Range(0, 2) == 0)
        {
            PunchLeft();
        }
        else
        {
            PunchRight();
        }
    }

    public void PunchLeft()
    {
        if(isGameOver)
        {
            return;
        }

        animator.SetTrigger("PunchLeft");
        if(punchSoundLeft != null)
        {
            audioSource.clip = punchSoundLeft;
            audioSource.PlayDelayed(0.5f);
        }
    }

    public void PunchRight()
    {
        if(isGameOver)
        {
            return;
        }

        animator.SetTrigger("PunchRight");
        if(punchSoundRight != null)
        {
            audioSource.PlayOneShot(punchSoundRight);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(!isGameOver && other.gameObject.CompareTag("Opponent"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if(playerController != null)
            {
                float health = playerController.TakeDamage(0.1f);
                if(health <= 0)
                {
                    animator.SetTrigger("Victory");
                    audioSource.PlayOneShot(victorySound);
                    isGameOver = true;
                    transitionManager.GetComponent<DemoLoadScene>().LoadScene("InBetween2");
                }
            }
        }

        if(isAI && !isGameOver && other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if(playerController != null)
            {
                float health = playerController.TakeDamage(0.1f);
                if(health <= 0)
                {
                    animator.SetTrigger("Victory");
                    audioSource.PlayOneShot(victorySound);
                    isGameOver = true;
                    CancelInvoke();
                    transitionManager.GetComponent<DemoLoadScene>().LoadScene("Scene_PowerStone");
                }
            }
        }
    }

    public float TakeDamage(float damage)
    {
        health -= damage;
        healthBar.size = health;
        if(health <= 0)
        {
            CancelInvoke();
            animator.SetTrigger("Defeat");
            isGameOver = true;
        }

        if(takeDamageSound.Length > 0)
        {
            audioSource.PlayOneShot(takeDamageSound[Random.Range(0, takeDamageSound.Length)]);
        }

        return health;
    }
}
