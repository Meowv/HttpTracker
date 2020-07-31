<p align="center">
    <img src="https://raw.githubusercontent.com/Meowv/HttpTracker/master/logo.png" width="150" />
</p>

<h1 align="center">HttpTracker - Http请求跟踪器</h1>

<div align="center">

[![HttpTracker](https://img.shields.io/nuget/v/HttpTracker.svg?color=red&style=flat-square)](https://www.nuget.org/packages/HttpTracker/)
[![HttpTracker](https://img.shields.io/nuget/dt/HttpTracker.svg?style=flat-square)](https://www.nuget.org/packages/HttpTracker/)
[![HttpTracker](https://img.shields.io/badge/License-MIT-blue?style=flat-square)](https://github.com/Meowv/HttpTracker/blob/master/LICENSE)

</div>

## ✨ Introduction

`HttpTracker` 是一个基于`.NET Standard`的`C#`库，它是一种收集`HTTP`请求的解决方案，旨在收集我们用`.NET Core`开发的`WebApi`项目中的接口请求日志，将所有的`HTTP`请求进行拦截并保存在任意数据库中，用于后续的分析工作。

`HttpTracker` 支持将数据存储在大部分主流数据库中，收集到的数据将自动以年月进行分表存储，可以选择你喜爱的任意存储方式。

## 🆕 Getting Started

### 1️⃣ NuGet

你可以运行以下下命令在你的项目中安装 `HttpTracker`。

`PM> Install-Package HttpTracker`

`HttpTracker` 提供了 `Elasticsearch`，`MongoDb`，`SqlServer`，`MySql`，`PostgreSQL`，`SQLite`，`Oracle` 的扩展作为数据库存储，可以按需选择存储方式。

Storage | Nuget| Is it done?
---|---|---
`Elasticsearch` | `PM> Install-Package HttpTracker.Elasticsearch` | ✔
`MongoDb` | `PM> Install-Package HttpTracker.MongoDb` | ✔
`SqlServer` | `PM> Install-Package HttpTracker.SqlServer` | ✔
`MySql` | `PM> Install-Package HttpTracker.MySql` | ✔
`SQLite` | `PM> Install-Package HttpTracker.SQLite` | ✔
`PostgreSQL` | `PM> Install-Package HttpTracker.PostgreSQL` | ❌
`Oracle` | `PM> Install-Package HttpTracker.Oracle` | ❌

### 2️⃣ Configuration

首先配置 `HttpTracker` 到 `Startup.cs` 文件中。

在 `ConfigureServices` 中添加。

```c#
public void ConfigureServices(IServiceCollection services)
{
    ...

    services.AddControllers();

    services.AddHttpTracker().UseSQLite();

    ...
}
```

同时 `services.AddHttpTracker().UseSQLite();` 也支持重载，使用 `Action<Option>` 方式传递参数。

```c#
public void ConfigureServices(IServiceCollection services)
{
    ...

    services.AddHttpTracker(options =>
    {
        options.Disabled = false;
        options.ServerName = "...";
        ...
    }).UseSQLite(options =>
    {
        options.ConnectionString = "...";
    });

    ....
}
```

如果使用默认的选项配置需要在 `appsettings.json` 中添加 `HttpTracker` 属性，如下：

```json
{
  "HttpTracker": {
    "Disabled": false,
    "ServerName": "...",
    "ServerHost": "localhost",
    "ServerPort": 5000,
    "FilterRequest": [],
    "Storage": {
      "Elasticsearch": {
        "Nodes": [ "http://localhost:9200" ],
        "Username": "",
        "Password": ""
      },
      "MongoDb": {
        "Services": [ "localhost:27017" ],
        "ConnectionMode": "",
        "DatabaseName": "",
        "Username": "",
        "password": ""
      },
      "SQLServer": {
        "ConnectionString": "..."
      },
      "MySql": {
        "ConnectionString": "..."
      },
      "PostgreSQL": {
        "ConnectionString": "..."
      },
      "Oracle": {
        "ConnectionString": "..."
      },
      "SQLite": {
        "ConnectionString": "..."
      }
    },
  }
}
```

在 `Configure` 方法中使用。

```c#
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    ...

    app.UseRouting();

    app.UseHttpTracker();

    ...
}
```

注意将 `app.UseHttpTracker();` 放在 `app.UseRouting();` 后面使用。

### 3️⃣ Dashboard

`HttpTracker` 同时集成了一个仪表盘，用于将收集来的请求数据用于分析展示，Dashboard 计划采用 [Blazor](https://blazor.net/) 进行开发。

使用下面命令安装 Dashboard：

`PM> Install-Package HttpTracker.Dashboard`

然后和 `HttpTracker` 使用方式一样，分别 `AddHttpTrackerDashboard()`、`UseHttpTrackerDashboard()` 即可。

仪表盘默认的访问地址是：~/httptracker，你可以在配置项中进行修改路径后缀为其他的名字，同时仪表盘的访问默认开启了 BasicAuthentication 认证，默认的账号密码为：httptracker/httptracker，如果你不想开启认证也可以手动关闭。

## 🤝 Contributing

因个人能力以及时间精力有限，欢迎有兴趣的朋友们一起参与本项目的开发工作。

## 📑 TODO

- [x] HTTP请求跟踪器中间件 SDK
- [x] 数据存储 SDK，Elasticsearch 支持
- [x] 数据存储 SDK，MongoDb 支持
- [x] 数据存储 SDK，SqlServer 支持
- [x] 数据存储 SDK，MySql 支持
- [x] 数据存储 SDK，SQLite 支持
- [ ] 数据存储 SDK，PostgreSQL 支持
- [ ] 数据存储 SDK，Oracle 支持
- [x] 仪表盘 SDK
- [x] 仪表盘集成 BasicAuthentication 认证
- [x] 仪表盘 API，自动发现
- [ ] 基于 Blazor 的仪表盘开发
- [ ] 数据存储消息队列 SDK，RabbitMQ 支持
- [ ] 数据存储消息队列 SDK，Kafka 支持
- [ ] ...

## ⚡ Sample

[HttpTracker.Demo](https://github.com/Meowv/HttpTracker/tree/master/samples/HttpTracker.Demo)

## ☀️ License

This project is licensed under [MIT](LICENSE).
