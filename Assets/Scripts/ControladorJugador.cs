using UnityEngine;
using System.Collections;

public class ControladorJugador : MonoBehaviour 
{
	// referencia al controlador del juego
	public ControladorJuego juego;

	// movimiento del jugador
	private Vector3 movimiento;
	
	// metodo que se llama en cada ciclo fisico
	void FixedUpdate() {
		// memorizamos el movimiento horizontal siempre
		float movH = Input.GetAxis("Horizontal");
		
		// en funcion del estado del juego
		switch(juego.estado) {
			case ControladorJuego.Estados.SacaJugador:
				// si saca el jugador lo movemos y luego vemos si pulsa up
				// en cuyo caso pasamos a sacando jugador
				Mover (movH);
				if(Input.GetAxis("Vertical") != 0) {
					juego.estado = ControladorJuego.Estados.SacandoJugador;
				}
				break;
				
			case ControladorJuego.Estados.SacandoJugador:
				// calculamos la direccion relativa del disco
				Vector2 posDisco = new Vector2(
					GetComponent<Rigidbody>().position.x,
					GetComponent<Rigidbody>().position.z
				);
				Vector2 posJugador = new Vector2(
					juego.disco.GetComponent<Rigidbody>().position.x,
					juego.disco.GetComponent<Rigidbody>().position.z
				);
				Vector2 direccionGolpe = posJugador - posDisco;
				
				// movemos el jugador
				movimiento = new Vector3(direccionGolpe.x, 0.0f, direccionGolpe.y);
				GetComponent<Rigidbody>().position += movimiento * juego.reaccionJugador;
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
		GetComponent<Rigidbody>().position += movimiento * juego.reaccionJugador;
		GetComponent<Rigidbody>().position = new Vector3(
			Mathf.Clamp(GetComponent<Rigidbody>().position.x, -5.5f, 5.5f),
			0.0f,
			-11.0f
		);
		
	}

}
