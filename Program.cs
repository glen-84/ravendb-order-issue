using DotNet.Testcontainers.Builders;
using Microsoft.Extensions.Logging.Abstractions;
using Raven.Client.Documents;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using ravendb_order_issue;

const int port = 8080;
const string db = "Test";

var container = new ContainerBuilder()
    .WithImage("ravendb/ravendb:5.4-ubuntu-latest")
    //.WithImage("ravendb/ravendb:6.0-ubuntu-latest")
    .WithPortBinding(port, true)
    .WithWaitStrategy(Wait.ForUnixContainer().UntilHttpRequestIsSucceeded(r => r.ForPort(port)))
    .WithLogger(NullLogger.Instance)
    .Build();

await container.StartAsync();

var store = new DocumentStore()
{
    Urls = [$"http://{container.Hostname}:{container.GetMappedPublicPort(port)}"],
    Database = db
};

store.Initialize();

store.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(db)));

using var session = store.OpenSession();

Bar[] _barEntities =
[
    new()
    {
        Foo = new Foo
        {
            BarShort = 12,
            BarBool = true
        },
    },
    new()
    {
        Foo = new Foo
        {
            BarShort = 14,
            BarBool = true
        },
    },
    new()
    {
        Foo = new Foo
        {
            BarShort = 13,
            BarBool = false
        },
    },
    new()
    {
        Foo = null
    }
];

foreach (var bar in _barEntities)
{
    session.Store(bar);
}

session.SaveChanges();

var result = session.Query<Bar>()
    .OrderByDescending(b => b.Foo!.BarBool)
    .ThenByDescending(b => b.Foo!.BarShort)
    .ToList();

foreach (var bar in result)
{
    Console.WriteLine($"Foo.BarBool: {bar.Foo?.BarBool}, Foo.BarShort: {bar.Foo?.BarShort}");
}

await container.StopAsync();
