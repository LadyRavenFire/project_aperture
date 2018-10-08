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

    private GameObject[] GreenMobs;
    private GameObject[] Waters;
    private GameObject[] Foods;

    private WaterNeeds _waterNeeds;
    private FoodNeeds _foodNeeds;

    private float _timeToBreeding;

    private Vector3 _changePlace;
    private float _timetoStuck;

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

        _timeToBreeding = 15f;

        Waters = GameObject.FindGameObjectsWithTag("Water");
        Foods = GameObject.FindGameObjectsWithTag("Food");
        GreenMobs = GameObject.FindGameObjectsWithTag("GreenMob");
        
        for (int i = 0; i < GreenMobs.Length; i++)
        {
            if (GreenMobs[i].name == gameObject.name)
            {
                GreenMobs[i] = null;
                GreenMobs = GreenMobs.Where(x => x != null).ToArray();
            }
        }

        _waterNeeds = GetComponent<WaterNeeds>();
        _foodNeeds = GetComponent<FoodNeeds>();

        _timetoStuck = 0f;
        _changePlace = new Vector3(0f,0f,0f);

        _way = WayPoint.Choose;
    }

    void Update()
    {
        GreenMobs = GameObject.FindGameObjectsWithTag("GreenMob");

        if (_changePlace == gameObject.transform.position)
        {
            _timetoStuck += Time.deltaTime;
        }
        else
        {
            _changePlace = gameObject.transform.position;
            _timetoStuck = 0f;
        }

        if (_timetoStuck >= 2f)
        {
            print("Stuck! " + gameObject.name);
            _way = WayPoint.Walking;
        }

        for (int i = 0; i < GreenMobs.Length; i++)
        {
            if (GreenMobs[i].name == gameObject.name)
            {
                GreenMobs[i] = null;
                GreenMobs = GreenMobs.Where(x => x != null).ToArray();
            }
        }

        if (GreenMobs.Length >= _locationGenerator.Size * 1.5)
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

    void Way()
    {        

        if (_way == WayPoint.Going)
        {
            if (_timeToBreeding <= 0f)
            {
                for (int i = 0; i < GreenMobs.Length; i++)
                {
                    if (Math.Abs(GreenMobs[i].transform.position.x - gameObject.transform.position.x) < 4f && Math.Abs(GreenMobs[i].transform.position.y - gameObject.transform.position.y) <4f)
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

            distance = (Math.Abs(Foods[number].transform.position.x - gameObject.transform.position.x -
                                 _locationGenerator.LongOfGrassBlock / 2) +
                        Math.Abs(Foods[number].transform.position.y - gameObject.transform.position.y +
                                 _locationGenerator.LongOfGrassBlock / 2));

            for (int i = 0; i < Foods.Length; i++)
            {
                need = Foods[i].GetComponent<FoodHave>();
                if (need.ReturnFood() > _foodNeeds.ReturnFoodNeeds())
                {
                    if ((Math.Abs(Foods[i].transform.position.x - gameObject.transform.position.x -
                                  _locationGenerator.LongOfGrassBlock / 2) +
                         Math.Abs(Foods[i].transform.position.y - gameObject.transform.position.y +
                                  _locationGenerator.HeightOfGrassBlock / 2)) < distance)
                    {
                        number = i;

                        distance = (Math.Abs(Foods[number].transform.position.x - gameObject.transform.position.x -
                                             _locationGenerator.LongOfGrassBlock / 2) +
                                    Math.Abs(Foods[number].transform.position.y - gameObject.transform.position.y +
                                             _locationGenerator.LongOfGrassBlock / 2));
                    }
                }
            }

            _target = Foods[number].transform.position;
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

            distance = (Math.Abs(Waters[number].transform.position.x - gameObject.transform.position.x -
                                 _locationGenerator.LongOfGrassBlock / 2) +
                        Math.Abs(Waters[number].transform.position.y - gameObject.transform.position.y +
                                 _locationGenerator.LongOfGrassBlock / 2));

            for (int i = 0; i < Waters.Length; i++)
            {
                need = Waters[i].GetComponent<WaterHave>();
                if (need.ReturnWater() > _waterNeeds.ReturnWaterNeed())
                {
                    if ((Math.Abs(Foods[i].transform.position.x - gameObject.transform.position.x -
                                  _locationGenerator.LongOfGrassBlock / 2) +
                         Math.Abs(Foods[i].transform.position.y - gameObject.transform.position.y +
                                  _locationGenerator.HeightOfGrassBlock / 2)) < distance)
                    {
                        number = i;
                        distance = (Math.Abs(Waters[number].transform.position.x - gameObject.transform.position.x -
                                             _locationGenerator.LongOfGrassBlock / 2) +
                                    Math.Abs(Waters[number].transform.position.y - gameObject.transform.position.y +
                                             _locationGenerator.LongOfGrassBlock / 2));
                    }
                }
            }

            _target = Waters[number].transform.position;
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
