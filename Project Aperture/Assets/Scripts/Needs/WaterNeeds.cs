﻿using UnityEngine;

public class WaterNeeds : MonoBehaviour
{
    [SerializeField] private float _waterAmount;

    public float WaterMax = 100f;
    public float WaterSpendInSecond = 0.02f;

    public string WaterTag = "Water";

    void Start()
    {
        _waterAmount = WaterMax;
    }

    void FixedUpdate()
    {
        WaterSpending();
    }

    public float ReturnWaterNeed()
    {
        return (WaterMax - _waterAmount);
    }

    public void AddWater(float add)
    {
        _waterAmount += add;
    }

    void WaterSpending()
    {
        _waterAmount = _waterAmount - WaterSpendInSecond;
    }

    public float ReturnWater()
    {
        return _waterAmount;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == WaterTag)
        {
            WaterHave addWater = col.gameObject.GetComponent<WaterHave>();
            if (addWater.ReturnWater() < (WaterMax - _waterAmount))
            {
                _waterAmount = _waterAmount + addWater.ReturnWater();
                addWater.WaterDelete(addWater.ReturnWater());
            }
            else
            {
                addWater.WaterDelete((WaterMax - _waterAmount));
                _waterAmount = WaterMax;
            }
        }
    }
}
