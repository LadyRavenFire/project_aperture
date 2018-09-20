using Pathfinding;
using UnityEngine;

public class GreenMobMovement : MonoBehaviour
{
    private GameObject _water;
	// Use this for initialization
	void Start () {
	    var seeker = GetComponent<Seeker>();

        _water = GameObject.FindWithTag("Food");

	    seeker.StartPath(transform.position, _water.transform.position + new Vector3(1.3f,0,0), OnPathComplete);
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
