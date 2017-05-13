using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerate : MonoBehaviour {

	public float[] probabilities = new float[3];
	public List<Vector3> positions;
	public List<Vector3> paths;
	public List<float> spikeBalls;
	public GameObject floor;
	public GameObject wall;
	public GameObject start;
	public GameObject end;
	public GameObject spikeBall;
	public GameObject Hole;

	public float scale;
	public float heightScale;
	public int size;
	public float probability;

	private float dir;


	// Use this for initialization
	void Start () {
		//Start
		positions.Add (new Vector3 (0, 0, 0));
		createFloor ();
		//Camino principal
		int i = 0;
		while (i < size) {
			if (createPath ()) {
				i++;
			}
		}

		//Final


		//Caminos sin sentido
		createNewPaths(); //prueba

		//Rodea el camino con paredes
		createWalls ();

		//Create Trpas 
		createTraps();
	}


	private bool createPath(){
		dir = Random.Range (0f, 1f);
		if (dir < probabilities [0] && !positions.Contains (transform.position + new Vector3 (0f, 0f, 1f * scale))) {
			transform.Translate (0f, 0f, 1f * scale);
			createFloor ();
			return true;
		} else if (dir < probabilities [1] && !positions.Contains (transform.position + new Vector3 (1f * scale, 0f, 0f))) {
			transform.Translate (1f * scale, 0f, 0f);
			createFloor ();
			return true;
		} else if (dir < probabilities [2] && !positions.Contains (transform.position + new Vector3 (-1f * scale, 0f, 0f))) {
			transform.Translate (-1f * scale, 0f, 0f);
			createFloor ();
			return true;
		} else {
			return false;
		}
	}

	private void createFloor(){
		Instantiate (floor, transform.position, Quaternion.identity);
		positions.Add (transform.position);
	}

	private void createTraps(){
		foreach (Vector3 pos in positions) {
			float r = Random.Range (0f,1f);
			if (r < probability) {
				if (r < probability / 2 && !spikeBalls.Contains(pos.z)) {
					createSpike (pos);

				} else if (r > probability/2){
					
				}
			}
		}

	}

	private void createSpike(Vector3 position){
		GameObject spikeprefab = (GameObject)Instantiate (spikeBall, position, Quaternion.identity);
		spikeBalls.Add (position.z);
	}

	private void createHole(){
		
	}

	private void createNewPaths(){
		
		int cont = 0;
		foreach (Vector3 pos in positions) {
			cont = 0;
			//Arriba
			if (!positions.Contains(pos + new Vector3 (0f, 0f, 1f) * scale))
				cont++;
			//Derecha
			if (!positions.Contains(pos + new Vector3 (1f, 0f, 0f) * scale))
				cont++;
			//Abajo
			if (!positions.Contains(pos + new Vector3 (0f, 0f, -1f) * scale))
				cont++;
			//Izquierda
			if (!positions.Contains(pos + new Vector3(-1f,0f,0f)*scale))
				cont++;
			//Si es un comino sin fin guardarlo
			if (cont >= 2){
				cont = 0;
				//Arriba-Derecha
				if (!positions.Contains (pos + new Vector3 (1f, 0f, 1f) * scale))
					cont++;
				//Arriba-Izquierda
				if (!positions.Contains (pos + new Vector3 (-1f, 0f, 1f) * scale))
					cont++;
				//Abajo-Derecha
				if (!positions.Contains (pos + new Vector3 (1f, 0f, -1f) * scale))
					cont++;
				//Arriba-Izquierda
				if (!positions.Contains (pos + new Vector3 (-1f, 0f, -1f) * scale))
					cont++;
				if (cont <= 2) {
					//Arriba-Derecha y Abajo-Derecha
					if (positions.Contains (pos + new Vector3 (1f, 0f, 1f) * scale)
						&& positions.Contains (pos + new Vector3 (1f, 0f, -1f) * scale)) {
						paths.Add (pos);
					}
					//Arriba-Izquierda y Abajo-Izquierda
					if (positions.Contains (pos + new Vector3 (-1f, 0f, 1f) * scale)
						&& positions.Contains (pos + new Vector3 (-1f, 0f, -1f) * scale)) {
						paths.Add (pos);
					}

				}
					
			}
				
		}


		foreach (Vector3 v in paths) {
			transform.position = v;
			createPaths ();
			Debug.Log ("" + v);
		}
	}

	private void createPaths(){
		
	}

	

	private void createWalls(){
		foreach (Vector3 pos in positions) {
			// Arriba
			if (!positions.Contains(pos + new Vector3 (0f,0f,1f)*scale)){
				GameObject prefabWall = (GameObject)Instantiate (wall, pos + new Vector3 (0f, 0f, 1f) * scale, Quaternion.identity);
				prefabWall.transform.Translate (new Vector3 (0f,heightScale/2, -(scale/2 - 0.5f)));
			}
			// Derecha
			if (!positions.Contains(pos + new Vector3 (1f,0f,0f)*scale)){
				GameObject prefabWall = (GameObject)Instantiate (wall, pos + new Vector3 (1f, 0f, 0f) * scale, Quaternion.identity);
				prefabWall.transform.Translate (new Vector3 (-(scale/2 - 0.5f),heightScale/2,0f));
				prefabWall.transform.Rotate (0f, 90f, 0f);
			}
			// Abajo
			if (!positions.Contains(pos + new Vector3 (0f,0f,-1f)*scale)){
				GameObject prefabWall = (GameObject)Instantiate (wall, pos + new Vector3 (0f, 0f, -1f) * scale, Quaternion.identity);
				prefabWall.transform.Translate (new Vector3 (0f,heightScale/2, scale/2 - 0.5f));
				prefabWall.transform.Rotate (0f, -180f, 0f);
			}
			// Izquierda
			if (!positions.Contains(pos + new Vector3 (-1f,0f,0f)*scale)){
				GameObject prefabWall = (GameObject)Instantiate (wall, pos + new Vector3 (-1f, 0f, 0f) * scale, Quaternion.identity);
				prefabWall.transform.Translate (new Vector3 (scale/2 - 0.5f,heightScale/2,0f));
				prefabWall.transform.Rotate (0f, 90f + 180f, 0f);
			}
		}
	}
}
