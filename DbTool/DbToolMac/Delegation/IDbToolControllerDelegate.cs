using System;
using DbToolMac.Models;

namespace DbToolMac.Delegation
{
    public interface IDbToolControllerDelegate
    {
        MainWindowViewModel Model { get; }
    }
}

