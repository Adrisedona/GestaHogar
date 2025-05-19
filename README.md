# GestaHogar
Repositorio del Trabajo de Fin de Ciclo
### 21.04.2025
Estructura inicial del proyecto creada
### 27.04.2025
Autenticación de usuarios añadida a la API, modelo de producto de usuario (relación entre usuario y producto) añadido.
### 15.05.2025
Mapeado completo de los controladores de la API y proyecto de scripts SQL realizado.
### 17.05.2025
Creación de las vistas provisionales de Log In, Register y UserProducts.
### 18.05.2025
Creación de todas las demás vistas provisionales, bug fixes, rutas de actualizar todos los productos y actualizar producto añadidas.
### 19.05.205
Validador añadido a ProductsController, bug fixes.

## Problemas surgidos
* La dependencia Mysql.EntityFrameworkCore de gestaHogar.Api falla al ejecutar las migraciones. Al ser un problema externo al programa, se utiliza Microsof.EntityFramworkCore.InMemory para realizar pruebas. Cuando la dependencia funcione correctamente, se podrán realizar las migraciones y ejecutar la aplicación como estaba previsto.
* La compilación del proyecto GestaHogar.UI falla para Android, iOS y Mac Catalyst. Por el momento, solo se compila como aplicación de escritorio Windows. Una vez se consiga arreglar el error, se restaurará el archivo GestaHogar.csproj para que compile a las plataformas mencionadas.
