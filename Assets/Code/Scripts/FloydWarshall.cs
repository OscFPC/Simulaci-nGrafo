using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;

public class MainConFloydWarshall
{
    static int[,] graph;
    static int[,] M_distancias;
    static int[,] T_trayectorias;
    static int n;


    // public int[,] getM_distancias()
    // {
    //     return M_distancias;
    // }

    public void generarMatrizDistancias(string path)
    {
        List<string[]> matrizOrig = new List<string[]>();

        //"C:/Users/JArellano/Desktop/AYR_2024_1/Unidad3/SimulaciónGrafo/Assets/Code/Scripts/Caminos.csv"
        using (var reader = new StreamReader(path))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                matrizOrig.Add(values);
                //Debug.Log(values[0]);
            }
        }

        //CREA LA MATRIZ COPIA Y ESTABLECE VALORES PARA ITERAR

        int[,] matrizCop = new int[matrizOrig.Count, matrizOrig[0].Length];
        int Nodos = int.Parse(matrizOrig[0][0]);
        int Columnas = matrizOrig[0].Length;
        int Filas = matrizOrig.Count;

        // SUSTITUYE LAS LETRAS POR NUMEROS, EJEMPLO NODO A = NODO 0... NODO L = NODO 11

        for (int i = 1; i < Filas; i++)
        {
            for (int j = 0; j < Columnas; j++)
            {
                switch (matrizOrig[i][j])
                {
                    case "A":
                        matrizCop[i, j] = 0;
                        break;
                    case "B":
                        matrizCop[i, j] = 1;
                        break;
                    case "C":
                        matrizCop[i, j] = 2;
                        break;
                    case "D":
                        matrizCop[i, j] = 3;
                        break;
                    case "E":
                        matrizCop[i, j] = 4;
                        break;
                    case "F":
                        matrizCop[i, j] = 5;
                        break;
                    case "G":
                        matrizCop[i, j] = 6;
                        break;
                    case "H":
                        matrizCop[i, j] = 7;
                        break;
                    case "I":
                        matrizCop[i, j] = 8;
                        break;
                    case "J":
                        matrizCop[i, j] = 9;
                        break;
                    case "K":
                        matrizCop[i, j] = 10;
                        break;
                    case "L":
                        matrizCop[i, j] = 11;
                        break;
                    default:
                        matrizCop[i, j] = int.Parse(matrizOrig[i][j]);
                        break;
                }
            }
        }

        graph = new int[Nodos, Nodos];

        // LLENA MATRIZ matrizTmp CON EL VALOR 9999 EN LAS CELDAS DONDE 
        // COINCIDEN EL MISMO NODO
        /*
            0     1     2     3     4     5     6 7 8 9
        0  9999

        1       9999

        2             9999

        3                   9999

        4                         9999

        5
        */

        for (int l = 0; l < Nodos; l++)
        {
            graph[l, l] = 9999;
        }

        // TOMA LOS DOS VALORES (NODOS) DE LA FILA CORRESPONDIENTE A LA ITERACIÓN
        // COLOCA EL PESO CORRESPONDIENTE EN LA CELDA CORRESPONDIENTE EN matrizTmp
        for (int i = 1; i < Filas; i++)
        {
            int val1 = matrizCop[i, 0];
            int val2 = matrizCop[i, 1];
            graph[val1, val2] = int.Parse(matrizOrig[i][2]);
            // if (graph[val1, val2] > 0)
            // {
            //     matrizAdy[val1, val2] = 1;
            // }
        }
    }

    static void floydWarshall()
    {
        for (int k = 0; k < n; k++) //nodos intermedios
        {
            for (int i = 0; i < n; i++) //origen
            {
                for (int j = 0; j < n; j++) //destino
                {
                    if (M_distancias[i, k] == 9999 || M_distancias[k, j] == 9999)
                    {
                        continue; //continua si no existe un enlace entre origen y destino
                    }

                    //si k es un nodo intermedio que mejora la distancia entre i y j
                    if (M_distancias[i, k] + M_distancias[k, j] < M_distancias[i, j])
                    {
                        M_distancias[i, j] = M_distancias[i, k] + M_distancias[k, j];
                        T_trayectorias[i, j] = T_trayectorias[i, k];
                    }
                }
            }
        }
    }

    static List<int> getPath(int u, int v)
    {
        //si no existe un path regresa null
        if (T_trayectorias[u, v] == -1)
            return null;

        // Inicio del camino
        List<int> path = new List<int>();
        path.Add(u);

        while (u != v)
        {
            u = T_trayectorias[u, v];
            path.Add(u);
        }
        return path;
    }

    public List<int> Main(int origen, int destino)
    {
        // Llenar M_distancias y T_trayectorias con las matrices generadas por el primer código
        // Aquí asumiendo que ya tienes las matrices M_distancias y T_trayectorias generadas por el primer código
        
        // Obtener el tamaño de las matrices
        n = graph.GetLength(0);

        T_trayectorias = new int[n, n];
        M_distancias = new int[n, n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; i < n; i++)
            {
                M_distancias[i, j] = graph[i, j];

                // Si no hay camino entre i y j
                if (graph[i, j] == 9999) T_trayectorias[i, j] = -1;
                else T_trayectorias[i, j] = j;
            }
        }

        Console.WriteLine("Inicia FloydWarshall:");

        // Aplicar el algoritmo de Floyd-Warshall a las matrices generadas
        floydWarshall();

        // Definir el origen y el destino (suponiendo que 'G' y 'R' son los nodos de origen y destino)
        // int origen = 6; // Suponiendo que 'G' es el nodo 6
        // int destino = 17; // Suponiendo que 'R' es el nodo 17

        // Obtener el camino desde el origen hasta el destino
        List<int> camino = getPath(origen, destino);
        List<string> newCamino = convierteToLetras(camino);

        // return camino;

        // Imprimir el camino
        if (camino != null)
        {
            // foreach (int nodo in camino)
            // {
            //     Console.WriteLine(nodo);
            // }
            return camino;
        }
        else
        {
            // Console.WriteLine("No hay camino disponible.");
            return null;
        }
    }

    public List<string> convierteToLetras(List<int> camino)
    {
        List<string> newCamino = new List<string>();

        for (int i = 0; i < camino.Count; i++)
        {
            int temp_i = camino[i] + 65;
            string letraInicio = (char)(temp_i>90? 65:temp_i)+"";
            letraInicio += temp_i<=90? "": (char)(temp_i%91+65);

            newCamino.Add(letraInicio);
        }

        return newCamino;
    }
}
