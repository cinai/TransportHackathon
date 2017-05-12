using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ParadasScript : MonoBehaviour {

	bool zooming = false;
	const float zoomTime = 0.5f;// Zoom time in seconds.
	float zoomTimer = 0f;		// Timer for zooming.

	public Image blackOverlay;	// Overlay for zooming.
	Color blackNoAlpha;			// Initial color for lerp.

	void Start() {
		blackNoAlpha = new Color(0f, 0f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		if(zooming) {
			zoomTimer += Time.deltaTime;
			blackOverlay.color = Color.Lerp(blackNoAlpha, Color.black, zoomTimer / zoomTime);
			if(zoomTimer >= zoomTime) {
				SceneManager.LoadScene("Paradero");
			}
		}
	}

	// Called from the bus stop button.
	public void GoToStop() {
		zooming = true;
	}
}
