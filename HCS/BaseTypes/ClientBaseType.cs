using HCS.Globals;
using System;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace HCS.BaseTypes {
    public abstract class ClientBaseType {
        public ClientConfig _config;
        protected CustomBinding _binding;
        protected EndpointAddress remoteAddress;

        public ClientBaseType(ClientConfig config) {
            _config = config;

            _binding = new CustomBinding();

            _binding.Elements.Add(new TextMessageEncodingBindingElement {
                MessageVersion = MessageVersion.Soap11,
                WriteEncoding = Encoding.UTF8
            });

            if (config.UseTunnel)
            {
                if (Process.GetProcessesByName("stunnel").Any() ? false : true)
                {
                    throw new Exception("stunnel не запущен");
                }

                _binding.Elements.Add(new HttpTransportBindingElement {
                    AuthenticationScheme = (config.IsPPAK ? System.Net.AuthenticationSchemes.Digest : System.Net.AuthenticationSchemes.Basic),
                    MaxReceivedMessageSize = int.MaxValue,
                    UseDefaultWebProxy = false
                });
            }
            else
            {
                _binding.Elements.Add(new HttpsTransportBindingElement {
                    AuthenticationScheme = (config.IsPPAK ? System.Net.AuthenticationSchemes.Digest : System.Net.AuthenticationSchemes.Basic),
                    MaxReceivedMessageSize = int.MaxValue,
                    UseDefaultWebProxy = false,
                    RequireClientCertificate = true
                });
            }

        }

        public EndpointAddress GetEndpointAddress(string endpointName) {
            if (_config.UseTunnel)
                return new EndpointAddress($"http://{Constants.Address.UriTunnel}/{endpointName}");

            return (_config.IsPPAK ? new EndpointAddress($"https://{Constants.Address.UriPPAK}/{endpointName}")
                    : new EndpointAddress($"https://{Constants.Address.UriSIT}/{endpointName}"));
        }
    }
}
