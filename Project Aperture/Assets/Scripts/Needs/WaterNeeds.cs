using UnityEngine;

public class WaterNeeds : MonoBehaviour {

    [SerializeField] private float _waterAmount;

    // Use this for initialization
    void Start()
    {
        _waterAmount = 100;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        WaterSpending();
    }

    void WaterSpending()
    {
        _waterAmount = _waterAmount - 0.01f;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            WaterHave AddWater = collision.gameObject.GetComponent<WaterHave>();
            _waterAmount = _waterAmount + AddWater.SMBDrinkWater();
        }
    }
}
