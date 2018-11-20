using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float LocationTime;
    public int DayOfYear;

    public enum TimeName
    {
        Morning,
        Day,
        Evening,
        Night
    }

    public enum Season
    {
        Spring,
        Summer,
        Autumn,
        Winter
    }

    public TimeName NameOfTime;
    public Season NowSeason;

	// Use this for initialization
	void Start ()
	{
	    //LocationTime = 0f;
	    //DayOfYear = 0; //???
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    ChangeTime();
	}

    void ChangeTime()
    {
        LocationTime += Time.deltaTime;
        if (LocationTime >= 0f && LocationTime <= 20f)
        {
            NameOfTime = TimeName.Morning;
        }
        else
        {
            if (LocationTime >= 20f && LocationTime <= 40f)
            {
                NameOfTime = TimeName.Day;
            }
            else
            {
                if (LocationTime >= 40f && LocationTime <= 60f)
                {
                    NameOfTime = TimeName.Evening;
                }
                else
                {
                    if (LocationTime >= 60f && LocationTime <= 80f)
                    {
                        NameOfTime = TimeName.Night;
                    }
                    else
                    {
                        if (LocationTime >= 80f)
                        {
                            DayOfYear++;
                            if (DayOfYear == 5)
                            {
                                DayOfYear = 0;
                            }
                            LocationTime = 0f;
                            ChangeSeason();
                        }
                    }
                }
            }
        }
    }

    void ChangeSeason()
    {
        if (DayOfYear == 0)
        {
            NowSeason = Season.Spring;
        }
        if (DayOfYear == 1)
        {
            NowSeason = Season.Summer;
        }
        if (DayOfYear == 3)
        {
            NowSeason = Season.Autumn;
        }
        if (DayOfYear == 4)
        {
            NowSeason = Season.Winter;
        }
    }
}
