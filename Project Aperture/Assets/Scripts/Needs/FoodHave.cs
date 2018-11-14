using UnityEngine;

public class FoodHave : MonoBehaviour
{
    [SerializeField] private float _foodHave;

    public float FoodTick = 0.03f;

    public int StartFoodMax = 20;
    public int StartFoodMin = 0;

    private LocationGenerator _locationGenerator;
    private TimeManager _timeManager;

    public string NameOfGenerationManager = "GenerationManager";
    public string NameOfTimeManager = "LocationManager";

    void Start()
    {
        _locationGenerator = GameObject.Find(NameOfGenerationManager).GetComponent<LocationGenerator>();
        _timeManager = GameObject.Find(NameOfTimeManager).GetComponent<TimeManager>();
        _foodHave = _locationGenerator.Rand.Next(StartFoodMin, StartFoodMax);
    }

    void FixedUpdate()
    {
        ChangeFoodTick();
        FoodSGeneration();
    }

    void ChangeFoodTick()
    {
        if (_timeManager.NowSeason == TimeManager.Season.Spring)
        {
            switch (_timeManager.NameOfTime)
            {
                case TimeManager.TimeName.Morning:
                    FoodTick = 0.02f;
                    break;
                case TimeManager.TimeName.Day:
                    FoodTick = 0.025f;
                    break;
                case TimeManager.TimeName.Evening:
                    FoodTick = 0.01f;
                    break;
                case TimeManager.TimeName.Night:
                    FoodTick = 0.005f;
                    break;
            }
        }

        if (_timeManager.NowSeason == TimeManager.Season.Summer)
        {
            switch (_timeManager.NameOfTime)
            {
                case TimeManager.TimeName.Morning:
                    FoodTick = 0.04f;
                    break;
                case TimeManager.TimeName.Day:
                    FoodTick = 0.05f;
                    break;
                case TimeManager.TimeName.Evening:
                    FoodTick = 0.02f;
                    break;
                case TimeManager.TimeName.Night:
                    FoodTick = 0.01f;
                    break;
            }
        }

        if (_timeManager.NowSeason == TimeManager.Season.Autumn)
        {
            switch (_timeManager.NameOfTime)
            {
                case TimeManager.TimeName.Morning:
                    FoodTick = 0.02f;
                    break;
                case TimeManager.TimeName.Day:
                    FoodTick = 0.025f;
                    break;
                case TimeManager.TimeName.Evening:
                    FoodTick = 0.01f;
                    break;
                case TimeManager.TimeName.Night:
                    FoodTick = 0.005f;
                    break;
            }
        }

        if (_timeManager.NowSeason == TimeManager.Season.Winter)
        {
            switch (_timeManager.NameOfTime)
            {
                case TimeManager.TimeName.Morning:
                    FoodTick = 0f;
                    break;
                case TimeManager.TimeName.Day:
                    FoodTick = 0f;
                    break;
                case TimeManager.TimeName.Evening:
                    FoodTick = 0f;
                    break;
                case TimeManager.TimeName.Night:
                    FoodTick = 0f;
                    break;
            }
        }

    }
    void FoodSGeneration()
    {
        _foodHave = _foodHave + FoodTick;
    }

    public void FoodDelete(float delete)
    {
        _foodHave = _foodHave - delete;
    }

    public float ReturnFood()
    {
        return _foodHave;
    }
}
