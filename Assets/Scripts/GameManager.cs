using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;		//Set up the static instance of game manager
	private int score;								//Used to keep game score
	private int level;								//Used to see what level of difficulty the player is on
	public GUIText scoreText;						//Text field to display the score
	public GUIText levelText;						//Text field used to display the current level of difficulty
	public GUIText gameOverText;					//Text displayed to the player when the game is over

	private bool gameOver;							//True if game is over
	private int highScore;							//Keeps track of the current highest score


	// Use this for initialization
	void Start () 
	{

		//If there is no current GameManger
		if(instance == null)
		{
			//Make this the new GameManager
			instance = this;
			score = 0;
		}
			

		//If there is an instance and it is not this one
		else if(instance != this)
		{
			//Then destroy it to keep singelton fidelity
			Destroy(gameObject);
		}

		gameOverText.text = "";
		gameOver = false;
		highScore = PlayerPrefs.GetInt("High Score");
		UpdateText();


	}
	
	// Update is called once per frame
	void Update () 
	{
		if(gameOver)
		{
			if(Input.GetKeyDown(KeyCode.R))
			{
				Application.LoadLevel(Application.loadedLevel);
			}
		}

		if((int)Time.timeSinceLevelLoad/10 == 0)
		{
			level = 1;
		}
		else
		{
			level = (int)Time.timeSinceLevelLoad/10;
		}
		UpdateText();
	}

	public void AddScore(int scoreAdder)
	{
		score += scoreAdder;
		UpdateText();
	}

	public int GetLevel()
	{
		return level;
	}

	public void GameOver()
	{
		//Check if the current score is bigger than the high score
		if(score > highScore)
		{
			highScore = score;
			PlayerPrefs.SetInt("High Score", highScore);
		}
			
		gameOverText.text = "RIP \n\n Press \"R\" to Restart";
		gameOver = true;

	}

	void UpdateText()
	{
		scoreText.text = "Score: " + score + "\nHigh Score: " + highScore;
		levelText.text = "Level: " + level;

	}


}
