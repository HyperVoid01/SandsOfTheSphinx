using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject tutorialMenu;
    
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TMP_Text highScoreText;

    private void Start()
    {
        highScoreText.text = playerData.highScore.ToString();

        musicSlider.value = playerData.musicVolume;
        sfxSlider.value = playerData.sfxVolume;
    }
    
    private void Update()
    {
        UpdateVolume();
    }
    
    public void StartGame()
    {
        AudioManager.Instance.Play("ButtonClick");
        
        SceneManager.LoadScene(1);
    }

    public void ShowSettings()
    {
        AudioManager.Instance.Play("ButtonClick");
        
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        
        musicSlider.value = playerData.musicVolume;
        sfxSlider.value = playerData.sfxVolume;
    }

    public void ShowMainMenu()
    {
        AudioManager.Instance.Play("ButtonClick");
        
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        tutorialMenu.SetActive(false);
    }

    public void ShowTutorial()
    {
        AudioManager.Instance.Play("ButtonClick");
        
        mainMenu.SetActive(false);
        tutorialMenu.SetActive(true);
    }

    private void UpdateVolume()
    {
        playerData.musicVolume = musicSlider.value;
        playerData.sfxVolume = sfxSlider.value;
        
        AudioManager.Instance.ChangeVolume("Music", playerData.musicVolume);
        AudioManager.Instance.ChangeVolume("ButtonClick", playerData.sfxVolume);
        AudioManager.Instance.ChangeVolume("Correct", playerData.sfxVolume);
        AudioManager.Instance.ChangeVolume("Wrong", playerData.sfxVolume);
    }

    public void QuitGame()
    {
        AudioManager.Instance.Play("ButtonClick");
        
        Application.Quit();
    }
}
