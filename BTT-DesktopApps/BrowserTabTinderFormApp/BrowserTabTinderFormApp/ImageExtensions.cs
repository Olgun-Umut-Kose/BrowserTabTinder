using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserTabTinderFormApp
{
    public static class ImageExtensions
    {
        
        public static Image FromBase64(this string base64)
        {
            
            var commaIndex = base64.IndexOf(',');
            if (commaIndex >= 0)
                base64 = base64[(commaIndex + 1)..];

            byte[] bytes = Convert.FromBase64String(base64);
            using var ms = new MemoryStream(bytes);
            return Image.FromStream(ms);
        }
    }
}
