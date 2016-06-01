using UnityEngine;
using System.Collections;

public class ControladorJugador : MonoBehaviour 
{
	// movimiento del jugador
	private Vector3 movimiento;

	private float zPosition = 0.0f;

	void Start() {
		zPosition = (Globals.isLocalPlayer) ? -11.0f : 11.0f;
	}

	// metodo que se llama en cada ciclo fisico
	void FixedUpdate() {
		float movH = 0;
		Touch touch;
		if (Input.touchCount == 1) {
			touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Moved) {
				movH = touch.deltaPosition.x;
			}
		}
		
		// en funcion del estado del juego
		switch(ControladorJuego.estado) {
			case ControladorJuego.Estados.SacaJugador1:
				Mover (movH);
				if (Input.touchCount == 1) {
					touch = Input.GetTouch(0);
					if (Mathf.Abs(touch.deltaPosition.y) > 5.0f) {
						ControladorJuego.estado = ControladorJuego.Estados.SacandoJugador1;
					}
				}
				break;
				
			case ControladorJuego.Estados.SacandoJugador1:
				// calculamos la direccion relativa del disco
				Vector2 posDisco = new Vector2(
					GetComponent<Rigidbody>().position.x,
					GetComponent<Rigidbody>().position.z
				);
				Vector2 posJugador = new Vector2(
					ControladorDisco.position.x,
					ControladorDisco.position.z
				);
				Vector2 direccionGolpe = posJugador - posDisco;
				
				// movemos el jugador
				movimiento = new Vector3(direccionGolpe.x, 0.0f, direccionGolpe.y);
				GetComponent<Rigidbody>().position += movimiento * Globals.reaccionJugador * 4;
				break;

			case ControladorJuego.Estados.SacaJugador2:
				Mover (movH);
				if (Input.touchCount == 1) {
					touch = Input.GetTouch(0);
					if (Mathf.Abs(touch.deltaPosition.y) > 5.0f) {
						ControladorJuego.estado = ControladorJuego.Estados.SacandoJugador2;
					}
				}
				break;

			case ControladorJuego.Estados.SacandoJugador2:
				// calculamos la direccion relativa del disco
				posDisco = new Vector2(
					GetComponent<Rigidbody>().position.x,
					GetComponent<Rigidbody>().position.z
				);
				posJugador = new Vector2(
					ControladorDisco.position.x,
					ControladorDisco.position.z
				);
				direccionGolpe = posJugador - posDisco;

				// movemos el jugador
				movimiento = new Vector3(direccionGolpe.x, 0.0f, direccionGolpe.y);
				GetComponent<Rigidbody>().position += movimiento * Globals.reaccionJugador * 4;
				break;

			case ControladorJuego.Estados.EnJuego:
				// movemos el jugador
				Mover(movH);
				break;
		}
	}
	
	// este metodo mueve el jugador horizontalmente
	void Mover(float movH) {
		// el movimiento esta restringido a los limites del campo de juego
		movimiento = new Vector3(movH, 0.0f, 0.0f);
		GetComponent<Rigidbody>().position += movimiento * Globals.reaccionJugador;
		GetComponent<Rigidbody>().position = new Vector3(
			Mathf.Clamp(GetComponent<Rigidbody>().position.x, -5.5f, 5.5f),
			0.0f,
			zPosition
		);
		
	}

}
