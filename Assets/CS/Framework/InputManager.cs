using UnityEngine;
using System.Collections;
using SLua;

public class InputManager : MonoBehaviour {

	public void Init ()
	{

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach(Touch touch in Input.touches)
		{
			Debug.Log(touch.fingerId);
			Debug.Log(touch.phase);
			Debug.Log(touch.deltaPosition);
		}

		if(Input.GetMouseButtonDown(0))
		{
			LuaFunction func = GameManager.instance().luaManager.l.luaState.getFunction("HandleTouchDown");

			if(func != null)
			{
				func.call(Input.mousePosition.x, Input.mousePosition.y);	
			}

		}

		if(Input.GetMouseButton(0))
		{
			LuaFunction func = GameManager.instance().luaManager.l.luaState.getFunction("HandleTouchMove");
			if(func != null)
			{
				func.call(Input.mousePosition.x, Input.mousePosition.y);	
			}
		}

		if(Input.GetMouseButtonUp(0))
		{
			LuaFunction func = GameManager.instance().luaManager.l.luaState.getFunction("HandleTouchUp");
			if(func != null)
			{
				func.call(Input.mousePosition.x, Input.mousePosition.y);	
			}
		}

	}
}
