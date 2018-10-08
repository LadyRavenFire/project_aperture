using UnityEngine;

public class FoodHave : MonoBehaviour
{
    [SerializeField] private float _foodHave;

    public float FoodTick = 0.03f;

    public int StartFoodMax = 20;
    public int StartFoodMin = 0;

    private LocationGenerator _locationGenerator;
    public string NameOfGenerationManager = "GenerationManager";

    void Start()
    {
        _locationGenerator = GameObject.Find(NameOfGenerationManager).GetComponent<LocationGenerator>();
        _foodHave = _locationGenerator.Rand.Next(StartFoodMin, StartFoodMax);
    }

    void FixedUpdate()
    {
        FoodSGeneration();
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
