using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    private RawImage _backgroundImage;

    private GameObject _mainMenuButtonsPanel;
    private GameObject _authorsPanel;
    private GameObject _settingPanel;
    private GameObject _newGamePanel;

    private Button _newGameButton;
    private Button _continueButton;
    private Button _settingsButton;
    private Button _authorsButton;
    private Button _quitButton;

    private Button _backFromAuthorsButton;

    private Button _backFromSettingsButton;

    private Button _startNewGameButton;
    private Button _backFromNewGameButton;

    private InputField _sizeOfMapInputField;


	void Start ()
	{
	    _backgroundImage = GameObject.Find("BackgroundImage").GetComponent<RawImage>();

        _newGamePanel = GameObject.Find("NewGamePanel");
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

	    _startNewGameButton = GameObject.Find("StartNewGameButton").GetComponent<Button>();
	    _backFromNewGameButton = GameObject.Find("BackFromNewGameButton").GetComponent<Button>();

	    _sizeOfMapInputField = GameObject.Find("SizeOfMapInputField").GetComponent<InputField>();

        _newGameButton.onClick.AddListener(NewGame);
        _continueButton.onClick.AddListener(Continue);
        _settingsButton.onClick.AddListener(Settings);
        _authorsButton.onClick.AddListener(Authors);
        _quitButton.onClick.AddListener(Quit);

        _backFromAuthorsButton.onClick.AddListener(BackFromAuthors);

        _backFromSettingsButton.onClick.AddListener(BackFromSettings);

        _startNewGameButton.onClick.AddListener(StartNewGame);
        _backFromNewGameButton.onClick.AddListener(BackFromNewGame);

        _authorsPanel.SetActive(false);
        _settingPanel.SetActive(false);
        _newGamePanel.SetActive(false);
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

    void BackFromNewGame()
    {
        _newGamePanel.SetActive(false);
        _mainMenuButtonsPanel.SetActive(true);
        _backgroundImage.texture = Resources.Load<Texture>("MainMenu/Images/MainMenu-1");
    }

    void StartNewGame()
    {
        string Number = _sizeOfMapInputField.text;
        PlayerPrefs.SetInt("SizeOfMap", int.Parse(Number));
        SceneManager.LoadScene(1);
    }

    void Settings()
    {
        _backgroundImage.texture = Resources.Load<Texture>("MainMenu/Images/MainMenu-2");
        _mainMenuButtonsPanel.SetActive(false);
        _settingPanel.SetActive(true);
    }

    void NewGame()
    {
        _backgroundImage.texture = Resources.Load<Texture>("MainMenu/Images/MainMenu-2");
        _mainMenuButtonsPanel.SetActive(false);
        _newGamePanel.SetActive(true);
    }

    void Continue()
    {

    }

    void Quit()
    {
        Application.Quit();
    }
}
