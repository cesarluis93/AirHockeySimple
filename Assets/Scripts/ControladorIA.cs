using UnityEngine;
using System.Collections;

public class ControladorIA : MonoBehaviour 
{
	// referencia al controlador del juego
	
	public ControladorJuego juego;

	// campos privados
	
	private Vector3 movimiento;
	private Vector2 posIA;
	private Vector2 posDisco;
	private Vector2 direccionGolpe;
	
	// metodo que se llama en cada ciclo fisico
	
	void FixedUpdate()
	{
		switch(juego.estado)
		{
		case ControladorJuego.Estados.SacaIA:
			
			// si la IA recibio un gol incrementamos su capacidad de reaccion
			
			juego.reaccionIA += 0.15f;
			
			// colocamos la IA en una posicion aleatoria y luego cambiamos de estado
			
			GetComponent<Rigidbody>().position = new Vector3(Random.Range(-5.5f, 5.5f), 0, 11);
			juego.estado = ControladorJuego.Estados.SacandoIA;
			break;
			
		case ControladorJuego.Estados.SacandoIA:
			
			//movemos la IA
			Mover ();
			break;
			
		case ControladorJuego.Estados.EnJuego:
			
			// para limitar la IA solo permitimos que se mueva cuando este en su campo
			
			if(juego.disco.GetComponent<Rigidbody>().position.z > 0)
			{
				
				// movemos la IA pero esta vez restringimos sus limites
				
				Mover ();
				GetComponent<Rigidbody>().position = new Vector3(Mathf.Clamp(GetComponent<Rigidbody>().position.x, -5.5f, 5.5f), 0.0f, 11.0f);
			}
			break;
		}
	}
	
	void Mover()
	{
		// calculamos la direccion relativa del disco
		
		posIA = new Vector2(GetComponent<Rigidbody>().position.x, GetComponent<Rigidbody>().position.z);
		posDisco = new Vector2(juego.disco.GetComponent<Rigidbody>().position.x, juego.disco.GetComponent<Rigidbody>().position.z);
		direccionGolpe = posDisco - posIA;
		
		// movemos la IA
		
		movimiento = new Vector3(direccionGolpe.x, 0.0f, direccionGolpe.y);
		GetComponent<Rigidbody>().position += movimiento * juego.reaccionIA;
		
	}
}
