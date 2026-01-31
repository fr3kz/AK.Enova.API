using AK.Enova.API;
using Soneta.Business;
using Soneta.Tools;
using System;
using System.Threading;

public sealed class EnovaSessionScope : IDisposable
{
    private static readonly object _lock = new();

    public Session Session { get; }

    public EnovaSessionScope(EnovaService service)
    {
        Monitor.Enter(_lock);
        Session = service.Session;
        if(Session.IsClosed || Session.IsSaved)
        {
            Session.InvokeSaved();
            SessionState.ResetAttaching();
            SessionState.Create().Attach();
            Session = Session.Login.CreateSession(false, true);
        }
    }


    public void Dispose()
    {
        Session?.Dispose();
        Monitor.Exit(_lock);
    }
}
