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
	
	// campos con propiedades del juego	
	public GUIText txtJugador; // marcador de goles del jugador
	public static int golesJugador1;
	public GUIText txtIA; // marcador de goles de la ia
	public static int golesJugador2;
	public GUIText txtDiscos; // marcador de ordinal de disco en juego
	private int discoEnJuego;
	public GUIText txtIp;
	public static Estados estado; // estado del juego

	
	public void Start()
	{
		// establecemos estado
		estado = Estados.Inicial;
		
		// iniciamos marcadores		
		discoEnJuego = 1;
		golesJugador1 = golesJugador2 = 0;
		txtIp.text = "IP: " + getMyIp();

		// cambio de estado
		estado = Estados.SacaJugador1;
	}
	
	void Update()
	{
		// actualizamos marcadores
		txtJugador.text = "Player1: " + golesJugador1.ToString();
		txtIA.text = "Player2: " + golesJugador2.ToString();
		txtDiscos.text = "Disc: " + discoEnJuego.ToString();
		
		// en funcion del estado del juego
		switch(estado) {
			case Estados.GolJugador1:
			case Estados.GolJugador2:
				// se produjo un gol
				discoEnJuego++;
				
				// si alguno tiene 4 goles se finaliza la partida
				if(golesJugador1 == 4 || golesJugador2 == 4) {
					estado = Estados.Final;
				}
				else if(estado == Estados.GolJugador1) {
					estado = Estados.SacaJugador2;
				}
				else if(estado == Estados.GolJugador2) {
					estado = Estados.SacaJugador1;
				}
				break;
			
		case Estados.Final:
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
		if (GUI.Button (new Rect(Screen.width - 90, 50, 80, 30), "Reiniciar")) {
			Application.LoadLevel ("Principal");
		}
	}

	string getMyIp() {
		Debug.Log ("MI IP: " + Network.player.ipAddress);

		return Network.player.ipAddress;
	}

}
