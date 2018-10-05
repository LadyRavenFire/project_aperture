using UnityEngine;

public class WaterHave : MonoBehaviour {

    [SerializeField] private float _waterHave;

    private LocationGenerator _locationGenerator;

    // Use this for initialization
    void Start()
    {
        _locationGenerator = GameObject.Find("GenerationManager").GetComponent<LocationGenerator>();
        _waterHave = _locationGenerator.Rand.Next(0, 20);
    }

    // Update is called once per frame
    void Update()
    {
        WaterGeneration();
    }

    public float SMBDrinkWater()
    {
        float giveFood = _waterHave;
        _waterHave = 0f;
         //print(giveFood);
        return giveFood;
    }

    void WaterGeneration()
    {
        _waterHave = _waterHave + 0.03f;

    }

    public void WaterDelete(float _delete)
    {
        _waterHave = _waterHave - _delete;
    }

    public float ReturnWater()
    {
        return _waterHave;
    }
}
