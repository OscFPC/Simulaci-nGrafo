using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class Moverse : MonoBehaviour
{

    List<int> camino;
    public int origen;
    public int destino;
    // Start is called before the first frame update
    void Start()
    {

        MainConFloydWarshall floydWarshall = new MainConFloydWarshall();
        floydWarshall.generarMatrizDistancias("C:/Users/JArellano/Desktop/AYR_2024_1/Unidad3/SimulaciónGrafo/Assets/Code/Scripts/Caminos.csv");

        camino = floydWarshall.Main(origen, destino);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < camino.Count; i++)
        {
            Debug.Log(camino[i]);
        }
    }
}