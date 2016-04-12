using UnityEngine;
using System.Collections;

public class GameManager 
{
	static GameManager _instance;
	static bool _isInit = false;
	static public GameManager instance()
	{
		if(_instance == null)
		{
			_instance = new GameManager();
		}

		return _instance;
	}

	// all manager here
	public InputManager inputManager = null;
	public UIManager 	uiManager = null;
	public LuaManager 	luaManager = null;

	public NetworkManager networkManager = null;

	public void InitAllManager()
	{
		if(_isInit)
		{
			return;
		}

		GameObject gameObj = new GameObject("gameManager");
		gameObj.AddComponent<UpdateManager>();
		GameObject.DontDestroyOnLoad(gameObj);

		// input
		GameObject inputObj = new GameObject("inputManager");
		inputManager = inputObj.AddComponent<InputManager>();
		inputManager.Init();
		GameObject.DontDestroyOnLoad(inputObj);
		inputObj.transform.SetParent(gameObj.transform);

		// ui
		GameObject uiObj = new GameObject("uiManager");
		uiManager = uiObj.AddComponent<UIManager>();
		uiManager.Init();
		GameObject.DontDestroyOnLoad(uiObj);
		uiObj.transform.SetParent(gameObj.transform);

		// lua
		GameObject luaObj = new GameObject("luaManager");
		luaManager = luaObj.AddComponent<LuaManager>();
		luaManager.Init();
		GameObject.DontDestroyOnLoad(luaObj);
		luaObj.transform.SetParent(gameObj.transform);

		// network
		networkManager = new NetworkManager();
		networkManager.Init();

		_isInit = true;
	}
}
