using System;
using Pathfinding;
using UnityEngine;

public class GreenMobMovement : MonoBehaviour
{
    private LocationGenerator _locationGenerator;
    private Vector3 _target;
    private Seeker _seeker;

    private Vector3 _goingTarget;


    private GameObject[] Waters;
    private GameObject[] Foods;

    private WaterNeeds _waterNeeds;
    private FoodNeeds _foodNeeds;

    private enum WayPoint
    {
       Food,
       Water,
       Going,
       Going2,
       Patrol
    }

    private WayPoint _way;


    void Start()
    {
        _locationGenerator = GameObject.Find("GenerationManager").GetComponent<LocationGenerator>();
        _seeker = GetComponent<Seeker>();
        

        Waters = GameObject.FindGameObjectsWithTag("Water");
        //NumberOfWater = 0;

        Foods = GameObject.FindGameObjectsWithTag("Food");
        //NumberOfFoods = 0;

        _waterNeeds = GetComponent<WaterNeeds>();
        _foodNeeds = GetComponent<FoodNeeds>();

        _way = WayPoint.Food;
    }

    void Update()
    {
        Way();
    }

    void Way()
    {
        if (_way == WayPoint.Food)
        {
            FoodHave need ; 
            float kolvo = 0;
            int number = 0;

            float distance;

            for (int i = 0; i < Foods.Length; i++)
            {
                need = Foods[i].GetComponent<FoodHave>();

                distance = (Math.Abs(Foods[number].transform.position.x - gameObject.transform.position.x -
                                     _locationGenerator.LongOfGrassBlock / 2) +
                            Math.Abs(Foods[number].transform.position.y - gameObject.transform.position.y +
                                     _locationGenerator.LongOfGrassBlock / 2));

                if (need.ReturnFood() > _foodNeeds.ReturnFoodNeeds())
                {
                    if ((Math.Abs(Foods[i].transform.position.x - gameObject.transform.position.x - _locationGenerator.LongOfGrassBlock / 2) +
                         Math.Abs(Foods[i].transform.position.y - gameObject.transform.position.y + _locationGenerator.HeightOfGrassBlock / 2)) < distance)
                    {
                        number = i;
                        kolvo = need.ReturnFood();
                    }
                }


                /*if (
                    kolvo < need.ReturnFood()
                    && i != NumberOfFoods
                    && ((Math.Abs(Foods[i].transform.position.x - gameObject.transform.position.x - _locationGenerator.LongOfGrassBlock/2) +
                         Math.Abs(Foods[i].transform.position.y - gameObject.transform.position.y + _locationGenerator.HeightOfGrassBlock/2))
                        < (Math.Abs(Foods[number].transform.position.x - gameObject.transform.position.x - _locationGenerator.LongOfGrassBlock/2) +
                           Math.Abs(Foods[number].transform.position.y - gameObject.transform.position.y + _locationGenerator.LongOfGrassBlock/2)))
                )
                {
                    number = i;
                    kolvo = need.ReturnFood();
                    //print(number);
                    //print(kolvo);
                }*/
                                             
            }
            
            _target = Foods[number].transform.position;
            //NumberOfFoods = number;
            _seeker.StartPath(transform.position, _target + new Vector3(0f, 1.7f, 0), OnPathComplete);
            _way = WayPoint.Going;
        }

        if (_way == WayPoint.Going)
        {
            if (gameObject.transform.position.x > _target.x - 0.5f &&
                gameObject.transform.position.x <= _target.x + 0.5f &&
                gameObject.transform.position.y > _target.y - 0.5f &&
                gameObject.transform.position.y <= _target.y  + 2.7f) 
            {
                //print("Done!");
                _way = WayPoint.Going2;
            }
        }

        if (_way == WayPoint.Water)
        {
            //print("water");
            WaterHave need;
            float kolvo = 0;
            int number = 0;

            //WaterNeeds waterNeeds = GetComponent<WaterNeeds>();

            float distance;

            for (int i = 0; i < Waters.Length; i++)
            {
                need = Waters[i].GetComponent<WaterHave>();

                distance = (Math.Abs(Waters[number].transform.position.x - gameObject.transform.position.x -
                                     _locationGenerator.LongOfGrassBlock / 2) +
                            Math.Abs(Waters[number].transform.position.y - gameObject.transform.position.y +
                                     _locationGenerator.LongOfGrassBlock / 2));

                if (need.ReturnWater() > _waterNeeds.ReturnWaterNeed())
                {
                    if ((Math.Abs(Foods[i].transform.position.x - gameObject.transform.position.x - _locationGenerator.LongOfGrassBlock / 2) +
                         Math.Abs(Foods[i].transform.position.y - gameObject.transform.position.y + _locationGenerator.HeightOfGrassBlock / 2)) < distance)
                    {
                        number = i;
                        kolvo = need.ReturnWater();
                    }
                }

            }

            _target = Waters[number].transform.position;
            _seeker.StartPath(transform.position, _target + new Vector3(0f, 2.4f, 0), OnPathComplete);

            _way = WayPoint.Going;


            /*bool flag = false;
            for (int i = 0; i < Waters.Length; i++)
            {
                need = Waters[i].GetComponent<WaterHave>(); //TODO сделать сохранение количества пищи и пути и сравнивать их в приоритете расстояния
                //print(need.ReturnWater());
                if ( ((Math.Abs(Waters[i].transform.position.x - gameObject.transform.position.x) +
                         Math.Abs(Waters[i].transform.position.y - gameObject.transform.position.y))
                        < (Math.Abs(Waters[number].transform.position.x - gameObject.transform.position.x) +
                           Math.Abs(Waters[number].transform.position.y - gameObject.transform.position.y)))
                )
                    {
                        if (kolvo < (need.ReturnWater())
                            && i != NumberOfWater
                            && waterNeeds.ReturnWaterNeed() < need.ReturnWater()
                        )
                            {
                                //print(i);
                                number = i;
                                kolvo = need.ReturnWater();
                                flag = true;
                            }
                        

                        //print(kolvo);
                    }

                if (i == 0)
                {
                    //print(waterNeeds.ReturnWaterNeed());
                    if (need.ReturnWater() > waterNeeds.ReturnWaterNeed())
                    {
                        flag = true;
                    } 
                }
                
            }

            if (flag)
            {
                //print(number);
                _target = Waters[number].transform.position;
                NumberOfWater = number;
                _seeker.StartPath(transform.position, _target + new Vector3(0f, 2.4f, 0), OnPathComplete);

                _way = WayPoint.Going;
            }
            else
            {
                print("Stuck!");
                _target = Waters[_locationGenerator.Rand.Next(0, Waters.Length)].transform.position;
                _seeker.StartPath(transform.position, _target + new Vector3(0f, 2.4f, 0), OnPathComplete);
                _way = WayPoint.Going;
            }*/

        }

        if (_way == WayPoint.Patrol)
        {
            var seeker = GetComponent<Seeker>();

            _goingTarget = new Vector3(_locationGenerator.Size * _locationGenerator.Rand.Next(0, (int)_locationGenerator.LongOfGrassBlock * 9 / 10),
                                   - _locationGenerator.Size * _locationGenerator.Rand.Next(0, (int)_locationGenerator.HeightOfGrassBlock/2), 0f);
            _target = _goingTarget;
           // _target = GameObject.Find("Player").transform.position;
              
            seeker.StartPath(transform.position, _target, OnPathComplete);
            _way = WayPoint.Going;
        }

        if (_way == WayPoint.Going2)
        {
            WaterNeeds water = GetComponent<WaterNeeds>();
            FoodNeeds food = GetComponent<FoodNeeds>();

            if (water.ReturnWater() < food.ReturnFood())
            {
                if (water.ReturnWater() < 85)
                {
                    _way = WayPoint.Water;
                }
                else
                {
                    _way = WayPoint.Patrol;;
                }
            }
            else
            {
                if (food.ReturnFood() < 85)
                {
                    _way = WayPoint.Food;
                }
                else
                {
                    _way = WayPoint.Patrol;
                }
            }     
        }        
    }

    public void OnPathComplete(Path p)
    {
        // We got our path back
        if (p.error)
        {
            // Nooo, a valid path couldn't be found
        }
        else
        {
            // Yay, now we can get a Vector3 representation of the path
            // from p.vectorPath
        }
    }
}
