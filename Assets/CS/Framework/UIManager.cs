using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UIManager : MonoBehaviour {

	GameObject m_Canvas;
	GameObject m_EventSystem;

	// Use this for initialization
	public void Init() {

		m_Canvas = new GameObject("Canvas");
		m_EventSystem = new GameObject("EventSystem");

		GameObject.DontDestroyOnLoad(m_Canvas);
		GameObject.DontDestroyOnLoad(m_EventSystem);
		m_Canvas.transform.SetParent(gameObject.transform);
		m_EventSystem.transform.SetParent(gameObject.transform);

		Canvas canvasCom = m_Canvas.AddComponent<Canvas>();
		canvasCom.renderMode = RenderMode.ScreenSpaceOverlay;

		m_Canvas.AddComponent<CanvasScaler>();
		m_Canvas.AddComponent<GraphicRaycaster>();

		m_EventSystem.AddComponent<EventSystem>();
		m_EventSystem.AddComponent<StandaloneInputModule>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
