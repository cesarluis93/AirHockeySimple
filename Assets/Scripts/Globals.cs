using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour {
	public static float reaccionJugador = 0.05f; // factor que indica la capacidad de reaccion del jugador
	public static float reaccionIA = 0.1f; // factor que indica la capacidad de reaccion de la IA
	public static float tiempoEspera = 2.0f; // tiempo antes de que la IA efectue un saque
	public static float reaccionDisco = 0.3f; // factor que indica la capacidad de reaccion del disco

	public static bool isLocalPlayer = true;
}
