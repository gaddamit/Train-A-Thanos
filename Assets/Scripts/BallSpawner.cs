using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    private Ball ballPrefab;
    [SerializeField]
    private Ball reference;
    private Ball ballInstance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBall()
    {
        if(ballInstance != null)
        {
            return;
        }

        ballInstance = Instantiate(ballPrefab, reference.transform.position, Quaternion.identity, transform.parent);
        ballInstance.onBallDestroyed.AddListener(OnBallDestroyed);
        Invoke(nameof(EnableBall), 1f);
    }

    private void EnableBall()
    {
        ballInstance.GetComponent<Rigidbody>().useGravity = true;
    }

    private void OnBallDestroyed()
    {
        Invoke(nameof(SpawnBall), 1f);
    
    }

    public void DestroyBall()
    {
        if(ballInstance == null)
        {
            return;
        }
        Destroy(ballInstance.gameObject);
    }
}
