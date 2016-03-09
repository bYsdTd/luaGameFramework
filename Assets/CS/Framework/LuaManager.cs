using UnityEngine;
using System.Collections;
using SLua;

public class LuaManager : MonoBehaviour {

	public LuaSvr l;

	// Use this for initialization
	public void Init () {

		Debug.Log("luaManager start");

		l = new LuaSvr();
		l.init(tick,complete,LuaSvrFlag.LSF_DEBUG);

	}

	void complete()
	{
		Debug.Log("compolete  start main");

		l.start("main");
	}

	void tick(int p)
	{
		int progress = p;

//		Debug.Log(progress);
	}

	// Update is called once per frame
	void Update () 
	{

	}

	void FixedUpdate()
	{

	}

	void LateUpdate()
	{

	}
}
