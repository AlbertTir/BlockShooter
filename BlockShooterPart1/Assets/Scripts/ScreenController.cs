using UnityEngine;
using System.Collections;

public class ScreenController : MonoBehaviour {

	private float ScreenHeight;
	private float ScreenWidth;

	// Use this for initialization
	void Start () {
		ScreenWidth = Camera.main.orthographicSize * Camera.main.aspect * 2;
		ScreenHeight = Camera.main.orthographicSize * 2;
		Debug.Log ("screen started in .cs");
		Debug.Log (ScreenWidth + ", " + ScreenHeight);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public float GetScreenHeight()
	{
		return ScreenHeight;
		//return Camera.main.orthographicSize * 2;
	}

	public float GetScreenWidth()
	{
		return ScreenWidth;
		//return Camera.main.orthographicSize * Camera.main.aspect * 2;
	}
}
