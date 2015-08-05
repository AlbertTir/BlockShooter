using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	private ScreenController screenController;
	
	private float ScreenHeight;
	private float ScreenWidth;

	public GameObject enemyPrefab;
	public GameObject enemyPrefab2;
	
	private int enemySpawnTimer;
	private int enemyBigSpawnTimer;


	// Use this for initialization
	void Start () {
		//screenController = gameObject.AddComponent<ScreenController> (); // AddComponent adds other gameobjects to the current of type classname
		//screenController = gameObject.GetComponent<ScreenController> ();
		screenController = GameObject.Find ("Main Camera").GetComponent<ScreenController>();
		/* 
		 * the above line basically finds a game object called Main Camera, and gets a component which we specify to be ScreenController
		 * since ScreenController was instantiated by Main Camera, Start and Update works and will give off the proper values here when we call the getter functions
		 * there is AddComponent, GetComponent and Instantiate, 2 of these adds a "new" object/element
		*/
		ScreenHeight = screenController.GetScreenHeight();
		ScreenWidth = screenController.GetScreenWidth();
		//Debug.Log (ScreenHeight  + ", " + ScreenWidth);
	}
	
	// Update is called once per frame
	void Update () {
		if(enemySpawnTimer == 60)
		{
			SpawnEnemy ();
			enemySpawnTimer = 0;
		}

		if(enemyBigSpawnTimer == 60)
		{
			SpawnBigEnemy();
			enemyBigSpawnTimer = 0;
		}
		enemySpawnTimer++;
		enemyBigSpawnTimer++;
	}

	private void SpawnEnemy()
	{
		Vector3 spawnPosition = transform.position;
		
		spawnPosition.y = (ScreenHeight/2 * (float)1.25); // formula for a y spawn position outside the camera range, for this case it gives 6.25
		
		float randomPosition = Random.Range (-((ScreenWidth/2) * (float)0.95), ((ScreenWidth/2) * (float)0.95));
		spawnPosition.x = randomPosition;
		//enemyPrefab2.transform.localScale = new Vector3(3,3,3);
		GameObject.Instantiate (enemyPrefab, spawnPosition, Quaternion.identity);
		
		//Debug.Log (randomPosition);
	}

	private void SpawnBigEnemy()
{
		Vector3 spawnBigPosition = transform.position;

		spawnBigPosition.y = (ScreenHeight / 2 * (float)1.25); // formula for a y spawn position outside the camera range, for this case it gives 6.25

		float randomPosition = Random.Range (-((ScreenWidth / 2) * (float)0.95), ((ScreenWidth / 2) * (float)0.95));
		spawnBigPosition.x = randomPosition;
		spawnBigPosition.z = 0; // for some reason I need to put this here but not for the small one; this makes it spawn close to the background
		
		GameObject.Instantiate (enemyPrefab2, spawnBigPosition, Quaternion.identity);
	}
	
	private		 void DespawnEnemy()
	{
		Vector3 tempPosition = transform.position; //copies the ship's current position, ensuring the ship maintains its z position
		
		float yMax = Camera.main.orthographicSize; // added the 3 just so it can go slightly off screen before disappearing
		
		if(tempPosition.y < -yMax)
		{
			DestroyObject (gameObject); // destroys the enemy when it hits the bottom of the screen
		}
	}
}
