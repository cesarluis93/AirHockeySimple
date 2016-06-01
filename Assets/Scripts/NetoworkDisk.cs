using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetoworkDisk : NetworkBehaviour {

	[SyncVar]
	public Vector3 position;

	[SyncVar]
	public Vector3 movimiento;


	// Use this for initialization
	void Start () {
		position = GetComponent<Rigidbody> ().position = new Vector3 (0.0f, 0.0f, -6.0f);
		movimiento = new Vector3(0.0f, 0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		switch(ControladorJuego.estado) {
		case ControladorJuego.Estados.SacaJugador1:
			GetComponent<Rigidbody> ().position = new Vector3 (0.0f, 0.0f, -6.0f);
			CmdChangePosition (GetComponent<Rigidbody> ().position);
			break;

		case ControladorJuego.Estados.SacaJugador2:
			GetComponent<Rigidbody> ().position = new Vector3 (0.0f, 0.0f, 6.0f);
			CmdChangePosition (GetComponent<Rigidbody> ().position);
			break;

		case ControladorJuego.Estados.Final:
			GetComponent<Rigidbody>().position = new Vector3(0.0f, 0.0f, 0.0f);
			CmdChangePosition (GetComponent<Rigidbody> ().position);
			GetComponent<AudioSource>().Stop();
			break;
		}
	}


	[Command]
	public void CmdChangePosition(Vector3 newPosition) {
		position = newPosition;
	}
}
