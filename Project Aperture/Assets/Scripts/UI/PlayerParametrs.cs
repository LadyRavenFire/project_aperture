using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerParametrs : MonoBehaviour
{
    public string PlayerName = "Player";
    public string PlayerPanelName = "UserPanel";

    private GameObject _player;

    private GameObject _playerPanel;
    private GameObject _winPanel;
    private GameObject _deathPanel;
    private GameObject _pausePanel;

    public Text FoodHave;
    public Text WaterHave;
    public Text RadioPartsHave;
    public Text RadioPartsMax;

    private Button _mainMenuButton;
    private Button _restartButton;

    private CollectRadioPart _collectRadioPart;
    private FoodNeeds _foodNeeds;
    private WaterNeeds _waterNeeds;

    private bool _isGamePaused;

    void Start()
    {
        _mainMenuButton = GameObject.Find("GoToMainMenuButton").GetComponent<Button>();
        _mainMenuButton.onClick.AddListener(GoToMainMenu);

        _restartButton = GameObject.Find("RestartButton").GetComponent<Button>();
        _restartButton.onClick.AddListener(RestartLevel);

        _player = GameObject.Find(PlayerName);
        _playerPanel = GameObject.Find(PlayerPanelName);

        _winPanel = GameObject.Find("WinPanel");
        _winPanel.SetActive(false);

        _deathPanel = GameObject.Find("DeathPanel");
        _deathPanel.SetActive(false);

        _pausePanel = GameObject.Find("PausePanel");
        _pausePanel.SetActive(false);
        _isGamePaused = false;

        _collectRadioPart = _player.GetComponent<CollectRadioPart>();
        _foodNeeds = _player.GetComponent<FoodNeeds>();
        _waterNeeds = _player.GetComponent<WaterNeeds>();

        RadioPartsMax.text = "/ " + _collectRadioPart.ReturnMax();
    }

    void Update()
    {
        UpdateText();
        CheckForWin();
        CheckForDeath();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isGamePaused == false)
            {
                _pausePanel.SetActive(true);
                _isGamePaused = true;
            }
            else
            {
                _pausePanel.SetActive(false);
                _isGamePaused = false;
            }
        }
    }

    void CheckForWin()
    {
        if (_collectRadioPart.ReturnHave() == _collectRadioPart.ReturnMax())
        {
            _playerPanel.SetActive(false);
            _winPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void CheckForDeath()
    {
        if (_foodNeeds.ReturnFood() < 0f || _waterNeeds.ReturnWater() < 0f)
        {
            _playerPanel.SetActive(false);
            _deathPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void UpdateText()
    {
        RadioPartsHave.text = "RadioParts: " + _collectRadioPart.ReturnHave();
        FoodHave.text = "Food: " + String.Format("{0:0.00}", _foodNeeds.ReturnFood());
        WaterHave.text = "Water: " + String.Format("{0:0.00}",_waterNeeds.ReturnWater());
    }

    void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }


}
