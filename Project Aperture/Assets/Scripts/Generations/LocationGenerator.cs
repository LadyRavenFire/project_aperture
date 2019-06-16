﻿using UnityEngine;
using Random = System.Random;

public class LocationGenerator : MonoBehaviour
{

    public GameObject LeftUpCorner;
    public GameObject RightUpCorner;
    public GameObject LeftDownCorner;
    public GameObject RightDownCorner;

    public GameObject BorderUp;
    public GameObject BorderDown;
    public GameObject BorderLeft;
    public GameObject BorderRight;    

    public GameObject CenterBlock;
    

    public int Size;

    public float LongOfGrassBlock;
    public float HeightOfGrassBlock;

    public Random Rand;
    
    void Start ()
	{
	    LongOfGrassBlock = 19.2f;
	    HeightOfGrassBlock = 10.8f;
	    Rand = new System.Random();

	    Size = PlayerPrefs.GetInt("SizeOfMap");

        LocationCreateEmpty();	    
	}

    void LocationCreateEmpty()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (i == 0 && j == 0)
                {
                    Instantiate(LeftUpCorner, new Vector3(0, 0, 0), Quaternion.identity);
                }

                else
                {
                    if (i == 0 && j == Size - 1)
                    {
                        Instantiate(RightUpCorner, new Vector3(j * LongOfGrassBlock, 0, 0), Quaternion.identity);
                    }
                    else
                    {
                        if (i == Size - 1 && j == 0)
                        {
                            Instantiate(LeftDownCorner, new Vector3(0, -(i * HeightOfGrassBlock), 0), Quaternion.identity);
                        }
                        else
                        {
                            if (i == Size - 1 && j == Size - 1)
                            {
                                Instantiate(RightDownCorner, new Vector3(j * LongOfGrassBlock, -(i * HeightOfGrassBlock), 0), Quaternion.identity);
                            }
                            else
                            {
                                if (j == 0 && i != 0 && i != Size - 1)
                                {
                                    Instantiate(BorderLeft, new Vector3(0, -(i * HeightOfGrassBlock), 0), Quaternion.identity);
                                }
                                else
                                {
                                    if (j != 0 && j != Size - 1 && i == 0)
                                    {
                                        Instantiate(BorderUp, new Vector3(j * LongOfGrassBlock, 0, 0), Quaternion.identity);
                                    }
                                    else
                                    {
                                        if (j == Size - 1 && i != 0 && i != Size - 1)
                                        {
                                            Instantiate(BorderRight, new Vector3(j * LongOfGrassBlock, -(i * HeightOfGrassBlock), 0), Quaternion.identity);
                                        }
                                        else
                                        {
                                            if (i == Size - 1 && j != 0 && j != Size - 1)
                                            {
                                                Instantiate(BorderDown, new Vector3(j * LongOfGrassBlock, -(i * HeightOfGrassBlock), 0), Quaternion.identity);
                                            }
                                            else
                                            {
                                                Instantiate(CenterBlock, new Vector3(j * LongOfGrassBlock, -(i * HeightOfGrassBlock), 0), Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }  
}
