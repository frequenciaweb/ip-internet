using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Xml;

namespace ip_request_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ObterIpInternetController : ControllerBase
    {
        [HttpGet("Versao")]
        public string Versao()
        {
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string strWorkPath = Path.GetDirectoryName(strExeFilePath);
            FileInfo fi = new FileInfo(strWorkPath + @"//ip-request-api.dll");
            if (fi.CreationTime > fi.LastWriteTime)
            {
                return fi.CreationTime.ToString();
            }

            return fi.LastWriteTime.ToString();
        }

        [HttpGet("GetJson")]
        public string GetJson()
        {


            return JsonConvert.SerializeObject(HttpContext.Request, Newtonsoft.Json.Formatting.Indented);
        }


        [HttpGet("Get1")]
        public string Get1()
        {
            var remoteIpAddress = HttpContext.Connection.RemoteIpAddress;
            return remoteIpAddress.ToString();
        }

        [HttpGet("Get2")]
        public string Get2()
        {
            IPAddress ip;
            var headers = Request.Headers.ToList();
            if (headers.Exists((kvp) => kvp.Key == "X-Forwarded-For"))
            {
                // when running behind a load balancer you can expect this header
                var header = headers.First((kvp) => kvp.Key == "X-Forwarded-For").Value.ToString();
                // in case the IP contains a port, remove ':' and everything after
                ip = IPAddress.Parse(header.Remove(header.IndexOf(':')));
            }
            else
            {
                // this will always have a value (running locally in development won't have the header)
                ip = Request.HttpContext.Connection.RemoteIpAddress;
            }

            return ip.ToString();
        }

        [HttpGet("Get3")]
        public string Get3()
        {
            return Request.HttpContext.Connection.RemoteIpAddress?.MapToIPv4()?.ToString();
        }

        [HttpGet("Get4")]
        public string Get4()
        {
            string resultado = string.Empty;

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    resultado += $"{ip} |";
                }
            }

            return resultado;
        }

        [HttpGet("Get5")]
        public string Get5()
        {
            string resultado = string.Empty;

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {

                resultado += $"{ip} |";

            }

            return resultado;
        }

        [HttpGet("GetDNS")]
        public string GetDNS()
        {
            return Dns.GetHostName();
        }

    }
}