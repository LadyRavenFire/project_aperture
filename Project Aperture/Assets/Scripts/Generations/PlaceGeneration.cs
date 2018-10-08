using UnityEngine;

public class PlaceGeneration : MonoBehaviour
{
    private LocationGenerator _locationGenerator;

    public int[] XCoordsOfWater;
    public int[] YCoordsOfWater;

    public int[] XCoordsOfFood;
    public int[] YCoordsOfFood;

    public int[] XCoordsOfPartsOfRadio;
    public int[] YCoordsOfPartsOfRadio;

    void Start()
    {
        _locationGenerator = GameObject.Find("GenerationManager").GetComponent<LocationGenerator>();
        CreateNumberOfCoords();
        CreateRandomPlaces(XCoordsOfWater, YCoordsOfWater);
        CreateRandomPlaces(XCoordsOfFood, YCoordsOfFood);
        CreateRandomPlaces(XCoordsOfPartsOfRadio, YCoordsOfPartsOfRadio);
    }

    void CreateNumberOfCoords() //rename and change code and balance like (coords* .... <= 2/3) or like that
    {
        if (_locationGenerator.Size <= 4)
        {
            XCoordsOfWater = new int[_locationGenerator.Size - 1];
            YCoordsOfWater = new int[_locationGenerator.Size - 1];

            XCoordsOfFood = new int[_locationGenerator.Size - 1];
            YCoordsOfFood = new int[_locationGenerator.Size - 1];

            XCoordsOfPartsOfRadio = new int[_locationGenerator.Size -1];
            YCoordsOfPartsOfRadio = new int[_locationGenerator.Size -1];
        }

        if (_locationGenerator.Size >= 5 && _locationGenerator.Size < 10)
        {
            XCoordsOfWater = new int[_locationGenerator.Size];
            YCoordsOfWater = new int[_locationGenerator.Size];

            XCoordsOfFood = new int[_locationGenerator.Size];
            YCoordsOfFood = new int[_locationGenerator.Size];

            XCoordsOfPartsOfRadio = new int[_locationGenerator.Size];
            YCoordsOfPartsOfRadio = new int[_locationGenerator.Size];
        }

        if (_locationGenerator.Size >= 10 && _locationGenerator.Size < 15)
        {
            XCoordsOfWater = new int[_locationGenerator.Size *2];
            YCoordsOfWater = new int[_locationGenerator.Size *2];

            XCoordsOfFood = new int[_locationGenerator.Size *2];
            YCoordsOfFood = new int[_locationGenerator.Size *2];

            XCoordsOfPartsOfRadio = new int[_locationGenerator.Size * 2];
            YCoordsOfPartsOfRadio = new int[_locationGenerator.Size * 2];
        }

        if (_locationGenerator.Size >= 15)
        {
            XCoordsOfWater = new int[_locationGenerator.Size * 3];
            YCoordsOfWater = new int[_locationGenerator.Size * 3];

            XCoordsOfFood = new int[_locationGenerator.Size * 3];
            YCoordsOfFood = new int[_locationGenerator.Size * 3];

            XCoordsOfPartsOfRadio = new int[_locationGenerator.Size * 3
];
            YCoordsOfPartsOfRadio = new int[_locationGenerator.Size * 3];
        }

    }

    void CreateRandomPlaces(int[] XCoords, int[] YCoords)
    {
        for (int i = 0; i < XCoords.Length; i++)
        {
            var randomX = RandomCreateNumberForPlace();
            var randomY = RandomCreateNumberForPlace();

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
                XCoords[i] = randomX;
                YCoords[i] = randomY;
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
        for (int i = 0; i < XCoordsOfWater.Length; i++)
        {
            if (XCoordsOfWater[i] == Xcoord)
            {
                if (YCoordsOfWater[i] == Ycoord)
                {
                    return true;
                }
            }
        }

        for (int i = 0; i < XCoordsOfFood.Length; i++)
        {
            if (XCoordsOfFood[i] == Xcoord)
            {
                if (YCoordsOfFood[i] == Ycoord)
                {
                    return true;
                }
            }
        }

        for (int i = 0; i < XCoordsOfPartsOfRadio.Length; i++)
        {
            if (XCoordsOfPartsOfRadio[i] == Xcoord)
            {
                if (YCoordsOfPartsOfRadio[i] == Ycoord)
                {
                    return true;
                }
            }
        }
        return false;
    }

}
