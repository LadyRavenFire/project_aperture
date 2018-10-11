using UnityEngine;

public class FoodGeneration : MonoBehaviour
{
    private PlaceGeneration _placeGeneration;
    private LocationGenerator _locationGenerator;

    public int[] XCoordsOfFood; //TODO убрать тут int[] и в waterGeneration, использовать данные напрямую из placegeneration
    public int[] YCoordsOfFood;

    public GameObject FoodBlock;

    private float _sizeOfFoodBlock;

    void Start()
    {
        _sizeOfFoodBlock = 1.3f;
        _placeGeneration = GameObject.Find("GenerationManager").GetComponent<PlaceGeneration>();
        _locationGenerator = GameObject.Find("GenerationManager").GetComponent<LocationGenerator>();
        XCoordsOfFood = new int[_placeGeneration.XCoordsOfFood.Length];
        YCoordsOfFood = new int[_placeGeneration.YCoordsOfFood.Length];

        for (int i = 0; i < _placeGeneration.XCoordsOfFood.Length; i++)
        {
            XCoordsOfFood[i] = _placeGeneration.XCoordsOfFood[i];
            YCoordsOfFood[i] = _placeGeneration.YCoordsOfFood[i];
        }

        Creation();
    }

    void Creation()
    {
        for (int i = 0; i < XCoordsOfFood.Length; i++)
        {
            Instantiate(FoodBlock,
                new Vector3(XCoordsOfFood[i] * _locationGenerator.LongOfGrassBlock,
                    -(YCoordsOfFood[i] * _locationGenerator.HeightOfGrassBlock), 0), Quaternion.identity);

            if (_locationGenerator.Rand.Next(0,100) %2 == 0)
            {
                Instantiate(FoodBlock,
                    new Vector3(XCoordsOfFood[i] * _locationGenerator.LongOfGrassBlock - _sizeOfFoodBlock,
                        -(YCoordsOfFood[i] * _locationGenerator.HeightOfGrassBlock) + _sizeOfFoodBlock, 0), Quaternion.identity);
            }          

            if (_locationGenerator.Rand.Next(0, 100) % 2 == 0)
            {
                Instantiate(FoodBlock,
                    new Vector3(XCoordsOfFood[i] * _locationGenerator.LongOfGrassBlock + _sizeOfFoodBlock,
                        -(YCoordsOfFood[i] * _locationGenerator.HeightOfGrassBlock) - _sizeOfFoodBlock, 0), Quaternion.identity);
            }


        }
    }

    
}

// 1. В Food и Water генераторах оставить только методы, которые будут отвечать только за саму инициализацию объектов. 
// 2. За вычисление блоков, в которых нужно будет иницализировать еду, воду, куски рации должен отвечать отдельный скрипт,
//    который будет просчитывать места генерации окружения с возможностью тонкой настройки.
//TODO написать сначла метод для вычисления блоков, потом переписать water generator, затем уже писать foodGenerator;