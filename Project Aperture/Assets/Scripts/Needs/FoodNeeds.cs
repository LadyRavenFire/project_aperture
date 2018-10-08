using UnityEngine;

public class FoodNeeds : MonoBehaviour
{
    [SerializeField] private float _foodAmount;

    public float FoodMax = 100f;
    public float FoodSpendInSecond = 0.02f;

    public string FoodTag = "Food";

    void Start()
    {
        _foodAmount = FoodMax;
    }

    void FixedUpdate()
    {
        FoodSpending();
    }

    void FoodSpending()
    {
        _foodAmount = _foodAmount - FoodSpendInSecond;
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
        return (FoodMax - _foodAmount);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Food")
        {
            FoodHave addFood = col.gameObject.GetComponent<FoodHave>();
            if (addFood.ReturnFood() < (FoodMax - _foodAmount))
            {
                _foodAmount = _foodAmount + addFood.ReturnFood();
                addFood.FoodDelete(addFood.ReturnFood());
            }
            else
            {
                addFood.FoodDelete(FoodMax - _foodAmount);
                _foodAmount = FoodMax;
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
