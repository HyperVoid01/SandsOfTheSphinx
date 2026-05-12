using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score;
    public int lives = 3;
    
    public PlayerData playerData;

    private RiddleData currentRiddle;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GenerateNewRiddle();
    }

    public void GenerateNewRiddle()
    {
        UIManager.Instance.ShowLoading(true);

        OllamaManager.Instance.GenerateRiddle((riddle) =>
        {
            currentRiddle = riddle;

            UIManager.Instance.ShowLoading(false);
            UIManager.Instance.DisplayRiddle(riddle);
        });
    }

    public void SubmitAnswer(int index)
    {
        if (index == currentRiddle.correctIndex)
        {
            score++;
            
            AudioManager.Instance.Play("Correct");

            UIManager.Instance.SetResultText(
                "Correct! The Sphinx is pleased."
            );
        }
        else
        {
            lives--;
            UIManager.Instance.UpdateLives(lives);
            
            AudioManager.Instance.Play("Wrong");
            
            UIManager.Instance.SetResultText(
                "Wrong! The Sphinx devours your pride."
            );
        }

        UIManager.Instance.UpdateScore(score);

        if (lives == 0)
        {
            if (score > playerData.highScore)
                playerData.highScore = score;
            
            UIManager.Instance.ShowGameOver();
        }

        Invoke(nameof(GenerateNewRiddle), 0.5f);
    }
}