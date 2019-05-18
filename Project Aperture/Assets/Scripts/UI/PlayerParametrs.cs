using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerParametrs : MonoBehaviour
{
    public string PlayerName = "Player";
    public string PlayerPanelName = "UserPanel";

    private GameObject _player;

    private GameObject _playerPanel;
    private GameObject _winPanel;
    private GameObject _deathPanel;

    public Text FoodHave;
    public Text WaterHave;
    public Text RadioPartsHave;
    public Text RadioPartsMax;

    private CollectRadioPart _collectRadioPart;
    private FoodNeeds _foodNeeds;
    private WaterNeeds _waterNeeds;

    void Start()
    {
        _player = GameObject.Find(PlayerName);
        _playerPanel = GameObject.Find(PlayerPanelName);

        _winPanel = GameObject.Find("WinPanel");
        _winPanel.SetActive(false);

        _deathPanel = GameObject.Find("DeathPanel");
        _deathPanel.SetActive(false);

        _collectRadioPart = _player.GetComponent<CollectRadioPart>();
        _foodNeeds = _player.GetComponent<FoodNeeds>();
        _waterNeeds = _player.GetComponent<WaterNeeds>();

        RadioPartsMax.text = "/ " + _collectRadioPart.ReturnMax();
    }

    void Update()
    {
        UpdateText();
        CheckForWin();
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


}
