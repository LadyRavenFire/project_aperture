using UnityEngine;

public class MobGeneration : MonoBehaviour
{
    private PlaceGeneration _placeGeneration;
    private LocationGenerator _locationGenerator;

    public int[] XCoordsOfRedMob;
    public int[] YCoordsOfRedMob;

    public int[] XCoordsOfGreenMob;
    public int[] YCoordsOfGreenMob;

    public GameObject GreenMob;
    public GameObject RedMob;


    void Start()
    {
        _placeGeneration = GameObject.Find("GenerationManager").GetComponent<PlaceGeneration>();
        _locationGenerator = GameObject.Find("GenerationManager").GetComponent<LocationGenerator>();

        XCoordsOfGreenMob = new int[_placeGeneration.XCoordsOfGreenMobs.Length];
        YCoordsOfGreenMob = new int[_placeGeneration.YCoordsOfGreenMobs.Length];

        XCoordsOfRedMob = new int[_placeGeneration.XCoordsOfRedMobs.Length];
        YCoordsOfRedMob = new int[_placeGeneration.YCoordsOfRedMobs.Length];

        for (int i = 0; i < _placeGeneration.XCoordsOfGreenMobs.Length; i++)
        {
            XCoordsOfGreenMob[i] = _placeGeneration.XCoordsOfGreenMobs[i];
            YCoordsOfGreenMob[i] = _placeGeneration.YCoordsOfGreenMobs[i];
        }

        for (int i = 0; i < _placeGeneration.XCoordsOfRedMobs.Length; i++)
        {
            XCoordsOfRedMob[i] = _placeGeneration.XCoordsOfRedMobs[i];
            YCoordsOfRedMob[i] = _placeGeneration.YCoordsOfRedMobs[i];
        }

        Creation();
    }

    void Creation()
    {
        for (int i = 0; i < _placeGeneration.XCoordsOfGreenMobs.Length; i++)
        {
            Instantiate(GreenMob,
                new Vector3(XCoordsOfGreenMob[i] * _locationGenerator.LongOfGrassBlock,
                    -(YCoordsOfGreenMob[i] * _locationGenerator.HeightOfGrassBlock), 0), Quaternion.identity);
        }

        for (int i = 0; i < _placeGeneration.XCoordsOfRedMobs.Length; i++)
        {
            Instantiate(RedMob,
                new Vector3(XCoordsOfRedMob[i] * _locationGenerator.LongOfGrassBlock,
                    -(YCoordsOfRedMob[i] * _locationGenerator.HeightOfGrassBlock), 0), Quaternion.identity);
        }
    }
}
