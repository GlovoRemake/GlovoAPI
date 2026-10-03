# 🛵 GlovoAPI

> Бекенд REST API для платформи доставки у стилі Glovo, побудований на **ASP.NET Core (.NET 10)**.

<!-- TODO: замінити на реальні бейджі, коли з'явиться CI -->
![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)
![Docker](https://img.shields.io/badge/Docker-ready-2496ED?logo=docker&logoColor=white)
![Status](https://img.shields.io/badge/статус-у%20розробці-orange)

---

## 📖 Про проєкт

**GlovoAPI** — це бекенд ремейку сервісу доставки, натхненного Glovo. Він надає HTTP API, яким можуть користуватися клієнтські застосунки (мобільні, веб, застосунок кур'єра) для роботи з даними та бізнес-логікою платформи.

<!-- TODO: 2–3 речення про те, що вже реалізовано: замовлення, кур'єри, заклади, каталог, авторизація тощо -->

## ✨ Можливості

<!-- TODO: залиште лише те, що реально реалізовано -->
- 🏪 Заклади-партнери та каталог товарів
- 🧾 Створення замовлень і керування їхнім життєвим циклом
- 🚴 Керування кур'єрами
- 🔐 Автентифікація та авторизація
- 📍 Адреси / зони доставки
- 📚 Документація Swagger / OpenAPI

## 🧱 Архітектура

Solution складається з трьох проєктів:

| Проєкт | Призначення |
|---|---|
| **`Domain`** | Сутності, value objects та ключові бізнес-правила. Без залежностей від інфраструктури. |
| **`Core`** | Прикладний шар: сервіси, сценарії використання, інтерфейси/контракти. <!-- TODO: уточнити --> |
| **`GlovoAPI`** | Точка входу: ASP.NET Core Web API, контролери/ендпоінти, DI та конфігурація. |

```
GlovoAPI/
├── Core/              # Прикладна логіка
├── Domain/            # Доменна модель
├── GlovoAPI/          # Web API (стартовий проєкт)
├── Dockerfile         # Multi-stage збірка
└── GlovoAPI.slnx      # Файл solution
```

## 🧰 Технологічний стек

- **.NET 10** / ASP.NET Core
- **Docker** (multi-stage збірка: `dotnet/sdk:10.0` → `dotnet/aspnet:10.0`)
- <!-- TODO: база даних (PostgreSQL / SQL Server?), ORM (EF Core / Dapper?), автентифікація (JWT?) тощо -->

## 🚀 Швидкий старт

### Вимоги

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) (за бажанням)
- <!-- TODO: сервер бази даних, якщо потрібен -->

### Локальний запуск

```bash
git clone https://github.com/GlovoRemake/GlovoAPI.git
cd GlovoAPI

dotnet restore
dotnet run --project GlovoAPI
```

API запуститься за адресами, які будуть виведені в консолі.
<!-- TODO: додати реальну адресу, напр. https://localhost:5001/swagger -->

### Запуск через Docker

```bash
docker build -t glovo-api .
docker run -d -p 8080:8080 --name glovo-api glovo-api
```

> Образ використовує стандартний порт контейнерів ASP.NET Core 10 (`8080`).
> Налаштування передавайте через змінні середовища (`-e KEY=value`).

## ⚙️ Конфігурація

Налаштування читаються з `appsettings.json` та змінних середовища.

| Змінна | Опис | Приклад |
|---|---|---|
| `ASPNETCORE_ENVIRONMENT` | Середовище виконання | `Development` |
| `ConnectionStrings__Default` | Рядок підключення до БД <!-- TODO: уточнити ключ --> | `Host=...;Database=...` |
| <!-- TODO --> | <!-- секрет JWT тощо --> | |

> ⚠️ Ніколи не комітьте реальні секрети. Локально використовуйте [User Secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets), а в продакшені — змінні середовища.

## 📡 Огляд API

<!-- TODO: заповнити реальними ендпоінтами або дати посилання на Swagger -->

| Метод | Ендпоінт | Опис |
|---|---|---|
| `GET` | `/api/...` | ... |
| `POST` | `/api/...` | ... |

Інтерактивна документація доступна за адресою `/swagger` у середовищі Development.

## 🧪 Тестування

```bash
dotnet test
```

<!-- TODO: видалити цей розділ, якщо тестів ще немає -->

## 🗺️ Плани розвитку

Заплановані задачі та відомі проблеми дивіться у [відкритих issues](https://github.com/GlovoRemake/GlovoAPI/issues).

## 🤝 Внесок у проєкт

1. Зробіть fork репозиторію
2. Створіть гілку: `git checkout -b feature/my-feature`
3. Закомітьте зміни: `git commit -m "feat: add my feature"`
4. Запуште гілку: `git push origin feature/my-feature`
5. Відкрийте Pull Request

## 📄 Ліцензія

<!-- TODO: додайте файл LICENSE (напр. MIT) і оновіть цей рядок -->
Поширюється за ліцензією MIT. Деталі — у файлі `LICENSE`.

---

> ℹ️ Це навчальний / фан-проєкт, **не пов'язаний із Glovo**.
