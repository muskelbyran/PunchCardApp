using PunchCardApp.Models;
using System.Collections.Concurrent;

namespace PunchCardApp.Services;

public interface ICircuitUserService
{
    ConcurrentDictionary<string, CircuitUserModel> Circuits { get; } // Thread safe dictionary

    event EventHandler CircuitsChanged;

    void Connect(string CircuitId, string UserId);
    void Disconnect(string CircuitId);
}
