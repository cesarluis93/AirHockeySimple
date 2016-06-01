using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody> ().transform.Rotate(-90.0f, 0.0f, 0.0f);

		if (isLocalPlayer) {
			GetComponent<ControladorJugador> ().enabled = true;
			GetComponent<Rigidbody> ().position = new Vector3 (0.0f, 0.0f, 11.0f);
			Globals.isLocalPlayer = true;
		}

		if (isServer) {
			GetComponent<Rigidbody> ().position = new Vector3 (0.0f, 0.0f, -11.0f);
			Globals.isLocalPlayer = false;
		}
	}
}
