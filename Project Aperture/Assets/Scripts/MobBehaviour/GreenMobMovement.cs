using System;
using System.Linq;
using Pathfinding;
using UnityEngine;

public class GreenMobMovement : MonoBehaviour
{
    private LocationGenerator _locationGenerator;
    private Vector3 _target;
    private Seeker _seeker;

    public GameObject GreenMobPrefab;

    private Vector3 _goingTarget;

    private GameObject[] _greenMobs;
    private GameObject[] _waterPlaces;
    private GameObject[] _foodPlaces;

    private WaterNeeds _waterNeeds;
    private FoodNeeds _foodNeeds;

    public float TimeToBreeding = 15f;

    private enum WayPoint
    {
        Food, //ищем еду
        Water, //ищем воду
        Going, //идем до точки
        Choose, //выбираем что делать
        Walking, //гуляем
        Breeding //размножение
    }

    private WayPoint _way;


    void Start()
    {
        _locationGenerator = GameObject.Find("GenerationManager").GetComponent<LocationGenerator>();
        _seeker = GetComponent<Seeker>();        

        _waterPlaces = GameObject.FindGameObjectsWithTag("Water");
        _foodPlaces = GameObject.FindGameObjectsWithTag("Food");
        _greenMobs = GameObject.FindGameObjectsWithTag("GreenMob");
        
        for (int i = 0; i < _greenMobs.Length; i++)
        {
            if (_greenMobs[i].name == gameObject.name)
            {
                _greenMobs[i] = null;
                _greenMobs = _greenMobs.Where(x => x != null).ToArray();
            }
        }

        _waterNeeds = GetComponent<WaterNeeds>();
        _foodNeeds = GetComponent<FoodNeeds>();

        _way = WayPoint.Choose;
    }

    void Update()
    {
        _greenMobs = GameObject.FindGameObjectsWithTag("GreenMob");

        for (int i = 0; i < _greenMobs.Length; i++)
        {
            if (_greenMobs[i].name == gameObject.name)
            {
                _greenMobs[i] = null;
                _greenMobs = _greenMobs.Where(x => x != null).ToArray();
            }
        }

        if (_greenMobs.Length >= _locationGenerator.Size * 1.5)
        {
            TimeToBreeding = 20f;
        }

        if (TimeToBreeding > 0f)
        {
            TimeToBreeding = TimeToBreeding - Time.deltaTime;
        }
        else
        {
            TimeToBreeding = 0;
        }

        Way();
    }

