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
#if UNITY_EDITOR
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
#endif

#if (UNITY_ANDROID || (UNITY_IOS || UNITY_IPHONE))
		//Debug.Log("touch count " + Input.touchCount.ToString());

		if (Input.touchCount > 0) {

			Vector2 touchPosition = Input.GetTouch(0).position;

			if(Input.GetTouch(0).phase == TouchPhase.Began)
			{
				LuaFunction func = GameManager.instance().luaManager.l.luaState.getFunction("HandleTouchDown");
				if(func != null)
				{
					func.call(touchPosition.x, touchPosition.y);	
				}
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				LuaFunction func = GameManager.instance().luaManager.l.luaState.getFunction("HandleTouchMove");
				if(func != null)
				{
					func.call(touchPosition.x, touchPosition.y);	
				}
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				LuaFunction func = GameManager.instance().luaManager.l.luaState.getFunction("HandleTouchUp");
				if(func != null)
				{
					func.call(touchPosition.x, touchPosition.y);	
				}
			}
		}
#endif

	}
}
