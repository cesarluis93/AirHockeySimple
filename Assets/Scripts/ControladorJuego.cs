using UnityEngine;
using System.Collections;

public class ControladorJuego : MonoBehaviour 
{
	// enumeracion que define los estados por los que pasa el juego
	
	public enum Estados
	{
		Inicial,
		SacaJugador,
		SacandoJugador,
		SacaIA,
		SacandoIA,
		EnJuego,
		GolJugador,
		GolIA,
		EnEspera,
		Final
	}

	public GameObject jugador; // el jugador
	public GameObject ia; // la inteligencia artificial
	public GameObject disco; // el disco

	public float reaccionIA;
	public float reaccionDisco;
	public float tiempoEspera;
	
	// campos con propiedades del juego	
	public GUIText txtJugador; // marcador de goles del jugador
	public int golesJugador;
	public GUIText txtIA; // marcador de goles de la ia
	public int golesIA;
	public GUIText txtDiscos; // marcador de ordinal de disco en juego
	private int discoEnJuego;
	public GUIText txtIp;
	public Estados estado; // estado del juego

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
		
		// iniciamos al jugador
		jugador = GameObject.Find ("Mallet1");
		jugador.transform.position = new Vector3(0.0f, 0.0f, -11.0f);

		// iniciamos la IA
		ia = GameObject.Find ("Mallet2");
		ia.transform.position = new Vector3(0.0f, 0.0f, 11.0f);
		reaccionIA = Globals.reaccionIA;
		tiempoEspera = Globals.tiempoEspera;

		// iniciamos el disco
		disco = GameObject.Find("Puck");
		disco.transform.position = new Vector3(0.0f, 0.0f, -6.0f);
		reaccionDisco = Globals.reaccionDisco;
		
		// iniciamos marcadores		
		discoEnJuego = 1;
		golesJugador = 0;
		golesIA = 0;
		
		// cambio de estado
		estado = Estados.SacaJugador;
		txtIp.text = "IP: " + getMyIp();
	}
	// este metodo se ejecuta en cada fotograma
	
	void Update()
	{
		// actualizamos marcadores
		txtJugador.text = "Goles Jugador: " + golesJugador.ToString();
		txtIA.text = "Goles IA: " + golesIA.ToString();
		txtDiscos.text = "Disco en juego: " + discoEnJuego.ToString();
		
		// en funcion del estado del juego
		switch(estado) {
			case Estados.GolIA:
			case Estados.GolJugador:
				// se produjo un gol
				discoEnJuego++;
				
				// recolocar jugadores
				// jugador.GetComponent<Rigidbody>().position = new Vector3(0.0f, 0.0f, -11.0f);
				// ia.GetComponent<Rigidbody>().position = new Vector3(0.0f, 0.0f, 11.0f);
				
				if(golesIA == 4 || golesJugador == 4) {
					// si alguno tiene 4 goles se finaliza la partida
					estado = Estados.Final;
					// colocar disco en medio
					disco.GetComponent<Rigidbody>().position = new Vector3(0.0f, 0.0f, 0.0f);
				}
				else if(estado == Estados.GolJugador) {
					// colocar disco cerca de ia
					disco.GetComponent<Rigidbody>().position = new Vector3(0.0f, 0.0f, 6.0f);
					
					// cambio de estado
					estado = Estados.EnEspera;
					tiempoEspera = Globals.tiempoEspera;
				}
				else {
					// si marco la IA
					// colocar disco cerca de jugador
					disco.GetComponent<Rigidbody>().position = new Vector3(0.0f, 0.0f, -6.0f);
					
					// cambio de estado
					estado = Estados.SacaJugador;
				}
				break;

		case Estados.EnEspera:
			// esperando a que saque la IA
			tiempoEspera -= Time.deltaTime;
			if(tiempoEspera < 0) {
				// al finalizar de la cuenta se cambia de estado
				estado = Estados.SacaIA;
			}
			break;
			
		case Estados.Final:
			// fin del juego
			disco.GetComponent<AudioSource>().Stop();
			if(golesJugador == 4) {
				txtDiscos.text = "Gano Jugador";
			}
			else {
				txtDiscos.text = "Gano IA";
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
