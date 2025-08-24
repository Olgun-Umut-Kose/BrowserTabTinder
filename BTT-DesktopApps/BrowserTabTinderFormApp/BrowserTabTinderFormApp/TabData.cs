using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserTabTinderFormApp
{
    public class TabData
    {
        public ulong Id { get; set; }
        public ulong WindowId { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string ImageB64 { get; set; }

        public long TabCount { get; set; }

        public long CompletedTabCount { get; set; }
    }
}
