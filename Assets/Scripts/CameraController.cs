using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CameraController : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		if (isServer) {
			this.transform.Rotate(30.0f, 0.0f, 0.0f);
			this.transform.position = new Vector3 (0.0f, 15.0f, -25.0f);
		}
	}
}
