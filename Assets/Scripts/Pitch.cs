using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pitch : MonoBehaviour {
	Animator pitcherAnimator;
	Animator ballAnimator;
	public GameObject ball;
	public float speed;
	public float w, h;
	public bool isPitching = false;
	public Button slider_btn;
	public int ballMode = 0;

	private Vector3 pitchPos;
	public Vector3 targetPos;
	private Vector3 mousePos;

	GameObject cloneBall;
	GameObject hitter;
	GameObject hitting_point;
	// Use this for initialization
	void Start () {
		pitcherAnimator = GameObject.FindGameObjectWithTag("Pitcher").GetComponent<Animator> ();
		pitcherAnimator.Play("Pitcher_Idle");
		hitter = GameObject.FindGameObjectWithTag ("Hitter");
		hitting_point = GameObject.Find ("Hitting_Point");
		//ballAnimator = GameObject.FindGameObjectWithTag ("Ball").GetComponent<Animator> ();
		//ballAnimator.Play ("Curve");

	}
	
	// Update is called once per frame
	void Update () {
	//	Debug.Log (Input.mousePosition);
		if (isPitching) {
			CallHitter (cloneBall);
		}
	}

	public void ChoosePosition(){
		if (Input.GetMouseButtonUp (0) && isPitching == false) {
			//pitcherAnimator.SetTrigger ("isPitch");
			targetPos = new Vector3 ((Input.mousePosition.x - 55.0f), (175.0f-Input.mousePosition.y) - pitchPos.y , pitchPos.z);

			Invoke ("PitchAnimate", 2.0f);
		}
	}

	private void PitchAnimate(){
		pitcherAnimator.SetTrigger ("isPitch");
		//ballAnimator.Play ("Curve");
		hitting_point.transform.position = new Vector3 (133.9f, 19.3f, 303.5f);

		Invoke ("PitchBall", 1.0f);

		//PitchBall();
	}

	public void ReadyToPitch(){
		isPitching = false;
	}

	public void PitchBall(){
		Vector3 pitchPos = GameObject.Find ("Pitching_Point").transform.position;
		Vector3 mouseMovingPos = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x - h, w - Input.mousePosition.y, pitchPos.z));
		Debug.DrawLine(pitchPos, mouseMovingPos, Color.cyan);
		isPitching = true;

		cloneBall = Instantiate (ball) as GameObject;
		cloneBall.name = "CloneBall";
		//mousePos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x,Input.mousePosition.y,0));
		//targetPos = new Vector3 ((Input.mousePosition.x - 385.0f), (403.0f-Input.mousePosition.y) - pitchPos.y , pitchPos.z);
		cloneBall.transform.position = pitchPos;
		cloneBall.GetComponent<Rigidbody> ().velocity = (-mouseMovingPos).normalized * speed;
		if (ballMode != 0) {
			gameObject.GetComponent<BreakBall> ().SetBreakBall (cloneBall, ballMode);
		}
	}

	public void SetModeAsFourSeam(){
		ballMode = 0;
		speed = 350f;
	}

	public void SetModeAsSlider(){
		ballMode = 1;
		speed = 270f;
	}

	public void SetModeAsCutter(){
		ballMode = 2;
		speed = 200f;
	}

	public void SetModeAsFork(){
		ballMode = 3;
		speed = 270f;
	}

	private void CallHitter(GameObject cloneBall){
		MoveHittingPoint ();
		if (Input.GetKeyDown ("space")) {
		    hitter.GetComponent<HitBall> ().Swing (cloneBall);
		}
	}

	private void MoveHittingPoint(){
		Vector3 hitting_point_end = GameObject.Find ("Hitting_Point_End").transform.position;
		if (hitter.GetComponent<HitBall> ().CanHit (cloneBall)) {
			//hitting_point.GetComponent<Rigidbody> ().velocity = hitting_point_end.normalized * 100.0f;
			hitting_point.transform.Translate(Vector3.forward * Time.deltaTime * 600);
		}
	}
}
