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
	public float hittingPointMovingSpeed;
	public Vector3 targetPos;
	public bool canChooseBall = true;

	private Vector3 pitchPos;
	private Vector3 mousePos;

	private GameObject cloneBall;
	private GameObject hitter;
	private GameObject hittingPoint;
	private GameObject targetPoint;
	private GameObject cursor;
	private MeshRenderer targetMesh;
	private Button confirmBallPos;
	// Use this for initialization
	void Start () {
		pitcherAnimator = GameObject.FindGameObjectWithTag("Pitcher").GetComponent<Animator> ();
		pitcherAnimator.Play("Pitcher_Idle");
		hitter = GameObject.FindGameObjectWithTag ("Hitter");
		hittingPoint = GameObject.Find ("Hitting_Point");
		targetPoint = GameObject.Find ("Pitching_Target");
		targetPos = targetPoint.transform.position;
		targetMesh = targetPoint.GetComponent<MeshRenderer> ();
		cursor = GameObject.FindGameObjectWithTag ("Cursor");
		confirmBallPos = GameObject.Find ("Confirm").GetComponent<Button>();
		//ballAnimator = GameObject.FindGameObjectWithTag ("Ball").GetComponent<Animator> ();
		//ballAnimator.Play ("Curve");

	}
	
	// Update is called once per frame
	void Update () {
	//	Debug.Log (Input.mousePosition);
		if (isPitching) {
			CallHitter (cloneBall);
		}

		if (canChooseBall) {
			ChooseBallPosition ();
			targetPoint.transform.position = targetPos;
		} else {
			targetMesh.enabled = false;
			confirmBallPos.gameObject.SetActive (false);
		}
	}

	public void ChooseBallPosition(){
		targetMesh.enabled = true;
		confirmBallPos.gameObject.SetActive (true);
		if (Input.GetKey(KeyCode.UpArrow)) {
			targetPos.y += 0.1f;
		}

		if (Input.GetKey(KeyCode.DownArrow)) {
			targetPos.y -= 0.1f;
		}

		if (Input.GetKey(KeyCode.RightArrow)) {
			targetPos.x -= 0.1f;
			targetPos.z += 0.1f;
		}

		if (Input.GetKey(KeyCode.LeftArrow)) {
			targetPos.x += 0.1f;
			targetPos.z -= 0.1f;
		}
	}

	public void ChooseBallType(){
		if (isPitching == false) {
			//pitcherAnimator.SetTrigger ("isPitch");
			canChooseBall = true;
			//targetPos = new Vector3 ((Input.mousePosition.x - 55.0f), (175.0f-Input.mousePosition.y) - pitchPos.y , pitchPos.z);

			//Invoke ("PitchAnimate", 2.0f);
		}
	}

	public void CallAnimate(){
		canChooseBall = false;
		Invoke ("PitchAnimate", 2.0f);
	}

	private void PitchAnimate(){
		pitcherAnimator.SetTrigger ("isPitch");
		//ballAnimator.Play ("Curve");
		hittingPoint.transform.position = new Vector3 (-155f, 19.3f, 633f);

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

		cloneBall.GetComponent<Rigidbody> ().velocity = (targetPos - pitchPos).normalized * speed;
		if (ballMode != 0) {
			gameObject.GetComponent<BreakBall> ().SetBreakBall (cloneBall, ballMode);
		}
	}

	public void SetModeAsFourSeam(){
		ballMode = 0;
		hittingPointMovingSpeed = 5500f;
		speed = 350f;
	}

	public void SetModeAsSlider(){
		ballMode = 1;
		hittingPointMovingSpeed = 7000f;
		speed = 270f;
	}

	public void SetModeAsCutter(){
		ballMode = 2;
		hittingPointMovingSpeed = 6300f;
		speed = 300f;
	}

	public void SetModeAsFork(){
		ballMode = 3;
		hittingPointMovingSpeed = 7000f;
		speed = 270f;
	}

	private void CallHitter(GameObject cloneBall){
		MoveHittingPoint ();
		if (Input.GetKeyDown ("space")) {
		    hitter.GetComponent<HitBall> ().Swing (cloneBall);
		}
	}

	private void MoveHittingPoint(){
		if (hitter.GetComponent<HitBall> ().CanHit (cloneBall)) {
			//hitting_point.GetComponent<Rigidbody> ().velocity = hitting_point_end.normalized * 100.0f;
			hittingPoint.transform.Translate(Vector3.forward * Time.deltaTime * hittingPointMovingSpeed);
		}
	}
}
