using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    private RawImage _backgroundImage;

    private GameObject _mainMenuButtonsPanel;
    private GameObject _authorsPanel;
    private GameObject _settingPanel;

    private Button _newGameButton;
    private Button _continueButton;
    private Button _settingsButton;
    private Button _authorsButton;
    private Button _quitButton;

    private Button _backFromAuthorsButton;

    private Button _backFromSettingsButton;


	void Start ()
	{
	    _backgroundImage = GameObject.Find("BackgroundImage").GetComponent<RawImage>();

        _mainMenuButtonsPanel = GameObject.Find("MainMenuPanel");
	    _authorsPanel = GameObject.Find("AuthorsPanel");
        _settingPanel = GameObject.Find("SettingsPanel");

	    _newGameButton = GameObject.Find("NewGameButton").GetComponent<Button>();
	    _continueButton = GameObject.Find("ContinueButton").GetComponent<Button>();
	    _settingsButton = GameObject.Find("SettingsButton").GetComponent<Button>();
	    _authorsButton = GameObject.Find("AuthorsButton").GetComponent<Button>();
	    _quitButton = GameObject.Find("QuitButton").GetComponent<Button>();

	    _backFromAuthorsButton = GameObject.Find("BackFromAuthorsButton").GetComponent<Button>();
	    _backFromSettingsButton = GameObject.Find("BackFromSettingButton").GetComponent<Button>();

        _newGameButton.onClick.AddListener(NewGame);
        _continueButton.onClick.AddListener(Continue);
        _settingsButton.onClick.AddListener(Settings);
        _authorsButton.onClick.AddListener(Authors);
        _quitButton.onClick.AddListener(Quit);

        _backFromAuthorsButton.onClick.AddListener(BackFromAuthors);
        _backFromSettingsButton.onClick.AddListener(BackFromSettings);

        _authorsPanel.SetActive(false);
        _settingPanel.SetActive(false);
	}

    void Authors()
    {
        _backgroundImage.texture = Resources.Load<Texture>("MainMenu/Images/MainMenu-2");
        _mainMenuButtonsPanel.SetActive(false);
        _authorsPanel.SetActive(true);
    }

    void BackFromAuthors()
    {
        _authorsPanel.SetActive(false);
        _mainMenuButtonsPanel.SetActive(true);
        _backgroundImage.texture = Resources.Load<Texture>("MainMenu/Images/MainMenu-1");
    }

    void BackFromSettings()
    {
        _settingPanel.SetActive(false);
        _mainMenuButtonsPanel.SetActive(true);
        _backgroundImage.texture = Resources.Load<Texture>("MainMenu/Images/MainMenu-1");
    }

    void Settings()
    {
        _backgroundImage.texture = Resources.Load<Texture>("MainMenu/Images/MainMenu-2");
        _mainMenuButtonsPanel.SetActive(false);
        _settingPanel.SetActive(true);
    }

    void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    void Continue()
    {

    }

    void Quit()
    {
        Application.Quit();
    }
}
