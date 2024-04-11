import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;

public class MainConFloydWarshall {

    static int graph[][];
    static int M_distancias[][];
    static int [][]T_trayectorias;

    static int n;


    static void floydWarshall() {
        int i, j, k;

        for (k = 0; k < n; k++) { //nodos intermedios
            for (i = 0; i < n; i++) { //origen
                for (j = 0; j < n; j++) { //destino

                    if (M_distancias[i][k] == 9999 || M_distancias[k][j] == 9999) {
                        continue; //continua si no existe un enlace entre origen y destino
                    }

                    //si k es un nodo intermedio que mejora la distancia entre i y j
                    if (M_distancias[i][k] + M_distancias[k][j] < M_distancias[i][j]) {
                        M_distancias[i][j] = M_distancias[i][k] + M_distancias[k][j];
                        T_trayectorias[i][j] = T_trayectorias[i][k];
                    }
                }
            }
        }
    }

    static ArrayList<Integer> getPath(int u, int v)
    {
        //si no existe un path regresa null
        if (T_trayectorias[u][v] == -1)
            return null;

        // Inicio del camino
        ArrayList<Integer> path = new ArrayList();
        path.add(u);

        while (u != v)
        {
            u = T_trayectorias[u][v];
            path.add(u);
        }
        return path;
    }


    static void printPath(ArrayList<String> path)
    {
        int n = path.size();
        for(int i = 0; i < n - 1; i++) {
            System.out.print(path.get(i) + " -> ");
        }
        System.out.print(path.get(n - 1));
    }


    public static void main(String[] args) throws IOException {

        int i, j, k;

        BufferedReader br  = new BufferedReader(
                new FileReader(new File("src/GRAFO_NAVEGACION_PROYECTO.csv")));

        String cadena = br.readLine();
        String []tempElementos = cadena.split(",");

        n = tempElementos.length;
        graph = new int[n][n];

        for (i = 1; i < n; i++) { //ignora el primer elemento que debe ser cero
            int c = Integer.parseInt(tempElementos[i]);
            if (c == 0) c = 9999;
            graph[0][i] = c;
        }

        j = 1;
        while((cadena = br.readLine())!=null){
            //System.out.println(cadena);
            tempElementos = cadena.split(",");
            for (i = 0; i < n; i++) {
                int c =Integer.parseInt(tempElementos[i]);
                if (c == 0 && i!=j) c = 9999;
                graph[j][i] = c;
            }
            j++;
        }

        /*
        //Print graph
        for (i = 0; i < n; i++) {
            for (j = 0; j < n; j++) {
                System.out.print(graph[i][k] + " ");
            }
            System.out.println();
        }
         */

        T_trayectorias = new int[n][n];
        M_distancias = new int[n][n];

        for (i = 0; i < n; i++) {
            for (j = 0; j < n; j++) {
                M_distancias[i][j] = graph[i][j];

                // Si no hay camino ente i y j
                if (graph[i][j] == 9999)
                    T_trayectorias[i][j] = -1; //
                else
                    T_trayectorias[i][j] = j;

            }
        }

        System.out.println("Inicia FloydWarshall:");

        floydWarshall();

/*
        for (i = 0; i < n; i++) {
            for (j = 0; j < n; j++) {
                //if (i!=j){
                    int temp_i = i+65;
                    String letraInicio = (char)(temp_i>90? 65:temp_i)+"";
                    letraInicio += temp_i<=90? "": (char)(temp_i%91+65); //91... primer caracter que ya no es letra... -> [

                    int temp_j = j+65;
                    String letraFin = (char)(temp_j>90? 65:temp_j)+ "";
                    letraFin += temp_j<=90? "": (char)(temp_j%91+65);

                    System.out.print("Ruta de " +  letraInicio + " a " + letraFin + ": ");

                    ArrayList<Integer> camino = getPath(i, j);

                    ArrayList<String> newCamino = convierteToLetras(camino);

                    printPath(newCamino);

                    System.out.println("  Distancia: " + M_distancias[i][j]);
                //}
            }
        }
*/


        int origen = (int)'G'-65; //.... ** Incompleto para AA...
        int destino = (int)'R'-65; //.... ** Incompleto para AA...


        ArrayList<Integer> camino = getPath(origen, destino);
        ArrayList<String> newCamino = convierteToLetras(camino);

        for (int l = 0; l < camino.size(); l++) {
            System.out.println(newCamino.get(l));
        }

    }

    public static ArrayList<String> convierteToLetras(ArrayList<Integer> camino){
        ArrayList<String> newCamino =  new ArrayList<>();

        for (int i = 0; i < camino.size(); i++) {
            int temp_i = camino.get(i)+65;
            String letraInicio = (char)(temp_i>90? 65:temp_i)+"";
            letraInicio += temp_i<=90? "": (char)(temp_i%91+65);

            newCamino.add(letraInicio);
        }


        return newCamino;
    }


}