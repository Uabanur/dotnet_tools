# Examples of different technologies implemented in .Net

Todo:
- [K6 - Load testing](https://grafana.com/docs/k6/latest/)
- Loadbalancing e.g. [Azure Load Balancer](https://learn.microsoft.com/en-us/azure/load-balancer/load-balancer-overview)


Implemented examples:
- Basic database integrations:
    - Sqlite: [TodoApiSqlite](./TodoApiSqlite_EF)
    - MySql: [TodoApiMySql](./TodoApiMySql_EF)
    - Postgres: [TodoApoPostgres](./TodoApiPostgres_EF)
- Polly: [RetryPolly](./RetryPolly)
- RabbitMq: [SimpleRabbitMq](./SimpleRabbitMq) and [AsyncMessagingSignalR](./AsyncMessagingSignalR)
- SignalR: [SingalRChat](./SignalRChat) and [AsyncMessagingSignalR](./AsyncMessagingSignalR)
- MediatR: [SimpleMediatR](./SimpleMediatR)
