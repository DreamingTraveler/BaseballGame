using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBall : MonoBehaviour {
	public Vector3 breakPoint;
	public float force;


	Rigidbody rigidbody;
	GameObject pitcher;
	Vector3 targetPos;

	// Use this for initialization
	void Start () {
		pitcher = GameObject.FindGameObjectWithTag ("Pitcher");
		targetPos = pitcher.GetComponent<Pitch> ().transform.position;

		//Invoke ("ChangeBallDirection", 0.3f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetBreakBall(GameObject ball, int ballMode){
		StartCoroutine (SetDetail (ball, ballMode));
	}

	IEnumerator SetDetail(GameObject ball, int ballMode){
		rigidbody = ball.GetComponent<Rigidbody> ();
		switch (ballMode) {
		case 1://Slider
			yield return new WaitForSeconds (0.36f);
			force = 2000f;
			breakPoint = new Vector3 (-0.2f, -0.8f, -1f);
			break;
		case 2://Cutter
			yield return new WaitForSeconds (0.23f);
			for (int i = 0; i < 20; i++) {
				yield return new WaitForSeconds (0.05f);
				force = 600f;
				breakPoint = new Vector3 (0.2f, -0.2f, -2f);
				rigidbody.AddForce (breakPoint.normalized * force);
			}
			force = 1500f;
			breakPoint = new Vector3 (0.2f, -0.2f, -2f);
			break;
		case 3://Forkball
			yield return new WaitForSeconds (0.32f);
			force = 2100f;
			breakPoint = new Vector3 (0f, -0.7f, 0f);
			break;
		}
		rigidbody.AddForce (breakPoint.normalized * force);

	}
}
