using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using WebSocketSharp.Net;

namespace HomeCenter.Communication.Hubs
{
    using System;
    using WebSocketSharp;
    using WebSocketSharp.Server;

    public class WebSite : WebSocketBehavior
    {
        private string _suffix;

        public WebSite()
            : this(null)
        {
        }

        public WebSite(string suffix)
        {
            _suffix = suffix ?? String.Empty;
        }

        protected override void OnOpen()
        {
            
            //var cookies = Context.Headers.Get("Cookie");
            //string cookie2 = new HttpCookie(".ASPXAUTH").Value;
            //var decrypt = FormsAuthentication.Decrypt(cookie2);
            


        }

        protected override void OnMessage(MessageEventArgs e)
        {
            
            
            //user.IsInRole("Administrator");
            
            Send("Received: " + e.Data);
            
        }
    }
}
