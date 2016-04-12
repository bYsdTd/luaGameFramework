using UnityEngine;
using System.Collections;

public class UpdateManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// update all , need to update
		// in the sequence sefl defined
		GameManager.instance().networkManager.Update(Time.deltaTime);
	}
}
