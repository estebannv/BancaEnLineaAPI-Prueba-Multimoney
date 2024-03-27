# BancaEnLineaAPI-Prueba-Multimoney

# Controladores y Endpoints

Se desarrollaron dos controladores: CuentaBancariaController, destinado a la gestión y consulta de información de cuentas, y TransaccionController, orientado a la ejecución de transacciones tales como depósitos, retiros y traspaso de dinero entre cuentas.

CuentaBancariaController:

•	Endpoint InformacionCuentaBancaria (/api/cuenta-bancaria/id): recibe como parámetro el id de la cuenta bancaria a consultar y retorna la información de la cuenta, cliente, transacciones realizadas y su interés devengado.

TransaccionController:

•	Endpoint RealizarDeposito (/api/transaccion/deposito): recibe como parámetro un objeto Transaccion y retorna un mensaje y estatus.

•	Endpoint RealizarRetiro (/api/transaccion/retiro): recibe como parámetro un objeto Transaccion y retorna un mensaje y estatus.

•	Endpoint RealizarTraspaso (/api/transaccion/traspaso): recibe como parámetro un objeto Transaccion y retorna un mensaje y estatus.

Nota: La ejecución de las transacciones se realiza por medio del procedimiento almacenado SP_MAN_TRANSACCION

# Cálculo del interés diario

El cálculo del interés diario se lleva a cabo mediante la implementación de la clase TimedHostedService (Carpeta: "Functions"), la cual se activa automáticamente por el API cada 24 horas. Esta clase utiliza el servicio InteresDiarioService, el cual a su vez ejecuta el procedimiento almacenado SP_CALCULAR_INTERES_DIARIO encargado de la actualización y registro del historial pertinente.

Para el cálculo de los intereses, se ha diseñado una tabla de rangos (TBL_TASA) la cual establece una tasa diaria variable dependiendo del rango de montos en que se encuentre la cuenta en el momento de la ejecución de dicho procedimiento almacenadol

# Modelado

Arquitectura: Repository

Patrón de diseño: Dependency injection

Framework de consulta de datos: EntityFramework

# Aspectos de seguriad

Se han implementado medidas como HtmlSanitizer para prevenir ataques XSS, y se han utilizado procedimientos almacenados parametrizados para evitar la inyección SQL.

# Pruebas Unitarias

Se ha desarrollado un proyecto de pruebas, MiBancaEnLineaAPITest, el cual incluye pruebas unitarias que abarcan diversos tipos de entradas, para asegurar la fiabilidad del sistema en conjunto.

# Ejecutar solución

El script de base de datos se encuentra en la carpeta ScriptsBD.

Es necesario establecer un datasource correcto en la cadena de conexión en appsettings.json.
