````markdown
# 🌐 Tristevida API

API REST para gestionar **Countries, Regions, Cities, Companies y Branches** con PostgreSQL y .NET 8.

---

## 📌 Endpoints principales

### 🌍 Countries
- **Crear país**
  ```http
  POST http://localhost:5026/api/countries
````

```json
{
  "name": "Colombia"
}
```

* **Obtener todos**

  ```http
  GET http://localhost:5026/api/countries/all
  ```

* **Obtener por ID**

  ```http
  GET http://localhost:5026/api/countries/1
  ```

---

### 🗺️ Regions

* **Crear región**

  ```http
  POST http://localhost:5026/api/regions
  ```

  ```json
  {
    "name": "Santander",
    "countryId": 1
  }
  ```

* **Obtener todos**

  ```http
  GET http://localhost:5026/api/regions/all
  ```

---

### 🏙️ Cities

* **Crear ciudad**

  ```http
  POST http://localhost:5026/api/cities
  ```

  ```json
  {
    "name": "Barrancabermeja",
    "regionId": 1
  }
  ```

* **Obtener todos**

  ```http
  GET http://localhost:5026/api/cities/all
  ```

---

### 🏢 Companies

* **Crear compañía**

  ```http
  POST http://localhost:5026/api/companies
  ```

  ```json
  {
    "name": "Honda Barrancabermeja",
    "ukniu": "1234567890",
    "address": "Av. 52 #67-23",
    "cityId": 1,
    "email": "contacto@honda.com"
  }
  ```

* **Obtener todos**

  ```http
  GET http://localhost:5026/api/companies/all
  ```

---

### 🏬 Branches

* **Crear sucursal**

  ```http
  POST http://localhost:5026/api/branches
  ```

  ```json
  {
    "number_Comercial": 101,
    "address": "Calle 45 #32-10",
    "email": "sucursal1@honda.com",
    "contact_Name": "Juan Pérez",
    "cityId": 1,
    "companyId": 1
  }
  ```

* **Obtener todos**

  ```http
  GET http://localhost:5026/api/branches/all
  ```

---

## 📝 Orden recomendado para pruebas

1. **Countries** → Crear primero un país.
2. **Regions** → Crear regiones ligadas a un país.
3. **Cities** → Crear ciudades ligadas a una región.
4. **Companies** → Crear compañías ligadas a una ciudad.
5. **Branches** → Crear sucursales ligadas a una compañía y ciudad.

---

## 🚀 Ejecución

1. Corre las migraciones:

   ```bash
   dotnet ef database update -p Tristevida.Infrastructure -s Tristevida.Api
   ```
2. Inicia la API:

   ```bash
   dotnet run --project Tristevida.Api
   ```
3. Prueba los endpoints en **Insomnia** o **Postman**.

```

¿Quieres que te lo prepare con una **tabla resumen de endpoints** al inicio (tipo CRUD con método + ruta) para que quede aún más pro?
```
