using UnityEngine;

public class RadioGenerator : MonoBehaviour
{
    private PlaceGeneration _placeGeneration;
    private LocationGenerator _locationGenerator;

    public int[] XCoordsOfPartsOfRadio;
    public int[] YCoordsOfPartsOfRadio;

    public GameObject RadioPart;

    // Use this for initialization
    void Start()
    {
        _placeGeneration = GameObject.Find("GenerationManager").GetComponent<PlaceGeneration>();
        _locationGenerator = GameObject.Find("GenerationManager").GetComponent<LocationGenerator>();

        XCoordsOfPartsOfRadio = new int[_placeGeneration.XCoordsOfPartsOfRadio.Length];
        YCoordsOfPartsOfRadio = new int[_placeGeneration.YCoordsOfPartsOfRadio.Length];

        for (int i = 0; i < XCoordsOfPartsOfRadio.Length; i++)
        {
            XCoordsOfPartsOfRadio[i] = _placeGeneration.XCoordsOfPartsOfRadio[i];
            YCoordsOfPartsOfRadio[i] = _placeGeneration.YCoordsOfPartsOfRadio[i];
        }

        Creation();
    }

    private void Creation()
    {
        for (int i = 0; i < XCoordsOfPartsOfRadio.Length; i++)
        {
            Instantiate(RadioPart,
                new Vector3(XCoordsOfPartsOfRadio[i] * _locationGenerator.LongOfGrassBlock,
                    -(YCoordsOfPartsOfRadio[i] * _locationGenerator.HeightOfGrassBlock), 0f),
                Quaternion.identity);
        }
    }
}
