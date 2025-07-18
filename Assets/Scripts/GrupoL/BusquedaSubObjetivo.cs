using Assets.Scripts.GrupoL;
using Navigation.Interfaces;
using Navigation.World;
using System.Collections.Generic;
using UnityEngine;

public class BusquedaSubObjetivo : INavigationAgent
{
    public CellInfo CurrentObjective { get; private set; }
    public Vector3 CurrentDestination { get; private set; }
    public int NumberOfDestinations { get; private set; }

    private WorldInfo _worldInfo;
    //Algoritmo que usamos, en nuestro caso A*
    private INavigationAlgorithm _navigationAlgorithm;

    //Nodo Origen (no solo es el nodo en donde empieza,
    //si no en donde esta cuando llega a un subobjetivo
    private ClaseNodo _origin;
    //lista de subobjetivos
    private List<ClaseNodo> _objectives;
    //Camino que recorre el agente
    private Queue<CellInfo> _path; 


    public BusquedaSubObjetivo(WorldInfo worldInfo, INavigationAlgorithm navigationAlgorithm, CellInfo PosicionInic) //constructor
    {
        // inicializamos el mundo
        _worldInfo = worldInfo;
        //asignamos el algoritmo a la variable
        _navigationAlgorithm = navigationAlgorithm;
        //asigno como origen la posición inicial
        _origin = new ClaseNodo(PosicionInic); 

        _navigationAlgorithm.Initialize(_worldInfo, INavigationAlgorithm.AllowedMovements.FourDirections); //inicializo el algoritmo
    }

    public Vector3? GetNextDestination()
    {
        //Si no hay ningun objetivo todavia, creamos la lista de subobjetivos
        if (_objectives == null)
        {
            _objectives = FillGoalsList();
        }
        //Si mi camino está vacio y mi lista de objetivos no esta vacia
        //voy buscando el camino a cada meta
        if ((_path == null || _path.Count == 0) && _objectives.Count > 0) 
        {
            //ordeno la lista de metas poniendo el origen 
            //(Recordemos que el origen depende de que subobjetivo tenemos siguiente)
            orderGoalList(_origin);
            //el objetivo actual es siempre la primera
            //posición de la lista de subobjetivos
            CurrentObjective = _objectives[0].infoNodo;
            //Y el número de destinos será el
            //tamaño de la lista de subobjetivos
            NumberOfDestinations = _objectives.Count;

            //Calculamos el camino usando el algoritmo base (en nuestro caso A*)
            CellInfo[] path = _navigationAlgorithm.GetPath(_origin.infoNodo, CurrentObjective);
            
            _path = new Queue<CellInfo>(path); 

          //Cambiamos la meta una vez llegada a la anterior
            ChangeGoal(); 
        }

        if (_path.Count > 0)
        {
           
            CellInfo destination = _path.Dequeue();
            _origin.infoNodo = destination;
            CurrentDestination = _worldInfo.ToWorldPosition(destination);
        }
        return CurrentDestination;
    }
    //Simplemente eliminamos el primer subobjetivo de la lista
    //para que asi el siguiente subobjetivo sea el primero y no tener que hacer 
    //codigo para que recorra la lista, simplemente leerá siempre la primera posición

    private void ChangeGoal()
    {
        _objectives.RemoveAt(0);
    }

    private List<ClaseNodo> FillGoalsList()
    {
        //Creamos una lista auxiliar donde guardar los subobjetivos
        List<ClaseNodo> targets = new List<ClaseNodo>(); 
        for (int i = 0; i < _worldInfo.Targets.Length; i++)
        {
            //Guardamos los Subobjetivos (cofres)
            targets.Add(new ClaseNodo(_worldInfo.Targets[i])); 
        }
        //Finalmente guardamos la meta
        targets.Add(new ClaseNodo(_worldInfo.Exit));
        //Devolvemos la lista con todos los Subobjetivos
        return targets; 
    }
    //Se ordena la lista de subobjetivos menos la última posición (meta)
    //de esta manera la meta final siempre será el Exit
    private void orderGoalList(ClaseNodo  nodeActual) 
    {
        for (int j = 0; j < _objectives.Count - 1; j++)
        {
            for (int i = 0; i < _objectives.Count - 2; i++)
            {
                //Usamos para ordenar la lista de subobjetivos
                //la distancia manhattan de los cofres a la meta
                //y ponemos la lista en orden de manera que el primer
                //subobjetivo sea el cofre con la distancia manhattan más grande
                if (_objectives[i].calcularHeuristica(new ClaseNodo(_worldInfo.Exit)) < _objectives[i + 1].calcularHeuristica(new ClaseNodo(_worldInfo.Exit)))
                {
                    ClaseNodo aux = _objectives[i];
                    _objectives[i] = _objectives[i + 1];
                    _objectives[i + 1] = aux;
                }
            }
        }
    }
   
}