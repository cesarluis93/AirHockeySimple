using UnityEngine;
using System.Collections;

public class ControladorJuego : MonoBehaviour 
{
	// enumeracion que define los estados por los que pasa el juego
	
	public enum Estados {
		Inicial,
		SacaJugador1,
		SacandoJugador1,
		SacaJugador2,
		SacandoJugador2,
		EnJuego,
		GolJugador1,
		GolJugador2,
		Final
	}


	public static GameObject disco; // el disco

	public float reaccionDisco;
	
	// campos con propiedades del juego	
	public GUIText txtJugador; // marcador de goles del jugador
	public int golesJugador1;
	public GUIText txtIA; // marcador de goles de la ia
	public int golesJugador2;
	public GUIText txtDiscos; // marcador de ordinal de disco en juego
	private int discoEnJuego;
	public GUIText txtIp;
	public static Estados estado; // estado del juego

	// metodo que se ejecuta al principio del juego
	
	public void Start()
	{
		// Formatos de marcadores
		txtJugador.fontSize = 40;
		txtIA.fontSize = 40;
		txtDiscos.fontSize = 40;
		txtIp.fontSize = 40;

		// establecemos estado
		estado = Estados.Inicial;

		// iniciamos el disco
		disco = GameObject.Find("Puck");
		disco.transform.position = new Vector3(0.0f, 0.0f, -6.0f);
		reaccionDisco = Globals.reaccionDisco;
		
		// iniciamos marcadores		
		discoEnJuego = 1;
		golesJugador1 = golesJugador2 = 0;
		
		// cambio de estado
		estado = Estados.SacaJugador1;
		txtIp.text = "IP: " + getMyIp();
	}
	// este metodo se ejecuta en cada fotograma
	
	void Update()
	{
		// actualizamos marcadores
		txtJugador.text = "Goles Jugador1: " + golesJugador1.ToString();
		txtIA.text = "Goles Jugador2: " + golesJugador2.ToString();
		txtDiscos.text = "Disco en juego: " + discoEnJuego.ToString();
		
		// en funcion del estado del juego
		switch(estado) {
			case Estados.GolJugador1:
			case Estados.GolJugador2:
				// se produjo un gol
				discoEnJuego++;
				
				// recolocar jugadores
				// jugador.GetComponent<Rigidbody>().position = new Vector3(0.0f, 0.0f, -11.0f);
				// ia.GetComponent<Rigidbody>().position = new Vector3(0.0f, 0.0f, 11.0f);
				
				if(golesJugador1 == 4 || golesJugador2 == 4) {
					// si alguno tiene 4 goles se finaliza la partida
					estado = Estados.Final;
					// colocar disco en medio
					disco.GetComponent<Rigidbody>().position = new Vector3(0.0f, 0.0f, 0.0f);
				}
				else if(estado == Estados.GolJugador1) {
					// colocar disco cerca de jugador2
					disco.GetComponent<Rigidbody>().position = new Vector3(0.0f, 0.0f, 6.0f);
					estado = Estados.SacaJugador2;
				}
				else {
					// colocar disco cerca de jugador1
					disco.GetComponent<Rigidbody>().position = new Vector3(0.0f, 0.0f, -6.0f);
					estado = Estados.SacaJugador1;
				}
				break;
			
		case Estados.Final:
			// fin del juego
			disco.GetComponent<AudioSource>().Stop();
			if(golesJugador1 == 4) {
				txtDiscos.text = "Gano Jugador1";
			}
			else {
				txtDiscos.text = "Gano Jugador2";
			}
			break;
		}
	}

	void OnGUI() {
		if (GUI.Button (
			new Rect(Screen.width - 90, 50, 80, 25),
			"Reiniciar"
		)) {
			Application.LoadLevel ("Principal");
		}
	}

	string getMyIp() {
		Debug.Log ("MI IP: " + Network.player.ipAddress);

		return Network.player.ipAddress;
	}

}
