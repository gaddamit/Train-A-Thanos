using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TargetController : MonoBehaviour
{
    public BallSpawner ballSpawner;
    public Button[] buttons;
    public PlayerController opponent;
    private void Start()
    {
        
    }

    public void OnTargetFound()
    {
        if(ballSpawner != null)
        {
            ballSpawner.SpawnBall();
        }

        if(opponent != null)
        {
            opponent.StartPunching();
        }

        EnableButtons();
    }

    public void OnTargetLost()
    {
        if(ballSpawner != null)
        {
            ballSpawner.DestroyBall();
        }

        if(opponent != null)
        {
            opponent.StopPunching();
        }

        DisableButtons();
    }

    private void EnableButtons()
    {
        foreach(Button button in buttons)
        {
            button.interactable = true;
        }
    }

    private void DisableButtons()
    {
        foreach(Button button in buttons)
        {
            button.interactable = false;
        }
    }
}