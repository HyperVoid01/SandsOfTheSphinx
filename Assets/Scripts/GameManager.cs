using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score;
    public int lives = 3;

    public PlayerData playerData;

    private RiddleData currentRiddle;
    private RiddleData cachedRiddle;
    private bool isCaching = false;
    private Action<RiddleData> pendingRiddleRequest = null;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Show loading only for the very first riddle
        UIManager.Instance.ShowLoading(true);

        OllamaManager.Instance.GenerateRiddle((riddle) =>
        {
            currentRiddle = riddle;
            UIManager.Instance.ShowLoading(false);
            UIManager.Instance.DisplayRiddle(riddle);

            // Immediately start caching the next one in the background
            PreCacheNextRiddle();
        });
    }

    private void PreCacheNextRiddle()
    {
        if (isCaching) return;
        isCaching = true;

        OllamaManager.Instance.GenerateRiddle((riddle) =>
        {
            isCaching = false;

            // If someone already asked for the next riddle, give it to them directly
            if (pendingRiddleRequest != null)
            {
                var pending = pendingRiddleRequest;
                pendingRiddleRequest = null;
                pending.Invoke(riddle);
            }
            else
            {
                cachedRiddle = riddle;
            }
        });
    }

    public void GenerateNewRiddle()
    {
        if (cachedRiddle != null)
        {
            currentRiddle = cachedRiddle;
            cachedRiddle = null;

            UIManager.Instance.ShowLoading(false);
            UIManager.Instance.DisplayRiddle(currentRiddle);

            PreCacheNextRiddle();
        }
        else
        {
            // Background generation is still running — hook into it
            UIManager.Instance.ShowLoading(true);

            pendingRiddleRequest = (riddle) =>
            {
                currentRiddle = riddle;
                UIManager.Instance.ShowLoading(false);
                UIManager.Instance.DisplayRiddle(currentRiddle);

                PreCacheNextRiddle();
            };
        }
    }
    
    public void SubmitAnswer(int index)
    {
        if (index == currentRiddle.correctIndex)
        {
            score++;
            AudioManager.Instance.Play("Correct");
            UIManager.Instance.SetResultText("Correct! The Sphinx is pleased.");
        }
        else
        {
            lives--;
            UIManager.Instance.UpdateLives(lives);
            AudioManager.Instance.Play("Wrong");
            UIManager.Instance.SetResultText("Wrong! The Sphinx devours your pride.");
        }

        UIManager.Instance.ShowAnswerResult(index, currentRiddle.correctIndex); 

        UIManager.Instance.UpdateScore(score);

        if (lives == 0)
        {
            if (score > playerData.highScore)
                playerData.highScore = score;

            UIManager.Instance.ShowGameOver();
            return;
        }

        Invoke(nameof(GenerateNewRiddle), 0.5f);
    }
}