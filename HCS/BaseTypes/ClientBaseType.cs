using HCS.Globals;
using System;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace HCS.BaseTypes {
    public abstract class ClientBaseType {
        public ClientConfig config;
        protected CustomBinding binding;
        protected EndpointAddress remoteAddress;

        public ClientBaseType(ClientConfig config) {
            this.config = config;

            binding = new CustomBinding();

            binding.Elements.Add(new TextMessageEncodingBindingElement {
                MessageVersion = MessageVersion.Soap11,
                WriteEncoding = Encoding.UTF8
            });

            if (config.UseTunnel)
            {
                if (Process.GetProcessesByName("stunnel").Any() ? false : true)
                {
                    throw new Exception("stunnel не запущен");
                }

                binding.Elements.Add(new HttpTransportBindingElement {
                    AuthenticationScheme = (config.IsPPAK ? System.Net.AuthenticationSchemes.Digest : System.Net.AuthenticationSchemes.Basic),
                    MaxReceivedMessageSize = int.MaxValue,
                    UseDefaultWebProxy = false
                });
            }
            else
            {
                binding.Elements.Add(new HttpsTransportBindingElement {
                    AuthenticationScheme = (config.IsPPAK ? System.Net.AuthenticationSchemes.Digest : System.Net.AuthenticationSchemes.Basic),
                    MaxReceivedMessageSize = int.MaxValue,
                    UseDefaultWebProxy = false,
                    RequireClientCertificate = true
                });
            }

        }

        public EndpointAddress GetEndpointAddress(string endpointName) {
            if (config.UseTunnel)
                return new EndpointAddress($"http://{Constants.Address.UriTunnel}/{endpointName}");
            else
                return (config.IsPPAK ? new EndpointAddress($"https://{Constants.Address.UriPPAK}/{endpointName}")
                    : new EndpointAddress($"https://{Constants.Address.UriSIT}/{endpointName}"));
        }
    }
}
