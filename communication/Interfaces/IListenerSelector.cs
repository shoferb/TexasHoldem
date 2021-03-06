﻿using System.Collections.Generic;
using System.Net.Sockets;

namespace TexasHoldem.communication.Interfaces
{
    public interface IListenerSelector
    {
        IList<TcpClient> SelectForReading(IEnumerable<TcpClient> tcpClients);
        IList<TcpClient> SelectForWriting(IEnumerable<TcpClient> tcpClients);
    }
}
