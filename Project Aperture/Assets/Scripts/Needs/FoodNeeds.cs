using UnityEngine;

public class FoodNeeds : MonoBehaviour
{

    [SerializeField]private float _foodAmount;

	// Use this for initialization
	void Start ()
	{
	    _foodAmount = 100;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		FoodSpending();
	}

    void FoodSpending()
    {
        _foodAmount = _foodAmount - 0.02f;
        
    }

    public float ReturnFood()
    {
        return _foodAmount;
    }

    public void AddFood(float add)
    {
        _foodAmount += add;
    }

    public float ReturnFoodNeeds()
    {
        return (100f - _foodAmount);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Food")
        {
            FoodHave AddFood = col.gameObject.GetComponent<FoodHave>();
            if (AddFood.ReturnFood() < (100f - _foodAmount))
            {
                _foodAmount = _foodAmount + AddFood.ReturnFood();
                AddFood.FoodDelete(AddFood.ReturnFood());
            }
            else
            {
                AddFood.FoodDelete((100f - _foodAmount));
                _foodAmount = 100f;
            }
            /*float eatFood = AddFood.SMBEatFood();
            if ((_foodAmount + eatFood) > 100f)
            {
                _foodAmount = 100f;
            }
            else
            {
                _foodAmount = _foodAmount + eatFood;
            }*/
        }
        
    }
}
