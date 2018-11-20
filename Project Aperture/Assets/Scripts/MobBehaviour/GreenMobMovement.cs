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

        DoNotStuckMob();

        Way();
    }

    void FindMobs()
    {
        _greenMobs = GameObject.FindGameObjectsWithTag("GreenMob");

        for (int i = 0; i < _greenMobs.Length; i++) //обновлять т.к. мобы деплоятся!
        {
            if (_greenMobs[i].name == gameObject.name)
            {
                _greenMobs[i] = null;
                _greenMobs = _greenMobs.Where(x => x != null).ToArray();
            }
        }
    }

    void ChangeFoodSpending()
    {
        if (_timeManager.NowSeason == TimeManager.Season.Spring) // wake up
        {
            switch (_timeManager.NameOfTime)
            {
                case TimeManager.TimeName.Morning:
                    _foodNeeds.FoodSendInSecond = 0.035f;
                    break;
                case TimeManager.TimeName.Day:
                    _foodNeeds.FoodSendInSecond = 0.0265f;
                    break;
                case TimeManager.TimeName.Evening:
                    _foodNeeds.FoodSendInSecond = 0.0165f;
                    break;
                case TimeManager.TimeName.Night:
                    _foodNeeds.FoodSendInSecond = 0.015f;
                    _waterNeeds.WaterSendInSecond = 0.002f;
                    break;
            }
        }

        if (_timeManager.NowSeason == TimeManager.Season.Summer) 
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
                    _waterNeeds.WaterSendInSecond = 0.002f;
                    break;
            }
        }

        if (_timeManager.NowSeason == TimeManager.Season.Autumn) 
        {
            switch (_timeManager.NameOfTime)
            {
                case TimeManager.TimeName.Morning:
                    _foodNeeds.FoodSendInSecond = 0.02f;
                    break;
                case TimeManager.TimeName.Day:
                    _foodNeeds.FoodSendInSecond = 0.013f;
                    break;
                case TimeManager.TimeName.Evening:
                    _foodNeeds.FoodSendInSecond = 0.008f;
                    break;
                case TimeManager.TimeName.Night:
                    _foodNeeds.FoodSendInSecond = 0.005f;
                    _waterNeeds.WaterSendInSecond = 0.002f;
                    break;
            }
        }

        if (_timeManager.NowSeason == TimeManager.Season.Winter) 
        {
            _foodNeeds.FoodSendInSecond = 0.003f;
            _waterNeeds.WaterSendInSecond = 0.003f;
        }

    } //TODO WaterSpending

    void Way()
    {
        if (_timeManager.NowSeason == TimeManager.Season.Spring)
        {
            FindMobs();
            SpringBehaviour();
        }

        if (_timeManager.NowSeason == TimeManager.Season.Summer)
        {
            FindMobs();
            SummerBehaviour();
        }

        if (_timeManager.NowSeason == TimeManager.Season.Autumn)
        {
            FindMobs();
            AutumnBehaviour();
        }

        if (_timeManager.NowSeason == TimeManager.Season.Winter)
        {
            WinterBehaviour();
        }

    }

    void DoNotStuckMob()
    {
        if ((_timeManager.NowSeason != TimeManager.Season.Winter &&
            _timeManager.NameOfTime != TimeManager.TimeName.Night) || (_timeManager.NowSeason == TimeManager.Season.Autumn && _timeManager.NameOfTime == TimeManager.TimeName.Night))
        {
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
        }   
    }

    void SpringBehaviour()
    {
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

        if (_way == WayPoint.Going)
        {
            if (_timeToBreeding <= 0f)
            {
                foreach (var t in _greenMobs)
                {
                    if (Math.Abs(t.transform.position.x - gameObject.transform.position.x) < 4f
                        && Math.Abs(t.transform.position.y - gameObject.transform.position.y) < 4f)
                    {
                        if (_foodNeeds.ReturnFood() > 50 && _waterNeeds.ReturnWater() > 50)
                        {
                            _way = WayPoint.Breeding;
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

        if (_way == WayPoint.Choose)
        {
            if (_timeManager.NameOfTime == TimeManager.TimeName.Night)
            {
                _seeker.CancelCurrentPathRequest();
            }
            else
            {
                if (_waterNeeds.ReturnWater() < _foodNeeds.ReturnFood())
                {
                    _way = _waterNeeds.ReturnWater() < 85 ? WayPoint.Water : WayPoint.Walking;
                }
                else
                {
                    _way = _foodNeeds.ReturnFood() < 85 ? WayPoint.Food : WayPoint.Walking;
                }
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
            int number = 0;
            var distance = (Math.Abs(_foods[number].transform.position.x - gameObject.transform.position.x -
                                       _locationGenerator.LongOfGrassBlock / 2) +
                              Math.Abs(_foods[number].transform.position.y - gameObject.transform.position.y +
                                       _locationGenerator.LongOfGrassBlock / 2));

            for (int i = 0; i < _foods.Length; i++)
            {
                var need = _foods[i].GetComponent<FoodHave>();
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
            int number = 0;

            var distance = (Math.Abs(_waters[number].transform.position.x - gameObject.transform.position.x -
                                       _locationGenerator.LongOfGrassBlock / 2) +
                              Math.Abs(_waters[number].transform.position.y - gameObject.transform.position.y +
                                       _locationGenerator.LongOfGrassBlock / 2));

            for (int i = 0; i < _waters.Length; i++)
            {
                var need = _waters[i].GetComponent<WaterHave>();
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
                _locationGenerator.Rand.Next(0, (int)_locationGenerator.LongOfGrassBlock * 9 / 10),
                -_locationGenerator.Size *
                _locationGenerator.Rand.Next(0, (int)_locationGenerator.HeightOfGrassBlock / 2), 0f);
            _target = _goingTarget;

            _seeker.StartPath(transform.position, _target, OnPathComplete);
            _way = WayPoint.Going;
        }
    }

    void SummerBehaviour()
    {
        if (_greenMobs.Length >= _locationGenerator.Size * 5)
        {
            _timeToBreeding = 5f;
        }

        if (_timeToBreeding > 0f)
        {
            _timeToBreeding = _timeToBreeding - Time.deltaTime;
        }
        else
        {
            _timeToBreeding = 0;
        }

        if (_way == WayPoint.Going)
        {
            if (_timeToBreeding <= 0f)
            {
                foreach (var t in _greenMobs)
                {
                    if (Math.Abs(t.transform.position.x - gameObject.transform.position.x) < 4f
                        && Math.Abs(t.transform.position.y - gameObject.transform.position.y) < 4f)
                    {
                        if (_foodNeeds.ReturnFood() > 50 && _waterNeeds.ReturnWater() > 50)
                        {
                            _way = WayPoint.Breeding;
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

        if (_way == WayPoint.Choose)
        {
            if (_timeManager.NameOfTime == TimeManager.TimeName.Night)
            {
                bool isRedMobNear = false;
                GameObject[] _redMobs = GameObject.FindGameObjectsWithTag("RedMob"); ;
                foreach (var t in _redMobs)
                {
                    if (gameObject.transform.position.x > t.transform.position.x - 0.5f &&
                        gameObject.transform.position.x <= t.transform.position.x + 0.5f &&
                        gameObject.transform.position.y > t.transform.position.y - 0.5f &&
                        gameObject.transform.position.y <= t.transform.position.y + 2.7f)
                    {
                        isRedMobNear = true;
                    }

                    if (isRedMobNear)
                    {
                        _way = WayPoint.Walking;
                    }
                }
            }
            else
            {
                if (_waterNeeds.ReturnWater() < _foodNeeds.ReturnFood())
                {
                    _way = _waterNeeds.ReturnWater() < 85 ? WayPoint.Water : WayPoint.Walking;
                }
                else
                {
                    _way = _foodNeeds.ReturnFood() < 85 ? WayPoint.Food : WayPoint.Walking;
                }
            }       
        }

        if (_way == WayPoint.Breeding)
        {
            _timeToBreeding = 7f;
            Instantiate(GreenMobPrefab,
                new Vector3(gameObject.transform.position.x + 0.3f, gameObject.transform.position.y + 0.3f,
                    gameObject.transform.position.z), Quaternion.identity);
            _way = WayPoint.Going;
        }

        if (_way == WayPoint.Food)
        {
            int number = 0;
            var distance = (Math.Abs(_foods[number].transform.position.x - gameObject.transform.position.x -
                                       _locationGenerator.LongOfGrassBlock / 2) +
                              Math.Abs(_foods[number].transform.position.y - gameObject.transform.position.y +
                                       _locationGenerator.LongOfGrassBlock / 2));

            for (int i = 0; i < _foods.Length; i++)
            {
                var need = _foods[i].GetComponent<FoodHave>();
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
            int number = 0;

            var distance = (Math.Abs(_waters[number].transform.position.x - gameObject.transform.position.x -
                                       _locationGenerator.LongOfGrassBlock / 2) +
                              Math.Abs(_waters[number].transform.position.y - gameObject.transform.position.y +
                                       _locationGenerator.LongOfGrassBlock / 2));

            for (int i = 0; i < _waters.Length; i++)
            {
                var need = _waters[i].GetComponent<WaterHave>();
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
                _locationGenerator.Rand.Next(0, (int)_locationGenerator.LongOfGrassBlock * 9 / 10),
                -_locationGenerator.Size *
                _locationGenerator.Rand.Next(0, (int)_locationGenerator.HeightOfGrassBlock / 2), 0f);
            _target = _goingTarget;

            _seeker.StartPath(transform.position, _target, OnPathComplete);
            _way = WayPoint.Going;
        }
    }

    void AutumnBehaviour()
    {
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

        if (_way == WayPoint.Going)
        {
            if (_timeToBreeding <= 0f)
            {
                foreach (var t in _greenMobs)
                {
                    if (Math.Abs(t.transform.position.x - gameObject.transform.position.x) < 4f
                        && Math.Abs(t.transform.position.y - gameObject.transform.position.y) < 4f)
                    {
                        if (_foodNeeds.ReturnFood() > 50 && _waterNeeds.ReturnWater() > 50)
                        {
                            _way = WayPoint.Breeding;
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

        if (_way == WayPoint.Choose)
        {
            if (_timeManager.NameOfTime == TimeManager.TimeName.Night)
            {
                _seeker.CancelCurrentPathRequest();
                _timeToBreeding = 20f;
            }
            else
            {
                if (_waterNeeds.ReturnWater() < _foodNeeds.ReturnFood())
                {
                    _way = _waterNeeds.ReturnWater() < 85 ? WayPoint.Water : WayPoint.Walking;
                }
                else
                {
                    _way = _foodNeeds.ReturnFood() < 85 ? WayPoint.Food : WayPoint.Walking;
                }
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
            int number = 0;
            var distance = (Math.Abs(_foods[number].transform.position.x - gameObject.transform.position.x -
                                       _locationGenerator.LongOfGrassBlock / 2) +
                              Math.Abs(_foods[number].transform.position.y - gameObject.transform.position.y +
                                       _locationGenerator.LongOfGrassBlock / 2));

            for (int i = 0; i < _foods.Length; i++)
            {
                var need = _foods[i].GetComponent<FoodHave>();
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
            int number = 0;

            var distance = (Math.Abs(_waters[number].transform.position.x - gameObject.transform.position.x -
                                       _locationGenerator.LongOfGrassBlock / 2) +
                              Math.Abs(_waters[number].transform.position.y - gameObject.transform.position.y +
                                       _locationGenerator.LongOfGrassBlock / 2));

            for (int i = 0; i < _waters.Length; i++)
            {
                var need = _waters[i].GetComponent<WaterHave>();
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
                _locationGenerator.Rand.Next(0, (int)_locationGenerator.LongOfGrassBlock * 9 / 10),
                -_locationGenerator.Size *
                _locationGenerator.Rand.Next(0, (int)_locationGenerator.HeightOfGrassBlock / 2), 0f);
            _target = _goingTarget;

            _seeker.StartPath(transform.position, _target, OnPathComplete);
            _way = WayPoint.Going;
        }
    }

    void WinterBehaviour()
    {
        if (_way == WayPoint.Choose)
        {
            if (gameObject.transform.position.x > _target.x - 0.5f &&
                gameObject.transform.position.x <= _target.x + 0.5f &&
                gameObject.transform.position.y > _target.y - 0.5f &&
                gameObject.transform.position.y <= _target.y + 2.7f)
            {
                _seeker.CancelCurrentPathRequest();
            }           
        }

        if (_way == WayPoint.Going)
        {
            WinterPath();
        }

        if (_way == WayPoint.Breeding)
        {
            WinterPath();
        }

        if (_way == WayPoint.Food)
        {
            WinterPath();
        }

        if (_way == WayPoint.Water)
        {
            WinterPath();
        }

        if (_way == WayPoint.Walking)
        {
            WinterPath();
        }
    }

    void WinterPath()
    {
        _greenMobs = GameObject.FindGameObjectsWithTag("GreenMob");
        int numberOfGreens = 0;
        for (int i = 0; i < _greenMobs.Length; i++) //обновлять т.к. мобы деплоятся!
        {
            if (_greenMobs[i].name == gameObject.name)
            {
                numberOfGreens = i;
            }
        }

        _target = new Vector3(0 + numberOfGreens, 0, 0);

        _seeker.StartPath(transform.position, _target, OnPathComplete);

        _way = WayPoint.Choose;
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
