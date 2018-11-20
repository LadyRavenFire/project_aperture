using UnityEngine;

public class FoodHave : MonoBehaviour {

    [SerializeField] private float _foodHave;

    private LocationGenerator _locationGenerator;
    private TimeManager _timeManager;

    public string NameOfGenerationManager = "GenerationManager";
    public string NameOfTimeManager = "LocationManager";

    // Use this for initialization
    void Start()
    {
<<<<<<< HEAD
        _locationGenerator = GameObject.Find("GenerationManager").GetComponent<LocationGenerator>();
        _foodHave = _locationGenerator.Rand.Next(0, 20);
=======
        _locationGenerator = GameObject.Find(NameOfGenerationManager).GetComponent<LocationGenerator>();
        _timeManager = GameObject.Find(NameOfTimeManager).GetComponent<TimeManager>();
        _foodHave = _locationGenerator.Rand.Next(StartFoodMin, StartFoodMax);
>>>>>>> code-refactorings
    }

    // Update is called once per frame
    void Update () {
		FoodSGeneration();
	}

    public float SMBEatFood()
    {
<<<<<<< HEAD
        float giveFood = _foodHave;
        _foodHave = 0f;
        return giveFood;
=======
        ChangeFoodTick();
        FoodSGeneration();
>>>>>>> code-refactorings
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
        _foodHave = _foodHave + 0.03f;

    }

    public void FoodDelete(float _delete)
    {
        _foodHave = _foodHave - _delete;
    }

    public float ReturnFood()
    {
        return _foodHave;
    }


}
