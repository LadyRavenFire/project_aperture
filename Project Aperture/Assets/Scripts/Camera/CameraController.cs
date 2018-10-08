using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject _player;
    public float XCoordOfCamera;
    public float YCoordOfcamera;

    private float _mnozitelXCoord, _mnozitelYCoord;

    private float _xSizeOfScreen, _ySizeOfScreen;
    public string NameOfPlayer = "Player";

	// Use this for initialization
	void Start ()
	{
        _player = GameObject.Find(NameOfPlayer);
	    XCoordOfCamera = 0;
	    YCoordOfcamera = 0;

	    _mnozitelXCoord = 0;
	    _mnozitelYCoord = 0;

	    _ySizeOfScreen = 10.8f;
	    _xSizeOfScreen = 19.2f;
	}
	
	// Update is called once per frame
	void Update () {
		CoordinateCounting();
	}

    void CoordinateCounting()
    {
        if (_player.transform.position.x > ((_mnozitelXCoord * _xSizeOfScreen) + _xSizeOfScreen/2))
        {
            _mnozitelXCoord++;
            XCoordOfCamera = _mnozitelXCoord * _xSizeOfScreen;
            gameObject.transform.position = new Vector3(XCoordOfCamera, YCoordOfcamera, -10);
        }

        if (_player.transform.position.x < (_mnozitelXCoord * _xSizeOfScreen) - _xSizeOfScreen / 2)
        {
            _mnozitelXCoord--;
            XCoordOfCamera = _mnozitelXCoord * _xSizeOfScreen;
            gameObject.transform.position = new Vector3(XCoordOfCamera, YCoordOfcamera, -10);
        }

        if (_player.transform.position.y < ((_mnozitelYCoord * _ySizeOfScreen) - _ySizeOfScreen/2))
        {
            _mnozitelYCoord--;
            YCoordOfcamera = _mnozitelYCoord * _ySizeOfScreen;
            gameObject.transform.position = new Vector3(XCoordOfCamera, YCoordOfcamera, -10);
        }

        if (_player.transform.position.y > ((_mnozitelYCoord * _ySizeOfScreen) + _ySizeOfScreen / 2))
        {
            _mnozitelYCoord++;
            YCoordOfcamera = _mnozitelYCoord * _ySizeOfScreen;
            gameObject.transform.position = new Vector3(XCoordOfCamera, YCoordOfcamera, -10);
        }

    }
}
