#region Copyright
// MIT License
// 
// Copyright (c) 2023 David María Arribas
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using Navigation.Interfaces;
using Navigation.World;



namespace Assets.Scripts.GrupoL
{
    public class ClaseNodo 
    {
        public CellInfo infoNodo; //Coordenadas de la celda en la que estamos
        public float h; //heuristica de cada nodo
        public ClaseNodo padre; //Nodo padre para poder asignarlo
        public float f;

        public ClaseNodo(CellInfo celda) // Constructor de los nodos, pasamos de celda a nodo
        {
            this.infoNodo = new CellInfo(celda.x,celda.y);
            this.infoNodo.Type = celda.Type;

            this.padre = null; //Simplemente inicializamos los valores
            this.f=0;
            this.h = 0;
        }

        public bool tienePadre() //Vemos si tiene padre o es Batman ;)
        {
            if(this.padre != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ponerPadre(ClaseNodo PadreN) //Asignamos a cada nodo un padre
        {
            this.padre = PadreN;
        }
        public ClaseNodo getPadre() {  // devuelve el Padre
            return this.padre; 
        }

        public bool esNodoIgual(ClaseNodo nodo) //Consultamos si 2 nodos son iguales
        {
            return this.infoNodo.Equals(nodo.infoNodo);
        }

        public bool isWalkable() //Vemos si el nodo es andable (se ve con los tipos de celdas)
        {
            return this.infoNodo.Walkable;
        }
        
        public float calcularHeuristica(ClaseNodo meta) //Calculamos nuestra heuristica, en nuestro caso es distancia Manhattan 
        {
            return this.h = MathF.Abs(this.infoNodo.x - meta.infoNodo.x) + MathF.Abs(this.infoNodo.y - meta.infoNodo.y);
        }
        
        public void calcularFNodo (ClaseNodo NodoMeta, float g) //Calcular f = g+h
        {
            this.f = g + calcularHeuristica(NodoMeta);
        }
    }
}