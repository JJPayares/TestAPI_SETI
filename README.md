# TestAPI

Este es un proyecto .NET 8 con una API minimalista. Se puede ejecutar tanto en Visual Studio como en la terminal utilizando Docker.

## Requisitos
- .NET 8 SDK
- Docker Desktop instalado y en ejecuci�n
- Visual Studio 2022 (opcional, pero recomendado para depuraci�n)

---

## Ejecuci�n en Visual Studio con Docker

1. Abre Visual Studio 2022.
2. Carga la soluci�n `TestAPI.sln`.
3. En la barra de herramientas, selecciona **Docker** como perfil de ejecuci�n.
4. Presiona `F5` o haz clic en el bot�n de **Ejecutar**.

Visual Studio se encargar� de construir la imagen Docker y ejecutar el contenedor autom�ticamente.

---

## Construcci�n y ejecuci�n con Docker desde la terminal

1. Abre una terminal en la ra�z del proyecto donde est� el `Dockerfile`.
2. Construye la imagen Docker:
   ```
   docker build -t testapi:latest .
   ```
3. Ejecuta un contenedor basado en la imagen creada:
   ```
   docker run -p 8080:8080 testapi:latest
   ```
4. La API estar� disponible en `http://localhost:8080/swagger/index.html`.

---

## Depuraci�n en Docker desde Visual Studio
Si necesitas depurar la API dentro de un contenedor Docker:
1. Aseg�rate de tener activada la opci�n **Docker** en Visual Studio.
2. Coloca puntos de interrupci�n en tu c�digo.
3. Ejecuta la API en modo depuraci�n (`F5`).
4. Visual Studio se conectar� al contenedor y permitir� depuraci�n en vivo.

---

## Estructura del Proyecto
```
TestAPI/
?-- Controllers/       # Controladores de la API
?-- Properties/        # Configuraciones del proyecto
?-- appsettings.json   # Configuraci�n de la API
?-- Dockerfile         # Configuraci�n de Docker
?-- Program.cs         # Punto de entrada de la API
?-- TestAPI.sln        # Archivo de soluci�n de Visual Studio
```

---

