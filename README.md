# Sistema de Gestión de Concesionario 🚗🏍️

Una aplicación de consola en **C# (.NET 8.0)** para la gestión de un concesionario de vehículos. Permite registrar clientes, administrar un inventario de motos y carros, y procesar ventas calculando automáticamente los precios finales con IVA. Todo el sistema cuenta con **persistencia de datos** mediante archivos CSV.

---

## 📋 Características Principales

* **Gestión de Clientes:** Registro, actualización, listado y eliminación de clientes.
* **Inventario (Almacén):** Soporte para herencia polimórfica (`Carro` y `Moto`). Cálculo de IVA dinámico basado en el cilindraje del vehículo.
* **Sistema de Ventas:** Creación de facturas detalladas asociando clientes y múltiples vehículos, marcando automáticamente el inventario como "Vendido".
* **Persistencia de Datos:** Toda la información se guarda y carga automáticamente en archivos locales `.csv` (`clientes.csv`, `vehiculos.csv`, `ventas.csv`).

---

## 🛠️ Tecnologías y Conceptos Aplicados

* **Lenguaje:** C# 12 / .NET 8.0
* **Paradigma:** Programación Orientada a Objetos (POO)
  * **Herencia y Polimorfismo:** Clase abstracta `Vehiculo` heredada por `Moto` y `Carro`.
  * **Interfaces:** Implementación de `IVendible` (comportamiento de venta) e `IPersistible` (serialización a CSV).
* **Almacenamiento:** Lectura y escritura manual de archivos `.csv` para manejo de tipos complejos, y soporte preparado con `CsvHelper`.

---

## 🚀 Requisitos e Instalación

1. Asegúrate de tener instalado el [SDK de .NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0).
2. Clona o descarga el repositorio.
3. Abre una terminal y navega hasta el directorio del proyecto (donde está el archivo `Concesionario.csproj`).

```bash
# Compilar el proyecto
cd src/Concesionario
dotnet build

# Ejecutar la aplicación
cd src/Concesionario
dotnet run
```

---

## 📖 Guía de Uso Rápido

El menú principal interactivo te guiará por las opciones:

1. **Registrar Cliente:** Crea un cliente para poder asignarle compras. Se guardará en `clientes.csv`.
2. **Consultar/Registrar en Almacén:** Agrega motos o carros. Se te pedirá la información básica y el cilindraje (usado para calcular el IVA). Se guardará en `vehiculos.csv`.
3. **Registrar Venta:**
   * Ingresa la cédula de un cliente existente.
   * El sistema mostrará los vehículos disponibles en el almacén.
   * Ingresa los IDs de los vehículos que el cliente va a comprar (separados por coma).
   * Se generará la `Factura` con el cálculo de los totales, los vehículos pasarán a estado "Vendido" y se guardarán en `ventas.csv`.

---

## 📂 Archivos Generados

Al ejecutar el programa y registrar datos, se crearán (o actualizarán) automáticamente en la carpeta de ejecución (`bin/Debug/net8.0/` o en la carpeta raíz, dependiendo de cómo ejecutes):

* 📁 `clientes.csv` - Base de datos de clientes.
* 📁 `vehiculos.csv` - Inventario del almacén (disponibles y vendidos).
* 📁 `ventas.csv` - Histórico de recibos/facturas.

---

## 🏗️ Diagrama de Clases UML Simplificado

* `Cliente` (Cédula, Nombre, Teléfono, Dirección)
* `Vehiculo` (Id, Marca, Modelo, Precio, Cilindraje...) 
  * `Carro` (1400cc - 2000cc -> 10% IVA, > 2000cc -> 20% IVA)
  * `Moto` (100cc - 300cc -> 10% IVA, > 300cc -> 20% IVA)
* `Venta` (Id, Fecha, Cliente, Vehículos, Factura)
* `Factura` (Desglose y Total de la Venta)
* `IPersistible` (`ToCsv()`)
* `IVendible` (`Vendido`, `Vender()`)

---

Desarrollado para el **Taller 1 de Programación Orientada a Objetos**.

Presentado por:

* Juan Esteban Londoño Buitrago     ID: 000575789
* Johan Quirama Acevedo             ID: 000577319
* Sofia Arango González             ID: 000260726
