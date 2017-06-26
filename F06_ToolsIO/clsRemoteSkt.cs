using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace nsFACC
{
    using System.Threading;
    using System.Net.Sockets;
    public class clsRemoteSkt
    {
        public string Id { get; set; }
        public Thread th { get; set; }
        public Socket m_skt_remote { get; set; }
        public bool StopTh { get; set; }
        public clsRemoteSkt()
        {
            StopTh = false;
        }
    }
}
