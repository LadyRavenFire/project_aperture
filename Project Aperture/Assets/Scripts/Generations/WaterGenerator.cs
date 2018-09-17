using UnityEngine;



public class WaterGenerator : MonoBehaviour
{
    private LocationGenerator _locationGenerator;

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

    private float _sizeOfWaterBlock;

    public int[] XcoordsOfWater;
    public int[] YcoordsOfWater;

    void Start()
    {
        _locationGenerator = GameObject.Find("GenerationManager").GetComponent<LocationGenerator>();
        _sizeOfWaterBlock = 1.3f;

        XcoordsOfWater = new int[_locationGenerator.Size];
        YcoordsOfWater = new int[_locationGenerator.Size];

        CreateRandomPlace(_locationGenerator.Size);
        CreateRandomGeneration();
    }

    void CreateRandomPlace(int sizeOLocation)
    {
        int randomX, randomY;

        for (int i = 0; i < sizeOLocation; i++)
        {
            randomX = RandomCreateNumberForPlace();
            randomY = RandomCreateNumberForPlace();

            bool check = true;

            while (check)
            {
                if (CheckForRepeat(randomX, randomY))
                {
                    randomX = RandomCreateNumberForPlace();
                    randomY = RandomCreateNumberForPlace();
                }
                else
                {
                    check = false;
                } 
            }

            if (check == false)
            {
                XcoordsOfWater[i] = randomX;
                YcoordsOfWater[i] = randomY;
            }

        }
            
    }

    int RandomCreateNumberForPlace()
    {
        int answer = _locationGenerator.Rand.Next(0, _locationGenerator.Size);
        return answer;
    }

    bool CheckForRepeat(int Xcoord, int Ycoord)
    {
        for (int i = 0; i < _locationGenerator.Size; i++)
        {
            if (XcoordsOfWater[i] == Xcoord)
            {
                if (YcoordsOfWater[i] == Ycoord)
                {
                    return true;
                }
            }
        }
        return false;
    }


    void CreateRandomGeneration()
    {
        for (int i = 0; i < _locationGenerator.Size; i++)
        {
            Instantiate((_locationGenerator.Rand.Next(0, 100) % 2) == 0 ? CenterBlock : CenterBlock2,
                new Vector3(XcoordsOfWater[i] * _locationGenerator.LongOfGrassBlock,
                    -(YcoordsOfWater[i] * _locationGenerator.HeightOfGrassBlock), 0), Quaternion.identity);

            Instantiate((_locationGenerator.Rand.Next(0, 100) % 2) == 0 ? BorderUp : BorderUp2,
                new Vector3(XcoordsOfWater[i] * _locationGenerator.LongOfGrassBlock,
                    -(YcoordsOfWater[i] * _locationGenerator.HeightOfGrassBlock) + _sizeOfWaterBlock, 0),
                Quaternion.identity);

            Instantiate((_locationGenerator.Rand.Next(0, 100) % 2) == 0 ? BorderDown : BorderDown2,
                new Vector3(XcoordsOfWater[i] * _locationGenerator.LongOfGrassBlock,
                    -(YcoordsOfWater[i] * _locationGenerator.HeightOfGrassBlock) - _sizeOfWaterBlock, 0),
                Quaternion.identity);

            Instantiate((_locationGenerator.Rand.Next(0, 100) % 2) == 0 ? BorderLeft : BorderLeft2,
                new Vector3((XcoordsOfWater[i] * _locationGenerator.LongOfGrassBlock) - _sizeOfWaterBlock,
                    -(YcoordsOfWater[i] * _locationGenerator.HeightOfGrassBlock), 0), Quaternion.identity);

            Instantiate((_locationGenerator.Rand.Next(0, 100) % 2) == 0 ? BorderRight : BorderRight2,
                new Vector3((XcoordsOfWater[i] * _locationGenerator.LongOfGrassBlock) + _sizeOfWaterBlock,
                    -(YcoordsOfWater[i] * _locationGenerator.HeightOfGrassBlock), 0), Quaternion.identity);

            Instantiate((_locationGenerator.Rand.Next(0, 100) % 2) == 0 ? LeftUpCorner : LeftUpCorner2,
                new Vector3((XcoordsOfWater[i] * _locationGenerator.LongOfGrassBlock) - _sizeOfWaterBlock,
                    -(YcoordsOfWater[i] * _locationGenerator.HeightOfGrassBlock) + _sizeOfWaterBlock, 0),
                Quaternion.identity);

            Instantiate((_locationGenerator.Rand.Next(0, 100) % 2) == 0 ? RightUpCorner : RightUpCorner2,
                new Vector3((XcoordsOfWater[i] * _locationGenerator.LongOfGrassBlock) + _sizeOfWaterBlock,
                    -(YcoordsOfWater[i] * _locationGenerator.HeightOfGrassBlock) + _sizeOfWaterBlock, 0),
                Quaternion.identity);

            Instantiate((_locationGenerator.Rand.Next(0, 100) % 2) == 0 ? LeftDownCorner : LeftDownCorner2,
                new Vector3((XcoordsOfWater[i] * _locationGenerator.LongOfGrassBlock) - _sizeOfWaterBlock,
                    -(YcoordsOfWater[i] * _locationGenerator.HeightOfGrassBlock) - _sizeOfWaterBlock, 0),
                Quaternion.identity);

            Instantiate((_locationGenerator.Rand.Next(0, 100) % 2) == 0 ? RightDownCorner : RightDownCorner2,
                new Vector3((XcoordsOfWater[i] * _locationGenerator.LongOfGrassBlock) + _sizeOfWaterBlock,
                    -(YcoordsOfWater[i] * _locationGenerator.HeightOfGrassBlock) - _sizeOfWaterBlock, 0),
                Quaternion.identity);
        }
    }


}
