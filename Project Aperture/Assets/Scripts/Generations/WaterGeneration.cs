using UnityEngine;

public class WaterGeneration : MonoBehaviour
{
    private LocationGenerator _locationGenerator;
    private PlaceGeneration _placeGeneration;

    public GameObject WaterTrigger;

    public GameObject LeftUpCorner;
    public GameObject RightUpCorner;
    public GameObject LeftDownCorner;
    public GameObject RightDownCorner;

    public GameObject LeftUpCorner2;
    public GameObject RightUpCorner2;
    public GameObject LeftDownCorner2;
    public GameObject RightDownCorner2;

    public GameObject BorderUp;
    public GameObject BorderDown;
    public GameObject BorderLeft;
    public GameObject BorderRight;

    public GameObject BorderUp2;
    public GameObject BorderDown2;
    public GameObject BorderLeft2;
    public GameObject BorderRight2;

    public GameObject CenterBlock;
    public GameObject CenterBlock2;

    public float SizeOfWaterBlock = 1.3f;

    void Start()
    {
        _locationGenerator = GameObject.Find("GenerationManager").GetComponent<LocationGenerator>();
        _placeGeneration = GameObject.Find("GenerationManager").GetComponent<PlaceGeneration>();

        CreateRandomGeneration();
    }

    void CreateRandomGeneration()
    {
        for (int i = 0; i < _placeGeneration.XCoordsOfWater.Length; i++)
        {
            Instantiate(WaterTrigger, new Vector3(
                _placeGeneration.XCoordsOfWater[i] * _locationGenerator.LongOfGrassBlock,
                -(_placeGeneration.YCoordsOfWater[i] * _locationGenerator.HeightOfGrassBlock), 0), Quaternion.identity);

            Instantiate((_locationGenerator.Rand.Next(0, 100) % 2) == 0 ? CenterBlock : CenterBlock2,
                new Vector3(_placeGeneration.XCoordsOfWater[i] * _locationGenerator.LongOfGrassBlock,
                    -(_placeGeneration.YCoordsOfWater[i] * _locationGenerator.HeightOfGrassBlock), 0),
                Quaternion.identity);

            Instantiate((_locationGenerator.Rand.Next(0, 100) % 2) == 0 ? BorderUp : BorderUp2,
                new Vector3(_placeGeneration.XCoordsOfWater[i] * _locationGenerator.LongOfGrassBlock,
                    -(_placeGeneration.YCoordsOfWater[i] * _locationGenerator.HeightOfGrassBlock) + SizeOfWaterBlock,
                    0),
                Quaternion.identity);

            Instantiate((_locationGenerator.Rand.Next(0, 100) % 2) == 0 ? BorderDown : BorderDown2,
                new Vector3(_placeGeneration.XCoordsOfWater[i] * _locationGenerator.LongOfGrassBlock,
                    -(_placeGeneration.YCoordsOfWater[i] * _locationGenerator.HeightOfGrassBlock) - SizeOfWaterBlock,
                    0),
                Quaternion.identity);

            Instantiate((_locationGenerator.Rand.Next(0, 100) % 2) == 0 ? BorderLeft : BorderLeft2,
                new Vector3(
                    (_placeGeneration.XCoordsOfWater[i] * _locationGenerator.LongOfGrassBlock) - SizeOfWaterBlock,
                    -(_placeGeneration.YCoordsOfWater[i] * _locationGenerator.HeightOfGrassBlock), 0),
                Quaternion.identity);

            Instantiate((_locationGenerator.Rand.Next(0, 100) % 2) == 0 ? BorderRight : BorderRight2,
                new Vector3(
                    (_placeGeneration.XCoordsOfWater[i] * _locationGenerator.LongOfGrassBlock) + SizeOfWaterBlock,
                    -(_placeGeneration.YCoordsOfWater[i] * _locationGenerator.HeightOfGrassBlock), 0),
                Quaternion.identity);

            Instantiate((_locationGenerator.Rand.Next(0, 100) % 2) == 0 ? LeftUpCorner : LeftUpCorner2,
                new Vector3(
                    (_placeGeneration.XCoordsOfWater[i] * _locationGenerator.LongOfGrassBlock) - SizeOfWaterBlock,
                    -(_placeGeneration.YCoordsOfWater[i] * _locationGenerator.HeightOfGrassBlock) + SizeOfWaterBlock,
                    0),
                Quaternion.identity);

            Instantiate((_locationGenerator.Rand.Next(0, 100) % 2) == 0 ? RightUpCorner : RightUpCorner2,
                new Vector3(
                    (_placeGeneration.XCoordsOfWater[i] * _locationGenerator.LongOfGrassBlock) + SizeOfWaterBlock,
                    -(_placeGeneration.YCoordsOfWater[i] * _locationGenerator.HeightOfGrassBlock) + SizeOfWaterBlock,
                    0),
                Quaternion.identity);

            Instantiate((_locationGenerator.Rand.Next(0, 100) % 2) == 0 ? LeftDownCorner : LeftDownCorner2,
                new Vector3(
                    (_placeGeneration.XCoordsOfWater[i] * _locationGenerator.LongOfGrassBlock) - SizeOfWaterBlock,
                    -(_placeGeneration.YCoordsOfWater[i] * _locationGenerator.HeightOfGrassBlock) - SizeOfWaterBlock,
                    0),
                Quaternion.identity);

            Instantiate((_locationGenerator.Rand.Next(0, 100) % 2) == 0 ? RightDownCorner : RightDownCorner2,
                new Vector3(
                    (_placeGeneration.XCoordsOfWater[i] * _locationGenerator.LongOfGrassBlock) + SizeOfWaterBlock,
                    -(_placeGeneration.YCoordsOfWater[i] * _locationGenerator.HeightOfGrassBlock) - SizeOfWaterBlock,
                    0),
                Quaternion.identity);
        }
    }
}
