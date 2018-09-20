using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public float MoveSpeed = 5f;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < - 0.5f)
	    {
	       // transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") *  MoveSpeed * Time.deltaTime, 0f, 0f));
            //gameObject.transform.position = new Vector3(Input.GetAxisRaw("Horizontal") * MoveSpeed * Time.deltaTime, 0f, 0f);
            _rigidbody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * MoveSpeed, _rigidbody2D.velocity.y);
	    }

	    if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
	    {
            //transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * MoveSpeed * Time.deltaTime, 0f));
	        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Input.GetAxisRaw("Vertical") * MoveSpeed);
        }

	    if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
	    {
	        _rigidbody2D.velocity = new Vector2(0f, _rigidbody2D.velocity.y);
        }

	    if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
	    {
	        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0f);
	    }
    }
}
