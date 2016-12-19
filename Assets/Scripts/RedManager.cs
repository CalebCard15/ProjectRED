using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;

	public Boundary(float xMin, float xMax, float yMin, float yMax)
	{
		this.xMin = xMin;
		this.xMax = xMax;
		this.yMin = yMin;
		this.yMax = yMax;

	}
}

public class RedManager : MonoBehaviour 
{
	//RED's movement speed
	public float speed = 6f;

	//Delay Time until boost can be used must be greater than 1 (time of animation)
	public float boostTimer = 1.25f;

	Vector2 movement;
	Animator anim;
	float restartTimer;
	Rigidbody2D rigidbody;

	public bool redIsDead;
	public bool isBoosting;

	//Set up the boundary to make sure R.E.D cannot go off screen
	public Boundary boundary = new Boundary(-7.35f, 7.35f, -4.5f, 4.5f);

	private float timestamp = 0.0f;


	// Use this for initialization
	void Awake () 
	{
		anim = GetComponent <Animator>();
		rigidbody = GetComponent <Rigidbody2D>();
		redIsDead = false;
		isBoosting = false;
	}
	
	// Update is called once per frame
	void Update () 
	{

		//Check if boosting
		if(Input.GetKeyDown("space") && Time.time >= timestamp)
		{
			timestamp = Time.time + boostTimer;
			anim.SetTrigger("RedBoost");
		}
			

		//Check current state to see if boosting
		if(anim.GetCurrentAnimatorStateInfo(0).IsTag ("Boosting"))
			isBoosting = true;
		else
			isBoosting = false;



		//Get the direction the player is moving
		float xDir = Input.GetAxisRaw ("Horizontal");
		float yDir = Input.GetAxisRaw ("Vertical");

		//Move in the direction the player is moving
		Move (xDir, yDir);
			

	}

	void Move (float x, float y)
	{
		Vector2 movement = new Vector2 (x, y);
		rigidbody.velocity = movement * speed;
		
		transform.position = new Vector2 
			(
				Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax), 
				Mathf.Clamp (rigidbody.position.y, boundary.yMin, boundary.yMax)
			);
		
	}
	
	
	
}
