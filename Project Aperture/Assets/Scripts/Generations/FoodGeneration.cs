using UnityEngine;

public class FoodGeneration : MonoBehaviour
{
    private PlaceGeneration _placeGeneration;
    private LocationGenerator _locationGenerator;

    public GameObject FoodBlock;

    public float SizeOfFoodBlock = 1.3f;

    void Start()
    {
        _placeGeneration = GameObject.Find("GenerationManager").GetComponent<PlaceGeneration>();
        _locationGenerator = GameObject.Find("GenerationManager").GetComponent<LocationGenerator>();

        Creation();
    }

    void Creation()
    {
        for (int i = 0; i < _placeGeneration.XCoordsOfFood.Length; i++)
        {
            Instantiate(FoodBlock,
<<<<<<< HEAD
                new Vector3(_placeGeneration.XCoordsOfFood[i] * _locationGenerator.LongOfGrassBlock,
                    -(_placeGeneration.YCoordsOfFood[i] * _locationGenerator.HeightOfGrassBlock), 0),
                Quaternion.identity);

            if (_locationGenerator.Rand.Next(0, 100) % 2 == 0)
            {
                Instantiate(FoodBlock,
                    new Vector3(
                        _placeGeneration.XCoordsOfFood[i] * _locationGenerator.LongOfGrassBlock - SizeOfFoodBlock,
                        -(_placeGeneration.YCoordsOfFood[i] * _locationGenerator.HeightOfGrassBlock) + SizeOfFoodBlock,
                        0), Quaternion.identity);
            }
=======
                new Vector3(XCoordsOfFood[i] * _locationGenerator.LongOfGrassBlock,
                    -(YCoordsOfFood[i] * _locationGenerator.HeightOfGrassBlock), 0), Quaternion.identity);

            if (_locationGenerator.Rand.Next(0,100) %2 == 0)
            {
                Instantiate(FoodBlock,
                    new Vector3(XCoordsOfFood[i] * _locationGenerator.LongOfGrassBlock - _sizeOfFoodBlock,
                        -(YCoordsOfFood[i] * _locationGenerator.HeightOfGrassBlock) + _sizeOfFoodBlock, 0), Quaternion.identity);
            }          
>>>>>>> code-refactorings

            if (_locationGenerator.Rand.Next(0, 100) % 2 == 0)
            {
                Instantiate(FoodBlock,
                    new Vector3(
                        _placeGeneration.XCoordsOfFood[i] * _locationGenerator.LongOfGrassBlock + SizeOfFoodBlock,
                        -(_placeGeneration.YCoordsOfFood[i] * _locationGenerator.HeightOfGrassBlock) - SizeOfFoodBlock,
                        0), Quaternion.identity);
            }
        }
    }
}