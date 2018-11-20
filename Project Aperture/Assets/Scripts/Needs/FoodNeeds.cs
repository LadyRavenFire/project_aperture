using UnityEngine;

public class FoodNeeds : MonoBehaviour
{
    [SerializeField] private float _foodAmount;

    public float FoodMax = 100f;
<<<<<<< HEAD
    public float FoodSpendInSecond = 0.02f;

    public string FoodTag = "Food";

=======
    public float FoodSendInSecond = 0.02f;

    public string FoodTag = "Food";

    // Use this for initialization
>>>>>>> code-refactorings
    void Start()
    {
        _foodAmount = FoodMax;
    }

<<<<<<< HEAD
=======
    // Update is called once per frame
>>>>>>> code-refactorings
    void FixedUpdate()
    {
        FoodSpending();
    }

    void FoodSpending()
    {
<<<<<<< HEAD
        _foodAmount = _foodAmount - FoodSpendInSecond;
=======
        _foodAmount = _foodAmount - FoodSendInSecond;
>>>>>>> code-refactorings
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
        if (col.gameObject.tag == FoodTag)
        {
<<<<<<< HEAD
            FoodHave addFood = col.gameObject.GetComponent<FoodHave>();
            if (addFood.ReturnFood() < (FoodMax - _foodAmount))
=======
            FoodHave AddFood = col.gameObject.GetComponent<FoodHave>();
            if (AddFood.ReturnFood() < (FoodMax - _foodAmount))
>>>>>>> code-refactorings
            {
                _foodAmount = _foodAmount + addFood.ReturnFood();
                addFood.FoodDelete(addFood.ReturnFood());
            }
            else
            {
<<<<<<< HEAD
                addFood.FoodDelete(FoodMax - _foodAmount);
                _foodAmount = FoodMax;
            }
            /*float eatFood = AddFood.SMBEatFood();
            if ((_foodAmount + eatFood) > 100f)
            {
                _foodAmount = 100f;
=======
                AddFood.FoodDelete((FoodMax - _foodAmount));
                _foodAmount = FoodMax;
>>>>>>> code-refactorings
            }
            else
            {
                _foodAmount = _foodAmount + eatFood;
            }*/
        }
    }
}
