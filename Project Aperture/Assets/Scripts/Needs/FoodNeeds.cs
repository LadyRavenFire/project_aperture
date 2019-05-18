using UnityEngine;

public class FoodNeeds : MonoBehaviour
{
    [SerializeField] private float _foodAmount;

    public float FoodMax = 100f;
    public float FoodSendInSecond = 0.02f;

    public string FoodTag = "Food";

    // Use this for initialization
    void Start()
    {
        _foodAmount = FoodMax;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FoodSpending();
    }

    void FoodSpending()
    {
        _foodAmount = _foodAmount - FoodSendInSecond;
    }

    public float ReturnFood()
    {
        return _foodAmount;
    }

    public void AddFood(float add)
    {
        if (_foodAmount + add > 100f)
        {
            _foodAmount = 100f;
        }
        else
        {
            _foodAmount += add;
        }
    }

    public float ReturnFoodNeeds()
    {
        return (FoodMax - _foodAmount);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == FoodTag)
        {
            FoodHave AddFood = col.gameObject.GetComponent<FoodHave>();
            if (AddFood.ReturnFood() < (FoodMax - _foodAmount))
            {
                _foodAmount = _foodAmount + AddFood.ReturnFood();
                AddFood.FoodDelete(AddFood.ReturnFood());
            }
            else
            {
                AddFood.FoodDelete((FoodMax - _foodAmount));
                _foodAmount = FoodMax;
            }
        }
    }
}
