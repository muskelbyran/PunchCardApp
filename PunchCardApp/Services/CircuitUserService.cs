using PunchCardApp.Models;
using System.Collections.Concurrent;

namespace PunchCardApp.Services;

public class CircuitUserService : ICircuitUserService
{
    public ConcurrentDictionary<string, CircuitUserModel> Circuits { get; private set; }

    public event EventHandler CircuitsChanged;

    void OnCircuitsChanged() => CircuitsChanged?.Invoke(this, EventArgs.Empty);

    public CircuitUserService()
    {
        Circuits = new ConcurrentDictionary<string, CircuitUserModel>();
    }

    public void Connect(string CircuitId, string UserId)
    {
        if (Circuits.ContainsKey(CircuitId))
            Circuits[CircuitId].UserId = UserId;
        else
        {
            var circuitUser = new CircuitUserModel();
            circuitUser.UserId = UserId;
            circuitUser.CircuitId = CircuitId;
            Circuits[CircuitId] = circuitUser;
        }

        OnCircuitsChanged();
    }

    public void Disconnect(string CircuitId)
    {
        CircuitUserModel circuitremoved;
        Circuits.TryRemove(CircuitId, out circuitremoved);

        if(circuitremoved != null)
        {
            OnCircuitsChanged();
        }
    }
}
