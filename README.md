# ATM System (Onion Architecture + ASP.NET Controllers + MSDI)

Проект реализует систему банкомата на основе **луковой архитектуры**: доменная модель и бизнес-правила находятся в центре, а инфраструктура (хранение, веб) подключается снаружи через абстракции и DI.

---

## Features

### Sessions
Поддерживаются два типа сессий. При создании сессии возвращается **session key** (например, `Guid`). Все дальнейшие запросы обязаны передавать ключ сессии и проходят авторизацию.

- **User session**
  - создаётся по данным счёта: номер + PIN
  - сохраняет контекст, необходимый для операций над счётом пользователя

- **Admin session**
  - создаётся по системному паролю (параметризуется через конфигурацию)
  - используется для административных операций (например, создание счетов)

Сессии и их данные сохраняются через репозитории.

### Accounts & Operations
- создание счёта
- просмотр баланса
- пополнение счёта
- снятие денег со счёта
- просмотр истории операций

---

## Web API
Интерфейс реализован на **ASP.NET** с использованием **Controllers**

Базовые ошибки:
- `Unauthorized` — неверный ключ сессии, неверный PIN/пароль, недостаточно прав
- `BadRequest` — некорректная операция (например, попытка снять сумму больше баланса)

---

## Data storage
Данные сохраняются через репозитории. Текущая реализация хранения — **in-memory**.

---

## Architecture (Onion)

### Layers
Разбиения слоёв:

- **Domain (Core)**
  - сущности, value objects, доменные правила
  - инварианты и бизнес-ограничения

- **Application**
  - use-cases (сценарии): create session, create account, withdraw, deposit, get balance, get history
  - порты: интерфейсы репозиториев и сервисов
  - DTO/команды/результаты
    
- **Infrastructure**
  - реализации портов: in-memory репозитории, конфигурационные адаптеры

- **WebApi**
  - Controllers, request/response модели
  - приём `session key` и передача его в application слой
  - маппинг ошибок на HTTP-коды (`BadRequest` / `Unauthorized`)

---

## Dependency Injection
Связывание абстракций и реализаций выполнено через **Microsoft.Extensions.DependencyInjection (MSDI)**.

Каждый модуль содержит extension methods, которые инкапсулируют регистрацию всех реализаций модуля, например:
- `AddApplication`
- `AddInfrastructure`
- `AddWebApi`

---

## Suggested solution layout

- `Atm.Domain`
- `Atm.Application`
- `Atm.Infrastructure`
- `Atm.WebApi`
- `Atm.Tests`

---

## Testing
Unit-тесты проверяют **бизнес-логику** и не зависят от Web слоя

В тестах используются **моки репозиториев**

---

## Dependencies
- ASP.NET (Controllers)
- Microsoft.Extensions.DependencyInjection (MSDI)
- NSubstitute

---

## Links
- NSubstitute: https://www.nuget.org/packages/NSubstitute  
- MSDI overview: https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-usage

---

## Run (template)

```bash
dotnet restore
dotnet build
dotnet test
dotnet run --project Atm.WebApi
