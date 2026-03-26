# AutoSpaceTestTask

Веб-приложение на ASP.NET Core с базой данных SQL Server, разворачиваемое в Docker.

## Требования

- Установленные **Docker** и **Docker Compose** (версия 3.9 или выше)
- Порт **5000** (веб-приложение) и **1433** (SQL Server) должны быть свободны

## Переменные окружения

Замените при желании в файле `.env` в корне проекта значения пароля для учетной записи администратора в развертываемой базе на более надежное.
Пароль замените следующим образом 

SA_PASSWORD: <your password>
ConnectionStrings__DefaultConnection=Server=db;Database=AsStorageDb;User Id=sa;Password=<your password>;TrustServerCertificate=True;


В корневой папке проекта выполните команду docker compose up
Дождитесь появления в консолии сообщения 

web-1      | info: Microsoft.Hosting.Lifetime[14]
web-1      |       Now listening on: http://[::]:8080
web-1      | info: Microsoft.Hosting.Lifetime[0]
web-1      |       Application started. Press Ctrl+C to shut down.
web-1      | info: Microsoft.Hosting.Lifetime[0]
web-1      |       Hosting environment: Production

Для открытия веб приложения перейдите по ссылке http://localhost:5000/

