using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Question")]
    public TMP_Text questionText;

    [Header("Answers")]
    public TMP_Text[] answerTexts;
    public Button[] answerButtons;
    
    [Header("Answer Button Sprites")]
    public Sprite correctSprite;
    public Sprite incorrectSprite;

    [Header("UI")]
    public TMP_Text resultText;
    public TMP_Text scoreText;
    public GameObject loadingPanel;
    public TMP_Text livesText;
    public TMP_Text endScoreText;
    
    [Header("Menus")]
    public GameObject hud;
    public GameObject gameOverPanel;
    
    private Sprite defaultButtonSprite;

    private void Awake()
    {
        Instance = this;
        defaultButtonSprite = answerButtons[0].GetComponent<Image>().sprite;
    }
    
    public void DisplayRiddle(RiddleData riddle)
    {
        // Reset button sprites
        foreach (Button btn in answerButtons)
            btn.GetComponent<Image>().sprite = defaultButtonSprite;
        
        questionText.text = riddle.question;
    
        if (riddle.answers == null)
        {
            Debug.LogError("Answers array is NULL");
            return;
        }
    
        for (int i = 0; i < answerTexts.Length; i++)
        {
            if (i < riddle.answers.Length)
            {
                answerTexts[i].text = riddle.answers[i];
            }
            else
            {
                answerTexts[i].text = "Missing Answer";
            }
        }
    
        resultText.text = "";
    }
    
    public void ShowAnswerResult(int selectedIndex, int correctIndex)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].interactable = false;

            Image img = answerButtons[i].GetComponent<Image>();

            if (i == correctIndex)
                img.sprite = correctSprite;
            else if (i == selectedIndex)
                img.sprite = incorrectSprite;
        }
    }


    public void SetResultText(string text)
    {
        resultText.text = text;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int lives)
    {
        livesText.text = lives.ToString();
    }

    public void ShowLoading(bool show)
    {
        loadingPanel.SetActive(show);

        foreach (Button btn in answerButtons)
        {
            btn.interactable = !show;
        }
    }

    public void ShowGameOver()
    {
        hud.SetActive(false);
        gameOverPanel.SetActive(true);
        
        endScoreText.text = scoreText.text;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}