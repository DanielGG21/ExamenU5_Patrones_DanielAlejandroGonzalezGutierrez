# Unidad 5 – Controlador de Dispositivos Inteligentes

Proyecto de consola en C# .
Patrones de Diseño
Gonzalez GFutierrez Daniel Alejandro 21212341.

Implementa un **controlador de casa inteligente** usando varios patrones:

- **Decorator**: para agregar comportamientos extra a los dispositivos.
- **Composite**: para controlar grupos de dispositivos como una sola unidad (Sala, Cocina, Cuarto, Baño, Casa).
- **Memento**: para guardar y restaurar escenas (configuraciones de encendido/apagado).
- **Singleton**: para tener un único centro de control de la casa.
- **Arquitectura en Capas**: separación en Presentación, Aplicación y Dominio.

---

## Arquitectura en capas

La solución está separada en tres proyectos:

- `Presentacion`
  - Contiene `Program.cs`
  - Solo arranca la aplicación:
    - `CentroDeControlCasa.Instancia.Iniciar();`

- `Aplicacion`
  - `CentroDeControlCasa` (Singleton, orquestador principal)
  - `ControladorGrupos` (menús para controlar Sala, Cocina, Cuarto, Baño y Casa)
  - `ControladorEscenasCasa` (gestiona escenas con Memento)
  - `ServicioDecoracionDispositivos` (usa Decorator para decorar dispositivos)

- `Dominio`
  - **Core de la casa inteligente**:
    - `IDispositivo`, `DispositivoSimple`
    - `DispositivoDecorador` + decoradores:
      - `DecoradorAhorroEnergia`
      - `DecoradorModoNocturno`
      - `DecoradorModoCine`
      - `DecoradorModoFiesta` (según implementación)
    - `GrupoDispositivos` (Composite)
    - `ConstructorCasa` (construye Sala, Cocina, Cuarto, Baño y Casa)
  - **Memento (escenas)**:
    - `EstadoCasaOriginator`
    - `EscenaCasaMemento`
    - `HistorialEscenasCasa`

---

## Funcionamiento general

1. El usuario inicia el programa (proyecto `Presentacion`).
2. `CentroDeControlCasa` (Singleton) se encarga de:
   - Crear los dispositivos iniciales.
   - Permitir decorarlos (Decorator).
   - Construir la estructura de la casa (Composite).
   - Crear Originator + Historial para escenas (Memento).
   - Crear los controladores y mostrar los menús.

3. Desde el menú, el usuario puede:
   - Encender/apagar grupos (Sala, Cocina, Cuarto, Baño, Casa).
   - Encender/apagar dispositivos específicos dentro de cada grupo.
   - Decorar dispositivos con diferentes modos (ahorro, nocturno, cine, etc.).
   - Guardar escenas de la casa (estado de encendido/apagado).
   - Restaurar escenas guardadas.

---

## Patrones implementados

- **Decorator**
  - Permite agregar comportamientos como “Ahorro de energía”, “Modo nocturno” o “Modo cine” a cualquier `IDispositivo` sin modificar su clase base.

- **Composite**
  - `GrupoDispositivos` permite tratar grupos (Sala, Cocina, Cuarto, Baño, Casa) como si fueran un dispositivo más (se pueden encender/apagar de forma recursiva).

- **Memento**
  - `EstadoCasaOriginator` crea y restaura escenas completas.
  - `EscenaCasaMemento` guarda el nombre y el estado ON/OFF de cada dispositivo.
  - `HistorialEscenasCasa` administra la lista de escenas guardadas.

- **Singleton**
  - `CentroDeControlCasa` asegura que solo haya una instancia central que coordina toda la casa inteligente.

- **Arquitectura en capas**
  - Presentación → solo interfaz (consola).
  - Aplicación → lógica de orquestación y casos de uso.
  - Dominio → lógica de negocio, dispositivos, patrones.

---

## Cómo ejecutar

1. Abrir la solución en Visual Studio.
2. Establecer **`Presentacion`** como *proyecto de inicio*.
3. Compilar la solución.
4. Ejecutar (F5 o Ctrl + F5).
5. Seguir el menú en consola para:
   - Decorar dispositivos.
   - Encender/apagar grupos.
   - Guardar/restaurar escenas.

---
