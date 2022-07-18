using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlatBallLauncher : MonoBehaviour {

	public List<Transform> TargetsList = new List<Transform>();

	public Rigidbody ball;
	public Transform target;

	public float h = 25;
	public float gravity = -18;

	public bool debugPath;
	public bool StopCalc;


	[SerializeField] private float sensitivity;
	private bool controllable = true;
	public float extralerp;
	public float minPath, maxPath;

	public Vector3 PathControler;
	public Vector3 displacementXZ;
	public Vector3 velocityY;
	public Vector3 velocityXZ;

	public GameObject[] _ball;


    public Game _scriptGame;
    public Rigidbody[] _BALLOVER;
	public int ballgo;


	void Start() 
	{
		ball.useGravity = false;
	}
	public void ClosePathImg()
	{
		foreach (GameObject ball in _ball)
		{
			ball.SetActive(false);
		}
}
	IEnumerator BallCount()
	{

		//send value point to ballover objects
		// _BALLOVER = new _BALLOVER[_scriptGame.point];
		yield return null;
		Vector3 a = CalculateLaunchData ().initialVelocity;
		for (int i = 0; i < ballgo; i++)
		{
			Debug.Log(i.ToString());
			Physics.gravity = PathControler * gravity;
			_BALLOVER[i].gameObject.SetActive(true);
			_BALLOVER[i].useGravity = true;
			_BALLOVER[i].velocity = a;
			yield return new WaitForSecondsRealtime(.3f);

		}
	//foreach (Rigidbody ball in _BALLOVER)
	//{
       
		//Physics.gravity = PathControler * gravity;
		//ball.gameObject.SetActive(true);
		//ball.useGravity = true;
		//ball.velocity = CalculateLaunchData ().initialVelocity;
		// yield return new WaitForSecondsRealtime(.5f);

	//}
}
	void Update() 
	{

		if (Input.GetKeyDown (KeyCode.Space)) 
		{

			StopCalc = false;
            ClosePathImg();
			ballgo =_scriptGame.point;
			StartCoroutine("BallCount");
			//ballgo = 1;
			//for (int i = 0; i == _scriptGame.point; i++)
			//{
				//Launch ();
			//}
			
		}
	
		if (debugPath) 
		{
			DrawPath ();
		}

		if (Input.touchCount > 0 && controllable)
		{
			Touch touch = Input.GetTouch(0);

			PathControler = new Vector3(Mathf.Clamp(PathControler.x + (touch.position.x / 1000 - 0.5f) * sensitivity * Time.deltaTime, minPath, maxPath), 0, 0);
			
		}

		/*if (Input.touchCount > 0 && controllable)
        {
            Touch touch = Input.GetTouch(0);

			transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z + (touch.deltaPosition.x * (sensitivity / 100) * Time.deltaTime), GlobalData.LeftBorder, GlobalData.RightBorder));

			PathControler = new Vector3(Mathf.Clamp(PathControler.x + (touch.deltaPosition.x * 100 * Time.deltaTime) * sensitivity * Time.deltaTime, minPath, maxPath), 0, 0);
		}*/

	}

	void Launch() 
	{
		Physics.gravity = PathControler * gravity;
		ball.useGravity = true;
		ball.velocity = CalculateLaunchData ().initialVelocity;
	}

	LaunchData CalculateLaunchData() 
	{

		float time = 0;

        if (!StopCalc)
        {
			displacementXZ = new Vector3(target.position.x - ball.position.x, 0, target.position.z - ball.position.z);

			time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (-h) / gravity);

			velocityY = PathControler * Mathf.Sqrt(-2 * gravity * h);

			velocityXZ = displacementXZ / time;
		}

		return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);

	}

	void DrawPath() 
	{
		LaunchData launchData = CalculateLaunchData ();
		Vector3 previousDrawPoint = ball.position;

		int resolution = 30;
		for (int i = 1; i <= resolution; i++) 
		{
			float simulationTime = i / (float)resolution * launchData.timeToTarget;

			Vector3 displacement = launchData.initialVelocity * simulationTime + PathControler * gravity * simulationTime * simulationTime / 2f;

			Vector3 drawPoint = ball.position + displacement;
			_ball[i].transform.position = drawPoint;
			Debug.DrawLine (previousDrawPoint, drawPoint, Color.green);
			previousDrawPoint = drawPoint;
		}
	}

	struct LaunchData 
	{
		public readonly Vector3 initialVelocity;
		public readonly float timeToTarget;

		public LaunchData (Vector3 initialVelocity, float timeToTarget)
		{
			this.initialVelocity = initialVelocity;
			this.timeToTarget = timeToTarget;
		}
		
	}
}
	