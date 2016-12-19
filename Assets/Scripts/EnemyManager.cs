using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour 
{
	public RedManager redManager;
	public GameObject enemy;
	public float spawnTime = 2f;
	private const float START_TIME = 2f;
	public Transform[] spawnPoints;
	private int level;

	// Use this for initialization
	IEnumerator Start () 
	{
		while(true)
		{
			level = GameManager.instance.GetLevel();
			if(level > 2)
			{
				spawnTime = START_TIME/Mathf.Log(level);
			}
			yield return new WaitForSeconds(spawnTime);
			Spawn ();
		}

	}

	void Spawn () 
	{
		if (redManager.redIsDead)
			return;

		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		
		Instantiate (enemy, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
	}


}
