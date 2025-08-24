using System.Net;
using System.Net.WebSockets;
using System.Text;

namespace BrowserTabTinderFormApp
{
    public class WebSocketServer
    {
        private HttpListener httpListener { get; set; } = new HttpListener();
        public WebSocket? Socket { get; private set; } = null;

        public bool IsConnected { get; private set; }

        public event EventHandler OnSocketConnected;
        public event EventHandler OnSocketDisconnected;




        public WebSocketServer(string url = "http://localhost:9988/ws/")
        {
            httpListener.Prefixes.Add(url);
            httpListener.TimeoutManager.IdleConnection = TimeSpan.FromDays(0.5);
            httpListener.Start();


            //_ = Listen();
        }


        public async Task Listen()
        {
            
            while (Socket is null)
            {

                var httpContext = await httpListener.GetContextAsync();

                if (httpContext.Request.IsWebSocketRequest)
                {
                    var wsContext = await httpContext.AcceptWebSocketAsync(null,TimeSpan.FromSeconds(30));
                    Socket = wsContext.WebSocket;
                    IsConnected = true;
                    OnSocketConnected?.Invoke(null, null);
                }
                else
                {
                    httpContext.Response.StatusCode = 400;
                    httpContext.Response.Close();
                }

            }
        }

        public async Task<string> RecvStringMessage()
        {
            if (!IsConnected || Socket is null)
            {
                return "";
            }
            var buffer = new byte[4096];
            using var ms = new MemoryStream();

            try
            {
                while (true)
                {

                    var result = await Socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    ms.Write(buffer, 0, result.Count);

                    if (result.EndOfMessage)
                    {
                        if (result.MessageType == WebSocketMessageType.Close)
                        {
                            await Socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by server", CancellationToken.None);
                            Socket = null;
                            IsConnected = false;
                            OnSocketDisconnected?.Invoke(null, null);
                        }

                        break;
                    }
                }
            }
            catch (Exception)
            {

            }
            
                
            
            
                
            

            return Encoding.UTF8.GetString(ms.ToArray());
        }

        public async Task SendStringMessage(string message)
        {
            if (!IsConnected || Socket is null || Socket.State != WebSocketState.Open)
            {
                OnSocketDisconnected?.Invoke(null, null);
                return;
            }
            var data = Encoding.UTF8.GetBytes(message);
            await Socket?.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Text, true, CancellationToken.None);

        }
        





    }
}
