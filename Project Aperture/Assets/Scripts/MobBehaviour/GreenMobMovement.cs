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
    private GameObject[] _waters;
    private GameObject[] _foods;

    private WaterNeeds _waterNeeds;
    private FoodNeeds _foodNeeds;

    private TimeManager _timeManager;
    public string NameOfTimeManager = "LocationManager";

    private float _timeToBreeding;

    private Vector3 _changePlace;
    private float _timeToStuck;

    private enum WayPoint
    {
        Food,
        Water,
        Going,
        Choose,
        Walking,
        Breeding
    }

    private WayPoint _way;


    void Start()
    {
        _locationGenerator = GameObject.Find("GenerationManager").GetComponent<LocationGenerator>();
        _seeker = GetComponent<Seeker>();

        _timeManager = GameObject.Find(NameOfTimeManager).GetComponent<TimeManager>();

        _timeToBreeding = 15f;

        _waters = GameObject.FindGameObjectsWithTag("Water");
        _foods = GameObject.FindGameObjectsWithTag("Food");
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

        _timeToStuck = 0f;
        _changePlace = new Vector3(0f,0f,0f);

        _way = WayPoint.Choose;
    }

    void FixedUpdate()
    {
        ChangeFoodSpending();

        _greenMobs = GameObject.FindGameObjectsWithTag("GreenMob");

        for (int i = 0; i < _greenMobs.Length; i++)
        {
            if (_greenMobs[i].name == gameObject.name)
            {
                _greenMobs[i] = null;
                _greenMobs = _greenMobs.Where(x => x != null).ToArray();
            }
        }

        if (_changePlace == gameObject.transform.position)
        {
            _timeToStuck += Time.deltaTime;
        }
        else
        {
            _changePlace = gameObject.transform.position;
            _timeToStuck = 0f;
        }

        if (_timeToStuck >= 2f)
        {
            _way = WayPoint.Walking;
        }

        if (_greenMobs.Length >= _locationGenerator.Size * 1.5)
        {
            _timeToBreeding = 20f;
        }

        if (_timeToBreeding > 0f)
        {
            _timeToBreeding = _timeToBreeding - Time.deltaTime;
        }
        else
        {
            _timeToBreeding = 0;
        }

        Way();
    }

    void ChangeFoodSpending()
    {
        switch (_timeManager.NameOfTime)
        {
            case TimeManager.TimeName.Morning:
                _foodNeeds.FoodSendInSecond = 0.03f;
                break;
            case TimeManager.TimeName.Day:
                _foodNeeds.FoodSendInSecond = 0.026f;
                break;
            case TimeManager.TimeName.Evening:
                _foodNeeds.FoodSendInSecond = 0.016f;
                break;
            case TimeManager.TimeName.Night:
                _foodNeeds.FoodSendInSecond = 0.01f;
                break;
        }
    }

    void Way()
    {        

        if (_way == WayPoint.Going)
        {
            if (_timeToBreeding <= 0f)
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
                _way = WayPoint.Choose;
            }
        }

        if (_way == WayPoint.Breeding)
        {
            _timeToBreeding = 15f;
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

            distance = (Math.Abs(_foods[number].transform.position.x - gameObject.transform.position.x -
                                 _locationGenerator.LongOfGrassBlock / 2) +
                        Math.Abs(_foods[number].transform.position.y - gameObject.transform.position.y +
                                 _locationGenerator.LongOfGrassBlock / 2));

            for (int i = 0; i < _foods.Length; i++)
            {
                need = _foods[i].GetComponent<FoodHave>();
                if (need.ReturnFood() > _foodNeeds.ReturnFoodNeeds())
                {
                    if ((Math.Abs(_foods[i].transform.position.x - gameObject.transform.position.x -
                                  _locationGenerator.LongOfGrassBlock / 2) +
                         Math.Abs(_foods[i].transform.position.y - gameObject.transform.position.y +
                                  _locationGenerator.HeightOfGrassBlock / 2)) < distance)
                    {
                        number = i;

                        distance = (Math.Abs(_foods[number].transform.position.x - gameObject.transform.position.x -
                                             _locationGenerator.LongOfGrassBlock / 2) +
                                    Math.Abs(_foods[number].transform.position.y - gameObject.transform.position.y +
                                             _locationGenerator.LongOfGrassBlock / 2));
                    }
                }
            }

            _target = _foods[number].transform.position;
            _seeker.StartPath(transform.position, _target + new Vector3(0f, 1.7f, 0), OnPathComplete);
            _way = WayPoint.Going;
        }

        if (_way == WayPoint.Water)
        {
            WaterHave need;
            int number = 0;
            
            float distance;

            distance = (Math.Abs(_waters[number].transform.position.x - gameObject.transform.position.x -
                                 _locationGenerator.LongOfGrassBlock / 2) +
                        Math.Abs(_waters[number].transform.position.y - gameObject.transform.position.y +
                                 _locationGenerator.LongOfGrassBlock / 2));

            for (int i = 0; i < _waters.Length; i++)
            {
                need = _waters[i].GetComponent<WaterHave>();
                if (need.ReturnWater() > _waterNeeds.ReturnWaterNeed())
                {
                    if ((Math.Abs(_foods[i].transform.position.x - gameObject.transform.position.x -
                                  _locationGenerator.LongOfGrassBlock / 2) +
                         Math.Abs(_foods[i].transform.position.y - gameObject.transform.position.y +
                                  _locationGenerator.HeightOfGrassBlock / 2)) < distance)
                    {
                        number = i;
                        distance = (Math.Abs(_waters[number].transform.position.x - gameObject.transform.position.x -
                                             _locationGenerator.LongOfGrassBlock / 2) +
                                    Math.Abs(_waters[number].transform.position.y - gameObject.transform.position.y +
                                             _locationGenerator.LongOfGrassBlock / 2));
                    }
                }
            }

            _target = _waters[number].transform.position;
            _seeker.StartPath(transform.position, _target + new Vector3(0f, 2.5f, 0), OnPathComplete);

            _way = WayPoint.Going;
        }

        if (_way == WayPoint.Walking)
        {
            _goingTarget = new Vector3(
                _locationGenerator.Size *
                _locationGenerator.Rand.Next(0, (int) _locationGenerator.LongOfGrassBlock * 9 / 10),
                -_locationGenerator.Size *
                _locationGenerator.Rand.Next(0, (int) _locationGenerator.HeightOfGrassBlock / 2), 0f);
            _target = _goingTarget;
            // _target = GameObject.Find("Player").transform.position;

            _seeker.StartPath(transform.position, _target, OnPathComplete);
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
