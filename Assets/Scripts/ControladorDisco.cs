﻿using UnityEngine;
using System.Collections;

public class ControladorDisco : MonoBehaviour 
{
	// referencia al controlador del juego
	public ControladorJuego juego;

	// campos privados
	private Vector3 movimiento;

	// este metodo se activa cada vez que el disco entra en un collider que es trigger
	void OnTriggerEnter(Collider colision) {
		// memorizamos el objeto con el que choco el disco
		GameObject objeto = colision.gameObject;
		
		if (colision.gameObject.tag == "Player") {
			// si choca con uno de los jugadores
			// puede ser un saque por lo que cambiamos el estado del juego
			ControladorJuego.estado = ControladorJuego.Estados.EnJuego;
			
			// calculamos el nuevo movimiento en funcion del angulo de choque
			Vector2 posDisco = new Vector2 (GetComponent<Rigidbody> ().position.x, GetComponent<Rigidbody> ().position.z);
			Vector2 posJugador = new Vector2 (objeto.GetComponent<Rigidbody> ().position.x, objeto.GetComponent<Rigidbody> ().position.z);
			Vector2 direccionGolpe = posDisco - posJugador;
			
			// calculamos el movimiento del disco
			movimiento.z = -movimiento.z;
			movimiento += new Vector3 (direccionGolpe.x, 0.0f, direccionGolpe.y); 
			movimiento = new Vector3 (Mathf.Clamp (movimiento.x, -1.25f, 1.25f), 0.0f, Mathf.Clamp (movimiento.z, -1.25f, 1.25f));

			// incrementamos la capacidad de reaccion del disco hasta un maximo de 0.5f
			if (juego.reaccionDisco < 0.5) {
				juego.reaccionDisco += 0.05f;
			}
		}
		else if(colision.gameObject.tag == "Lateral") {
			// si choca con un lateral invertimos el eje x del movimiento
			movimiento.x = -movimiento.x;
		}
		else if(colision.gameObject.tag == "Frontal") {
			// si choca con un frontal invertimos el eje z del movimiento
			movimiento.z = -movimiento.z;
		}
		else if(colision.gameObject.tag == "PorteriaJugador1") {
			// si choca con la porteria del jugador1
			movimiento = new Vector3(0.0f, 0.0f, 0.0f);
			juego.golesJugador1++;
			juego.reaccionDisco = Globals.reaccionDisco;
			ControladorJuego.estado = ControladorJuego.Estados.GolJugador1;
		}
		else if(colision.gameObject.tag == "PorteriaJugador2") {
			// si choca con la porteria del jugador
			movimiento = new Vector3(0.0f, 0.0f, 0.0f);
			juego.golesJugador2++;
			juego.reaccionDisco = Globals.reaccionDisco;
			ControladorJuego.estado = ControladorJuego.Estados.GolJugador2;
		}

		// emitimos el sonido de choque
		GetComponent<AudioSource>().Play();
	}
	
	void Update() {
		// movemos el disco restringiendo su posicion
		GetComponent<Rigidbody>().position += movimiento * juego.reaccionDisco;
		GetComponent<Rigidbody>().position = new Vector3(
			Mathf.Clamp(GetComponent<Rigidbody>().position.x, -6.05f, 6.05f),
			0.0f,
			Mathf.Clamp(GetComponent<Rigidbody>().position.z, -12.1f, 12.1f)
		);
	}
}