    void Way()
    {        

        if (_way == WayPoint.Going)
        {
            if (TimeToBreeding <= 0f)
            {
                for (int i = 0; i < _greenMobs.Length; i++)
                {
                    if (Math.Abs(_greenMobs[i].transform.position.x - gameObject.transform.position.x) < 4f && Math.Abs(_greenMobs[i].transform.position.y - gameObject.transform.position.y) <4f)
                    {
                        if (_foodNeeds.ReturnFood() > 50)
                        {
                            if (_waterNeeds.ReturnWater() > 50)
                            {
                                _way = WayPoint.Breeding;
                            }
                        }
                    }
                }
            }


            if (gameObject.transform.position.x > _target.x - 0.5f &&
                gameObject.transform.position.x <= _target.x + 0.5f &&
                gameObject.transform.position.y > _target.y - 0.5f &&
                gameObject.transform.position.y <= _target.y + 2.7f)
            {
                //print("Done!");
                _way = WayPoint.Choose;
            }
        }

        if (_way == WayPoint.Breeding)
        {
            TimeToBreeding = 15f;
            Instantiate(GreenMobPrefab,
                new Vector3(gameObject.transform.position.x + 0.3f, gameObject.transform.position.y + 0.3f,
                    gameObject.transform.position.z), Quaternion.identity);
            _way = WayPoint.Going;
        }

        if (_way == WayPoint.Food)
        {
            FoodHave need;
            int number = 0;

            float distance;

            distance = (Math.Abs(_foodPlaces[number].transform.position.x - gameObject.transform.position.x -
                                 _locationGenerator.LongOfGrassBlock / 2) +
                        Math.Abs(_foodPlaces[number].transform.position.y - gameObject.transform.position.y +
                                 _locationGenerator.LongOfGrassBlock / 2));

            for (int i = 0; i < _foodPlaces.Length; i++)
            {
                need = _foodPlaces[i].GetComponent<FoodHave>();
                if (need.ReturnFood() > _foodNeeds.ReturnFoodNeeds())
                {
                    if ((Math.Abs(_foodPlaces[i].transform.position.x - gameObject.transform.position.x -
                                  _locationGenerator.LongOfGrassBlock / 2) +
                         Math.Abs(_foodPlaces[i].transform.position.y - gameObject.transform.position.y +
                                  _locationGenerator.HeightOfGrassBlock / 2)) < distance)
                    {
                        number = i;

                        distance = (Math.Abs(_foodPlaces[number].transform.position.x - gameObject.transform.position.x -
                                             _locationGenerator.LongOfGrassBlock / 2) +
                                    Math.Abs(_foodPlaces[number].transform.position.y - gameObject.transform.position.y +
                                             _locationGenerator.LongOfGrassBlock / 2));
                    }
                }
            }

            _target = _foodPlaces[number].transform.position;
            _seeker.StartPath(transform.position, _target + new Vector3(0f, 1.7f, 0), OnPathComplete);
            _way = WayPoint.Going;
        }

        if (_way == WayPoint.Water)
        {
            //print("water");
            WaterHave need;
            int number = 0;

            //WaterNeeds waterNeeds = GetComponent<WaterNeeds>();

            float distance;

            distance = (Math.Abs(_waterPlaces[number].transform.position.x - gameObject.transform.position.x -
                                 _locationGenerator.LongOfGrassBlock / 2) +
                        Math.Abs(_waterPlaces[number].transform.position.y - gameObject.transform.position.y +
                                 _locationGenerator.LongOfGrassBlock / 2));

            for (int i = 0; i < _waterPlaces.Length; i++)
            {
                need = _waterPlaces[i].GetComponent<WaterHave>();
                if (need.ReturnWater() > _waterNeeds.ReturnWaterNeed())
                {
                    if ((Math.Abs(_foodPlaces[i].transform.position.x - gameObject.transform.position.x -
                                  _locationGenerator.LongOfGrassBlock / 2) +
                         Math.Abs(_foodPlaces[i].transform.position.y - gameObject.transform.position.y +
                                  _locationGenerator.HeightOfGrassBlock / 2)) < distance)
                    {
                        number = i;
                        distance = (Math.Abs(_waterPlaces[number].transform.position.x - gameObject.transform.position.x -
                                             _locationGenerator.LongOfGrassBlock / 2) +
                                    Math.Abs(_waterPlaces[number].transform.position.y - gameObject.transform.position.y +
                                             _locationGenerator.LongOfGrassBlock / 2));
                    }
                }
            }

            _target = _waterPlaces[number].transform.position;
            _seeker.StartPath(transform.position, _target + new Vector3(0f, 2.4f, 0), OnPathComplete);

            _way = WayPoint.Going;
        }

        if (_way == WayPoint.Walking)
        {
            var seeker = GetComponent<Seeker>();

            _goingTarget = new Vector3(
                _locationGenerator.Size *
                _locationGenerator.Rand.Next(0, (int) _locationGenerator.LongOfGrassBlock * 9 / 10),
                -_locationGenerator.Size *
                _locationGenerator.Rand.Next(0, (int) _locationGenerator.HeightOfGrassBlock / 2), 0f);
            _target = _goingTarget;
            // _target = GameObject.Find("Player").transform.position;

            seeker.StartPath(transform.position, _target, OnPathComplete);
            _way = WayPoint.Going;
        }

        if (_way == WayPoint.Choose)
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
                    _way = WayPoint.Walking;
                    ;
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
                    _way = WayPoint.Walking;
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
