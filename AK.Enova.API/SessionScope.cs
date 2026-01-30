using AK.Enova.API;
using Soneta.Business;
using System;
using System.Threading;

public sealed class EnovaSessionScope : IDisposable
{
    private static readonly object _lock = new();

    public Session Session { get; }
    private ITransaction _transaction;

    public EnovaSessionScope(EnovaService service)
    {
        Monitor.Enter(_lock);

        Session = service.Session;
        _transaction = Session.Logout(true);
    }

    public void Commit()
    {
        _transaction.Commit();
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        Monitor.Exit(_lock);
    }
}
