using UnityEngine;
using System.Collections;

public class MuralScript : MonoBehaviour {

	private bool zoomed = false;

	public Transform target;
	public float smooth = 5.0f;
	private Vector3 velocity = new Vector3(30.0f, 30.0f, 30.0f);
	private bool showGUI = false, volver = false;

	private float zoom_x = 8.1f, zoom_y = 13.7f, zoom_z = 35.2f;
	private float def_x = 35.2f, def_y = 20.0f, def_z = 29.4f;

	private bool postitcreated = false;

	private TouchScreenKeyboard keyboard;

	public float z_post = -2.0f;

	// Use this for initialization
	void Start () {
		target = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (zoomed) {
			showGUI = true;
			Vector3 targetPosition = new Vector3(zoom_x, zoom_y, zoom_z);
			target.position = Vector3.Slerp(target.position, targetPosition, Time.deltaTime * smooth);
		}
		if(volver) {
			showGUI = false;
			Vector3 targetPosition = new Vector3(def_x, def_y, def_z);
			target.position = Vector3.Slerp(target.position, targetPosition, Time.deltaTime * smooth);
			if (Mathf.Abs (target.position.x - def_x) < 0.5) {
				volver = false;
			}
		}
	}

	void OnGUI() {
		if (showGUI) {
			if (GUI.Button (new Rect (10, 10, 100, 100), "Volver")) {
				showGUI = false;
				volver = true;
				zoomed = false;
			}
		}
		if(keyboard != null && keyboard.done) {
			GUI.Label (new Rect(50, 50, 100, 20),  keyboard.text);
		}
	}

	void OnMouseDown() {
		if (!zoomed) {
			volver = false;
			zoomed = true;
		}
		if (zoomed && !postitcreated && Mathf.Abs (target.position.x - zoom_x) < 0.5) {
			//postitcreated = false;
			Quaternion rot = Quaternion.Euler (0.0f, 0.0f, -90.0f);
			GameObject postit = GameObject.Find ("Post");
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				Vector3 posit_pos = new Vector3 (hit.point.x + 0.1f, hit.point.y, hit.point.z);
				Instantiate (postit, posit_pos, rot);
			}
		}
	}
}
