using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserTabTinderFormApp
{
    public interface IOverlay
    {
        void ShowOverlay(Form parent);
        void Hide();
        void Close();
    }
}
