﻿using UnityEngine;

<<<<<<< HEAD
public class WaterNeeds : MonoBehaviour
{
    [SerializeField] private float _waterAmount;

    public float WaterMax = 100f;
    public float WaterSpendInSecond = 0.02f;
=======
public class WaterNeeds : MonoBehaviour {
>>>>>>> parent of 9022eb9... some code changes

    [SerializeField] private float _waterAmount;

    // Use this for initialization
    void Start()
    {
        _waterAmount = 100f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        WaterSpending();
    }

    public float ReturnWaterNeed()
    {
        return (100f - _waterAmount);
    }

    public void AddWater(float add)
    {
        _waterAmount += add;
    }

    void WaterSpending()
    {
<<<<<<< HEAD
        _waterAmount = _waterAmount - WaterSpendInSecond;
=======
        _waterAmount = _waterAmount - 0.02f;

>>>>>>> parent of 9022eb9... some code changes
    }

    public float ReturnWater()
    {
        return _waterAmount;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Water")
        {
            WaterHave AddWater = col.gameObject.GetComponent<WaterHave>();
            if (AddWater.ReturnWater() < (100f -_waterAmount))
            {
                _waterAmount = _waterAmount + AddWater.ReturnWater();
                AddWater.WaterDelete(AddWater.ReturnWater());
            }
            else
            {
                AddWater.WaterDelete((100f - _waterAmount));
                _waterAmount = 100f;
            }

            
            /*float DrinkWater = AddWater.SMBDrinkWater();
            if ((_waterAmount + DrinkWater > 100f))
            {
                _waterAmount = 100f;
            }
            else
            {
               
                _waterAmount = _waterAmount + DrinkWater;
            }*/
        }
               
    }
}
