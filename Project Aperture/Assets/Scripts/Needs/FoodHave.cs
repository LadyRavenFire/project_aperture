using UnityEngine;

public class FoodHave : MonoBehaviour {

    [SerializeField] private float _foodHave;

    private LocationGenerator _locationGenerator;

    // Use this for initialization
    void Start()
    {
        _locationGenerator = GameObject.Find("GenerationManager").GetComponent<LocationGenerator>();
        _foodHave = _locationGenerator.Rand.Next(0, 20);
    }

    // Update is called once per frame
    void Update () {
		FoodSGeneration();
	}

    public float SMBEatFood()
    {
        float giveFood = _foodHave;
        _foodHave = 0f;
        return giveFood;
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
