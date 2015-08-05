using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float enemyMoveSpeed;
	public GameObject enemyBulletPrefab;

	private Vector3 currentPosition;
	private Vector3 targetPosition;
	private Vector3 instCurrentPosition;
	private Vector3 playerShipPosition;
	private GameObject playerShip;

	private int bulletTimeCounter = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		instCurrentPosition = transform.position;
		playerShip = GameObject.Find ("ship");

		if(playerShip != null)
		{
			playerShipPosition = playerShip.transform.position;
			
			if(playerShipPosition.y <= instCurrentPosition.y){
				if(bulletTimeCounter >= 60)
				{
					GameObject.Instantiate (enemyBulletPrefab, instCurrentPosition, Quaternion.identity);
					bulletTimeCounter = 0;
				}
			}
		}

		FlyDown ();
		EnforceBounds ();
		bulletTimeCounter++;
	}

	private void FlyDown(){
		currentPosition = transform.position;
		targetPosition = currentPosition;
		targetPosition.y = currentPosition.y--;
		targetPosition.z = 0;
		transform.position = Vector3.Lerp (targetPosition, currentPosition, enemyMoveSpeed / 50);
	}

	private void EnforceBounds()
	{
		Vector3 tempPosition = transform.position; //copies the ship's current position, ensuring the ship maintains its z position
		/* do something with this later, maybe for pathing

		Vector3 cameraPosition = Camera.main.transform.position;

		float xDist = Camera.main.aspect * Camera.main.orthographicSize;
		float xMax = cameraPosition.x + xDist;
		float xMin = cameraPosition.x - xDist;
		
		if(tempPosition.x < xMin || tempPosition.x > xMax)
		{
			DestroyObject (gameObject);
		}
		*/

		float yMax = Camera.main.orthographicSize + ((float)3); // added the 3 just so it can go slightly off screen before disappearing

		if(tempPosition.y < -yMax || tempPosition.y > yMax)
		{
			DestroyObject (gameObject); // destroys the bullet when it hits the top of the screen...or bottom
		}
		/*
		Vector3 newPosition = transform.position; //copies the ship's current position, ensuring the ship maintains its z position
		Camera mainCamera = Camera.main;
		Vector3 cameraPosition = mainCamera.transform.position;
		
		float xDist = mainCamera.aspect * mainCamera.orthographicSize; // calculates distance from the center of the screen to one of its edges
		float xMax = cameraPosition.x + xDist;						   // adds the distance to the camera's x position
		float xMin = cameraPosition.x - xDist;
		// essentially the 3 above lines finds the max and min range of the x-axis

		currentPosition = transform.position;
		targetRight = currentPosition;
		targetRight.x = mainCamera.aspect + currentPosition.x;
		targetLeft = currentPosition;
		targetLeft.x = -mainCamera.aspect - currentPosition.x;

		if ( newPosition.x > xMax) {
			transform.position = Vector2.Lerp (currentPosition, targetLeft, enemyMoveSpeed);
	        //newPosition.x = Mathf.Clamp (newPosition.x, xMin, xMax);
			//moveDirection.x = -moveDirection.x;	
		} else {
			transform.position = Vector2.Lerp (currentPosition, targetRight, enemyMoveSpeed);
		}
		*/
		// need to get the whole rebouncing deal working

	}

}
