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
        _foodAmount = _foodAmount - 0.01f;
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            FoodHave AddFood = collision.gameObject.GetComponent<FoodHave>();
            _foodAmount = _foodAmount + AddFood.SMBEatFood();
        }
    }
}
