using UnityEngine;
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
		
		if(colision.gameObject.tag == "Player") {
			// si choca con uno de los jugadores
			// puede ser un saque por lo que cambiamos el estado del juego
			juego.estado = ControladorJuego.Estados.EnJuego;
			
			// calculamos el nuevo movimiento en funcion del angulo de choque
			Vector2 posDisco = new Vector2(GetComponent<Rigidbody>().position.x, GetComponent<Rigidbody>().position.z);
			Vector2 posJugador = new Vector2(objeto.GetComponent<Rigidbody>().position.x, objeto.GetComponent<Rigidbody>().position.z);
			Vector2 direccionGolpe = posDisco - posJugador;
			
			// calculamos el movimiento del disco
			movimiento.z = -movimiento.z;
			movimiento += new Vector3(direccionGolpe.x, 0.0f, direccionGolpe.y); 
			movimiento = new Vector3(Mathf.Clamp(movimiento.x, -1.25f, 1.25f), 0.0f, Mathf.Clamp(movimiento.z, -1.25f, 1.25f));
		}
		else if(colision.gameObject.tag == "Lateral") {
			// si choca con un lateral invertimos el eje x del movimiento
			movimiento.x = -movimiento.x;
		}
		else if(colision.gameObject.tag == "Frontal") {
			// si choca con un frontal invertimos el eje z del movimiento
			movimiento.z = -movimiento.z;
		}
		else if(colision.gameObject.tag == "PorteriaJugador") {
			// si choca con la porteria del jugador
			movimiento = new Vector3(0.0f, 0.0f, 0.0f);
			juego.golesIA++;
			juego.estado = ControladorJuego.Estados.GolIA;
		}
		else if(colision.gameObject.tag == "PorteriaIA") {
			// si choca con la porteria del jugador
			movimiento = new Vector3(0.0f, 0.0f, 0.0f);
			juego.golesJugador++;
			juego.estado = ControladorJuego.Estados.GolJugador;
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
