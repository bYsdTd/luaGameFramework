using UnityEngine;
using System.Collections;

public class Launch : MonoBehaviour {

	// Use this for initialization
	void Start () {

		Debug.Log(Time.realtimeSinceStartup.ToString());

		GameManager.instance().InitAllManager();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
