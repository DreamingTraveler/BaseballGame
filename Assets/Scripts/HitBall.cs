using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBall : MonoBehaviour {
	Animator animator;

	GameObject ball;
	GameObject hitting_point;
	Rigidbody rig;
	public bool isSwing = false;
	private bool canHitBall = false;
    private int strick = 0;
    private int badball = 0;
	//Vector3 pitchPos = GameObject.Find ("Pitching_Point").transform.position;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		ball = GameObject.FindGameObjectWithTag("Ball");
		hitting_point = GameObject.Find ("Hitting_Point");
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("space")){
			animator.SetTrigger ("isHit");
		}
	}
		
	public bool CanHit(GameObject ball){
		Vector3 ballPos = ball.transform.position;
		if(ballPos.x >= 171.0f && ballPos.x <= 220.0f && ballPos.y >= 11.0f && ballPos.y <= 27.0f && ballPos.z >= 176.0f && ballPos.z <= 228.0f){
			return true;
		}
		return false;
	}	

    public void JudgeBall(GameObject ball)
    {
        Vector3 ballPos = ball.transform.position;
        if (ballPos.x >= 171.0f && ballPos.x <= 220.0f && ballPos.y >= 11.0f && ballPos.y <= 27.0f && ballPos.z >= 176.0f && ballPos.z <= 228.0f)
        {
            strick++;
        }
        else badball++;
        if (strick >= 3) {
            strick = 0;
            Debug.Log("Strick-Out!!!");
        }
        if (badball >= 4) {
            badball = 0;
            Debug.Log("Base-On-Balls!!!");
        } 
    }

	public void Swing(GameObject ball){
		Vector3 pitchPos = GameObject.Find ("Pitching_Point").transform.position;
		Vector3 hitting_point = GameObject.Find ("Hitting_Point").transform.position;
		print (ball.transform.position);
		if (CanHit(ball)) {
			print ("HIT!!!!!!!!!!!!!!!");
			ball.GetComponent<Rigidbody> ().velocity = (new Vector3(hitting_point.x, Input.mousePosition.y, hitting_point.z)).normalized * 88.2f;

		} 
	}
}
