# 🧠 Inteligencia-Artificial-Videojuegos

**Repositorio de la asignatura de Inteligencia Artificial del Grado en Diseño y Desarrollo de Videojuegos.**  
Incluye los apuntes de clase y la práctica del primer bloque realizados durante el curso, también se incluye su respectiva memoria explicativa.

---

## Resumen del temario

La asignatura se ha dividido en dos bloques de temario. A continuación, se desglosa el contenido del primer bloque:

### 🔹 Bloque I – Fundamentos y búsqueda

1. **Introducción a la Inteligencia Artificial**  
   Definición, objetivos y principales disciplinas. Aplicaciones en videojuegos.

2. **Agentes inteligentes y entornos**  
   Tipos de agentes, autonomía, racionalidad y modelado de entornos en videojuegos.

3. **Búsqueda no informada**  
   Algoritmos como búsqueda en amplitud y búsqueda de coste uniforme.

4. **Búsqueda con heurísticas**  
   Uso de funciones heurísticas (A*, búsqueda local, hill climbing, búsqueda por horizonte, etc.).

5. **Búsqueda multiagente**  
   Estrategias para juegos con más de un agente o jugador.

---

## Práctica Bloque I

Este repositorio contiene la primera práctica desarrollada durante la asignatura.

Para una mejor comprensión de la implementación, se recomienda consultar la **memoria explicativa** disponible en PDF.

---

## 📋 Enunciado original de la Práctica 1

**Práctica 1: Búsqueda en el espacio de estados** consistía en implementar un agente inteligente en Unity™ (versión 2022.3.2f1, URP) capaz de desenvolverse en un mundo virtual utilizando algoritmos de búsqueda.

El entorno estaba compuesto por una escena base (`PlayGroundScene`) con un tablero de 20x20 unidades, obstáculos, cofres, enemigos (zombies) y una salida. El agente debía resolver distintos escenarios, implementando los algoritmos desde cero en C#.

### Objetivos específicos:

1. **Búsqueda del camino de salida**
   Implementar un algoritmo de búsqueda para encontrar el camino desde la posición inicial del agente hasta la salida (`Exit`), utilizando una heurística adecuada.

2. **Búsqueda por subobjetivos**
   El agente debía recoger todos los cofres (`Treasure`) y luego dirigirse a la salida, eligiendo el orden óptimo.

3. **Atrapar a los enemigos (no implementado)**
   Como reto adicional, el agente debía perseguir y atrapar enemigos (`Zombie`) en movimiento, lo cual requería implementar una búsqueda online.

---
