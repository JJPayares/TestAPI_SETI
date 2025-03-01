# TestAPI

Este es un proyecto .NET 8 con una API minimalista. Se puede ejecutar tanto en Visual Studio como en la terminal utilizando Docker.

## Requisitos
- .NET 8 SDK
- Docker Desktop instalado y en ejecución
- Visual Studio 2022 (opcional, pero recomendado para depuración)

---

## Ejecución en Visual Studio con Docker

1. Abre Visual Studio 2022.
2. Carga la solución `TestAPI.sln`.
3. En la barra de herramientas, selecciona **Docker** como perfil de ejecución.
4. Presiona `F5` o haz clic en el botón de **Ejecutar**.

Visual Studio se encargará de construir la imagen Docker y ejecutar el contenedor automáticamente.

---

## Construcción y ejecución con Docker desde la terminal

1. Abre una terminal en la raíz del proyecto donde está el `Dockerfile`.
2. Construye la imagen Docker:
   ```
   docker build -t testapi:latest .
   ```
3. Ejecuta un contenedor basado en la imagen creada:
   ```
   docker run -p 8080:8080 testapi:latest
   ```
4. La API estará disponible en `http://localhost:8080/swagger/index.html`.

---

## Depuración en Docker desde Visual Studio
Si necesitas depurar la API dentro de un contenedor Docker:
1. Asegúrate de tener activada la opción **Docker** en Visual Studio.
2. Coloca puntos de interrupción en tu código.
3. Ejecuta la API en modo depuración (`F5`).
4. Visual Studio se conectará al contenedor y permitirá depuración en vivo.

---

## Estructura del Proyecto
```
TestAPI/
?-- Controllers/       # Controladores de la API
?-- Properties/        # Configuraciones del proyecto
?-- appsettings.json   # Configuración de la API
?-- Dockerfile         # Configuración de Docker
?-- Program.cs         # Punto de entrada de la API
?-- TestAPI.sln        # Archivo de solución de Visual Studio
```

---

