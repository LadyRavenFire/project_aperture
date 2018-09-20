using UnityEngine;

public class CreatePathes : MonoBehaviour
{

    private AstarPath _path;

    void Start()
    {
        _path = gameObject.GetComponent<AstarPath>();
        _path.Scan();
    }
}
