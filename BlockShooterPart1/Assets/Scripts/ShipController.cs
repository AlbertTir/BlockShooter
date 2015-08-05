using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

	public float moveSpeed;

	private Vector2 mousePosition;
	private Vector2 currentPosition;
	private Transform spawnPoint;

	public GameObject bulletPrefab;
	
	// Use this for initialization
	void Start () {
		spawnPoint = GameObject.Find ("SpawnPoint").transform;
		Debug.Log (spawnPoint);
		transform.position = spawnPoint.position;
	}
	
	// Update is called once per frame
	void Update () {
		currentPosition = transform.position;

		// gets the mouse position according to the main camera
		mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint (mousePosition);

		// move our ship sprite to the mouse position based on moveSpeed. We use moveSpeed here instead of deltaTime since we want it to be instantaneous
		transform.position = Vector2.Lerp (currentPosition, mousePosition, moveSpeed);

		if(Input.GetButtonDown ("Fire1")){
			GameObject.Instantiate(bulletPrefab, mousePosition, Quaternion.identity);
			// Instantiate clones the original bulletPrefab at mouseposition with no changes in rotation (Quaternion.identity)
		}

		EnforceBounds ();
	}

	private void EnforceBounds()
	{
		Vector3 newPosition = transform.position; //copies the ship's current position, ensuring the ship maintains its z position
		Camera mainCamera = Camera.main;
		Vector3 cameraPosition = mainCamera.transform.position;
		
		float xDist = mainCamera.aspect * mainCamera.orthographicSize; // calculates distance from the center of the screen to one of its edges
		float xMax = cameraPosition.x + xDist;						   // adds the distance to the camera's x position
		float xMin = cameraPosition.x - xDist;
		// essentially the 3 above lines finds the max and min range of the x-axis
		
		if(newPosition.x < xMin || newPosition.x > xMax)
		{

		}
		
		// since the camera is at the center of the screen, the yMin and yMax is just the negative and positive values of the same value
		// therefore we can condense this to just one line
		float yMax = mainCamera.orthographicSize;
		
		if(newPosition.y < -yMax || newPosition.y > yMax)
		{

		}
		
		transform.position = newPosition; // updates the zombie's position with newPosition
		// this will be the same position the ship had when this method is called if the ship is still within horizontal bounds
	}
}
