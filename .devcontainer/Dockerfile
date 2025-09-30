# Usa la imagen base de .NET
FROM mcr.microsoft.com/devcontainers/dotnet:1-9.0

# Instala el cliente de PostgreSQL
RUN apt-get update && apt-get install -y \
    postgresql-client \
    apt-transport-https \
    curl

# ----------------------------------------------------
# PASOS CRUCIALES PARA INSTALAR LAS HERRAMIENTAS DE SQL SERVER
# Esto asegura que 'sqlcmd' y otras utilidades estén disponibles
# ----------------------------------------------------
RUN curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > /usr/share/keyrings/microsoft-prod.gpg && \
    curl https://packages.microsoft.com/config/debian/12/prod.list | tee /etc/apt/sources.list.d/microsoft-prod.list && \
    apt-get update && \
    ACCEPT_EULA=Y apt-get install -y msodbcsql18 mssql-tools unixodbc-dev

# Crea un enlace simbólico para que sqlcmd sea accesible en el PATH del sistema
RUN ln -s /opt/mssql-tools/bin/sqlcmd /usr/local/bin/sqlcmd

# ----------------------------------------------------

# Limpia los paquetes para reducir el tamaño de la imagen
RUN apt-get clean && rm -rf /var/lib/apt/lists/*

# Establece el directorio de trabajo
WORKDIR /workspaces

# Comando por defecto para mantener el contenedor en ejecución
CMD ["tail", "-f", "/dev/null"]