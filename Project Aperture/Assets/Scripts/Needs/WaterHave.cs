using UnityEngine;

public class WaterHave : MonoBehaviour {

    [SerializeField] private float _waterHave;

    // Use this for initialization
    void Start()
    {
        _waterHave = 0f;
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
        return giveFood;
    }

    void WaterGeneration()
    {
        _waterHave = _waterHave + 0.01f;

    }
}
