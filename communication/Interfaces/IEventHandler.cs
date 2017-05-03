﻿using System.Net.Sockets;

namespace TexasHoldem.communication.Reactor.Interfaces
{
    public interface IEventHandler
    {
        void HandleEvent(string data);
    }
}
