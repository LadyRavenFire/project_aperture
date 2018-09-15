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

    public GameObject Water;

    public int Size;

    private float _long;
    private float _height;

    private Random rand;

    // Use this for initialization
    void Start ()
	{
	    _long = 19.2f;
	    _height = 10.8f;
	    rand = new System.Random();

        LocationCreateEmpty();

        EnviromentCreate();
	    
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
                        Instantiate(RightUpCorner, new Vector3(j * _long, 0, 0), Quaternion.identity);
                    }
                    else
                    {
                        if (i == Size - 1 && j == 0)
                        {
                            Instantiate(LeftDownCorner, new Vector3(0, -(i * _height), 0), Quaternion.identity);
                        }
                        else
                        {
                            if (i == Size - 1 && j == Size - 1)
                            {
                                Instantiate(RightDownCorner, new Vector3(j * _long, -(i * _height), 0), Quaternion.identity);
                            }
                            else
                            {
                                if (j == 0 && i != 0 && i != Size - 1)
                                {
                                    Instantiate(BorderLeft, new Vector3(0, -(i * _height), 0), Quaternion.identity);
                                }
                                else
                                {
                                    if (j != 0 && j != Size - 1 && i == 0)
                                    {
                                        Instantiate(BorderUp, new Vector3(j * _long, 0, 0), Quaternion.identity);
                                    }
                                    else
                                    {
                                        if (j == Size - 1 && i != 0 && i != Size - 1)
                                        {
                                            Instantiate(BorderRight, new Vector3(j * _long, -(i * _height), 0), Quaternion.identity);
                                        }
                                        else
                                        {
                                            if (i == Size - 1 && j != 0 && j != Size - 1)
                                            {
                                                Instantiate(BorderDown, new Vector3(j * _long, -(i * _height), 0), Quaternion.identity);
                                            }
                                            else
                                            {
                                                Instantiate(CenterBlock, new Vector3(j * _long, -(i * _height), 0), Quaternion.identity);
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

    void EnviromentCreate()
    {
        

        if (Size == 2)
        {
            WaterCreation(1);
        }
        else
        {
            if (Size == 3)
            {
                WaterCreation(3);
            }
            else
            {
                if (Size == 4) 
                {
                    WaterCreation(4);
                }
            }
        }
        

    }

    void WaterCreation(int number)
    {
        for (int i = 0; i < number; i++)
        {
            
            int randomx = rand.Next(0, Size);
            int randomy = rand.Next(0, Size);

            Instantiate(Water, new Vector3(randomx * _long, -(randomy * _height), 0), Quaternion.identity);
        }     
    }
}