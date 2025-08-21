using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserTabTinderFormApp
{
    public class WebSocketMessage<T> 
    {
        public string Command { get; set; }
        public T? Message { get; set; }
    }

    
}
