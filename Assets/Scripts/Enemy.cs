using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public float speed = 4f;
	private const float START_SPEED = 4f;
	private int level;


	GameObject red; 				//Reference to red's GameObject
	RedManager redManager;			//Reference to red's Manager
	Animator redAnim;				//Reference to red's Animator



	// Use this for initialization
	void Awake () 
	{
		red = GameObject.FindGameObjectWithTag("Player");
		redManager = red.GetComponent<RedManager>();
		redAnim = red.GetComponent<Animator>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject == red)
		{
			if(!redManager.isBoosting)
			{
				redAnim.SetTrigger("RedDeath");
				GameManager.instance.GameOver();
			}

			else
			{
				Destroy(this.gameObject);
				GameManager.instance.AddScore(50*level);
			}

		}
	}

	// Update is called once per frame
	void Update () 
	{
		level = GameManager.instance.GetLevel();
		if(level > 2)
		{
			speed = START_SPEED * Mathf.Log(level);
		}
		Move ();
	}

	// Move the enemy
	void Move()
	{

		if(!redManager.redIsDead)
		{
			transform.position = Vector2.MoveTowards(this.transform.position, red.transform.position, speed * Time.deltaTime);
		}
			

	}

	IEnumerator GameOver()
	{
		yield return new WaitForSeconds(2);
		Application.LoadLevel(Application.loadedLevel);
	}
}
