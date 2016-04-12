using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Text;

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
		m_EventSystem.AddComponent<TouchInputModule>();

		GameObject button = new GameObject("send button");
		Button btnCom = button.AddComponent<Button>();
		button.AddComponent<Image>();

		button.transform.SetParent(m_Canvas.gameObject.transform);
		button.transform.localPosition = Vector3.zero;

		int count = 0;

		btnCom.onClick.AddListener(delegate() {

			count++;

			GameManager.instance().networkManager.connect();
			GameManager.instance().networkManager.send(Encoding.UTF8.GetBytes("hello world" + count.ToString()));

		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
