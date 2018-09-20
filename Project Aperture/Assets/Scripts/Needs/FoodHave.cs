using UnityEngine;

public class FoodHave : MonoBehaviour {

    [SerializeField] private float _foodHave;

    // Use this for initialization
    void Start ()
    {
        _foodHave = 0f;
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
        _foodHave = _foodHave + 0.005f;

    }
}
