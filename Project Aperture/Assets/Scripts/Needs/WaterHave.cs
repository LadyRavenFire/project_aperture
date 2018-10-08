using UnityEngine;

public class WaterHave : MonoBehaviour
{
    [SerializeField] private float _waterHave;

    public float WaterTick = 0.03f;

    public int StartWaterMax = 20;
    public int StartWaterMin = 0;

    private LocationGenerator _locationGenerator;

    void Start()
    {
        _locationGenerator = GameObject.Find("GenerationManager").GetComponent<LocationGenerator>();
        _waterHave = _locationGenerator.Rand.Next(StartWaterMin, StartWaterMax);
    }

    void FixedUpdate()
    {
        WaterGeneration();
    }

    void WaterGeneration()
    {
        _waterHave += WaterTick;
    }

    public void WaterDelete(float delete)
    {
        _waterHave = _waterHave - delete;
    }

    public float ReturnWater()
    {
        return _waterHave;
    }
}
