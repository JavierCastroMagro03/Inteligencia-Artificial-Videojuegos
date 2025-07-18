using Assets.Scripts.GrupoL;
using Navigation.Interfaces;
using Navigation.World;
using System.Collections.Generic;
using UnityEngine;
using static Navigation.Interfaces.INavigationAlgorithm;


public class AEstrella : INavigationAlgorithm
{
    //Nodos necesarios
    ClaseNodo currentState;


    List<ClaseNodo> succesorStates = new List<ClaseNodo>();
    List<ClaseNodo> openList = new List<ClaseNodo>();
    List<ClaseNodo> closeList = new List<ClaseNodo>();

    //Variables necesarias
    private int g = 1; //coste que hemos puesto, constante
    private WorldInfo world;

    public void Initialize(WorldInfo worldInfo, AllowedMovements allowedMovements)
    {
        world = worldInfo;
    }

    public CellInfo[] GetPath(CellInfo NodoInicio, CellInfo NodoTarget) //Implementamos el algoritmo A*
    {
        CellInfo[] path = new CellInfo[1];
        //Convertimos las celdas en Nodos y obtenemos el nodo inicial y final
        ClaseNodo initialState = new ClaseNodo(NodoInicio);
        ClaseNodo goalState = new ClaseNodo(NodoTarget);

        openList.Add(initialState);

        while (openList.Count != 0) {
        currentState = openList[0];
            openList.RemoveAt(0);

            //Vemos si el nodo en el que estamos es el nodo meta
            if(currentState.esNodoIgual(goalState))
            {
                Debug.Log("Meta encontrada!!!!");
                path =CaminoRellenado(currentState).ToArray(); //Cogemos la rama en la que se llega a la meta
                openList.Clear();
                closeList.Clear(); 
                return path;
               

            }
           
            if (puedeExpandir(currentState))
            {
                expandirNodo(currentState);

                foreach(ClaseNodo sucessor in succesorStates)
                {
                    sucessor.ponerPadre(currentState);
                    sucessor.calcularFNodo(goalState, g);
                    openList.Add(sucessor);
                }
                ordenaHeuristica();
                closeList.Add(currentState);
                g++;
            }
            succesorStates.Clear();
        }
        return path;
    }

    private List<CellInfo> CaminoRellenado (ClaseNodo nodoMeta)
    {
        List<CellInfo> aux = new List<CellInfo>();
        ClaseNodo nodoPadre= nodoMeta.getPadre();

        aux.Add(nodoMeta.infoNodo);

        while(nodoPadre != null) //Cuando es null seria el nodo inicial
        {
            //Hacer una lista desde la meta hasta el origen
            aux.Add (nodoPadre.infoNodo); 
            nodoPadre = nodoPadre.getPadre();
        }
        // Aqui se usa el reverse para hacer la lista en orden correcto desde origen a meta
        aux.Reverse();
        return aux;

    }

    private bool puedeExpandir(ClaseNodo nodoToAdd) // Vemos si esta expandido el nodo
    {
        foreach (ClaseNodo nodo in closeList)
        {
            if (nodo.esNodoIgual(nodoToAdd))
            {
                return false;
            }
            
        }
        
            return true;

    }
    private void expandirNodo(ClaseNodo expNodo)
    {
        //Se definen las cuatro direcciones en las que se expandirán los nodos
        ClaseNodo Up = new ClaseNodo(world[expNodo.infoNodo.x, expNodo.infoNodo.y - 1]);
        ClaseNodo Down = new ClaseNodo(world[expNodo.infoNodo.x,expNodo.infoNodo.y + 1]);
        ClaseNodo Right = new ClaseNodo(world[expNodo.infoNodo.x + 1, expNodo.infoNodo.y]);
        ClaseNodo Left = new ClaseNodo(world[expNodo.infoNodo.x - 1, expNodo.infoNodo.y]);

        // solo se añaden los nodos walkable
        if(Up.isWalkable()) succesorStates.Add(Up); 
        if(Down.isWalkable())succesorStates.Add(Down);
        if (Right.isWalkable()) succesorStates.Add(Right);
        if (Left.isWalkable()) succesorStates.Add(Left);
        

    }
    //Se ordena de acuerdo al peso de cada nodo (ascendente debido al algoritmo)
    private void ordenaHeuristica()
    {
        for(int i=0;i<openList.Count;i++)
        {
            for(int j = 0; j < openList.Count-1; j++)
            {
                if (openList[j].f > openList[j + 1].f)
                {
                    ClaseNodo aux = openList[j];
                    openList[j] = openList[j + 1];
                    openList[j+1]= aux;
                }
            }
        }
    }
}
