using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Security;
using HomeCenter.AuthenticationDomain;
using HomeCenter.Communication.Hubs;
using HomeCenter.DeviceDomain;
using WebSocketSharp.Net;
using WebSocketSharp.Server;

namespace HomeCenter.Communication
{
    public class WebSocket : IWebSocket
    {
        public static BlockingCollection<DeviceDtoOutput> BlockingCollection { get; set; }

        private readonly IAuthenticationService _authenticationService;

        public WebSocket(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public void AddDeviceToUpdate(DeviceDtoOutput device)
        {
            if (BlockingCollection != null)
            {
                BlockingCollection.Add(device);
            }
        }
        public void Initialise()
        {
            WebSocketServiceHost service = null;
            BlockingCollection = new BlockingCollection<DeviceDtoOutput>();
            try
            {
                var wssv = new WebSocketServer(4649);
                wssv.AddWebSocketService<WebSite>("/WebSite", () => new WebSite()
                {
                    CookiesValidator = (req, res) =>
                    {
                        foreach (Cookie cookie in req)
                        {
                            if (cookie.Name == ".ASPXAUTH" || cookie.Name == ".MONOAUTH")
                            {
                                var formsAuthenticationTicket = FormsAuthentication.Decrypt(cookie.Value);
                                if (formsAuthenticationTicket != null)
                                {
                                    var username = formsAuthenticationTicket.Name;
                                    var roles = _authenticationService.GetRolesForUser(username);
                                    if (roles.Contains("Administrator") && roles.Contains("User"))
                                    {
                                        return true;
                                    }
                                }
                            }
                            res.Add(cookie);
                        }
                        return false;
                    }
                });
                wssv.Start();
                wssv.WebSocketServices.TryGetServiceHost("/WebSite", out service);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                Console.WriteLine(e.Message);
                Debug.WriteLine(e.InnerException);
                Debug.WriteLine(e.Message);
            }
            var t = new Thread(() =>
            {
                foreach (var data in BlockingCollection.GetConsumingEnumerable())
                {
                    if (service != null)
                        service.Sessions.Broadcast(ServiceStack.Text.JsonSerializer.SerializeToString(data));
                }
            });
            t.Start();
        }
    }

    public interface IWebSocket
    {
        void Initialise();
        void AddDeviceToUpdate(DeviceDtoOutput device);
    }
}
