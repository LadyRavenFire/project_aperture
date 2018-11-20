using UnityEngine;

public class CollectRadioPart : MonoBehaviour
{
    public string RadioTag = "RadioPart";

    private int _radioPartsMax;
    private int _radioPartsHave;

    private GameObject[] _radioObjects;

    void Start()
    {
        _radioPartsHave = 0;
        _radioObjects = GameObject.FindGameObjectsWithTag(RadioTag);
        _radioPartsMax = _radioObjects.Length;
       // print(_radioPartsMax);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // print("lol1");
        if (col.gameObject.tag == RadioTag)
        {
           // print("lol2");
            _radioPartsHave++;
            Destroy(col.gameObject);
        }
    }

    public int ReturnMax()
    {
        return _radioPartsMax;
    }

    public int ReturnHave()
    {
        return _radioPartsHave;
    }
}
