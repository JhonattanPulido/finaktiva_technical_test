# Finaktiva Developer Technical Test

Este repositorio contiene el desarrollo de la prueba técnica de __Desarrollador Fullstack__

# Back-end

Se desarrolló un API en .NET 6

1. Ir al directorio `./back-end`
2. Ejecutar el archivo `FinaktivaBackend.sln` y abrir la solución con `Visual Studio 2022`
3. Presionar `F10` o dar clic en :arrow_forward: `Run` para ejecutar la aplicación

El API local trabajará en el host `https://localhost:7002`

# Front-end

Se desarrolló una Web Application en Angular V16

1. Abrir al directorio `./front-end` con `Visual Studio Code`
2. Usar `Ctrl` + `Shift` + `Ñ` para abrir una nueva terminal
3. Ejecutar el comando `npm i` para instalar las dependencias requeridas para la ejecución de la aplicación
4. Ejecutar el comando `ng serve -o`

La aplicacón web local trabajará en el host `http://localhost:4002`

# Database

Se hizo uso del motor `MongoDB`, la __BD__ se desplegó en el servicio de nube `MongoDB Atlas` para facilidad de ejecución del proyecto.

| Nombre       | Colección |
| ------------ | --------- |
| Registration | EventLogs |

* Estructura del documento en la colección __EventLogs__:

| Campo         | Tipo     | Ejemplo                                        |
| ------------- | -------- | ---------------------------------------------- |
| _id           | uuid     | _"5bf142459b72e12b2b1b2cd"_                    |
| descrption    | string   | _"Descripción del evento 1"_                   |
| type          | int      | `0-Success`, `1-Info`, `2-Warning` o `3-Error` |
| creation_date | datetime | _"2023-07-27T13:30:12"_                        |