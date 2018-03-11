using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Xml;
using Xades.Implementations;
using HCS.Globals;

namespace HCS
{
    internal class MessageInspectorBehavior : IClientMessageInspector {
        public object BeforeSendRequest(ref Message request, IClientChannel channel) {
            try
            {
                FixHeaderForDebug(ref request);
                var messageRef = MessageString(ref request);

                if (!messageRef.Contains(Constants.SignElementId)) return null;

                var service = new GostXadesBesService(ServicePointConfig.CryptoProviderType);

                var signedXml = service.Sign(messageRef,
                    Constants.SignElementId,
                    ServicePointConfig.CertificateThumbPrint,
                    ServicePointConfig.PrivateKeyPassword);
                request = CreateMessageFromString(signedXml, request.Version);
            }
            catch (Exception ex)
            {
                string error = $"В {this.GetType().Name} произошло исключение: {ex.GetBaseException().Message}";
                Console.WriteLine(error);
            }

            return null;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState) {
        }

        private void FixHeaderForDebug(ref Message request) {
            int limit = request.Headers.Count;
            for (int i = 0; i < limit; ++i)
            {
                if (request.Headers[i].Name.Equals("VsDebuggerCausalityData"))
                {
                    request.Headers.RemoveAt(i);
                    break;
                }
            }
        }

        String MessageString(ref Message m) {
            MessageBuffer mb = m.CreateBufferedCopy(int.MaxValue);

            m = mb.CreateMessage();

            Stream s = new MemoryStream();
            XmlWriter xw = XmlWriter.Create(s);
            mb.CreateMessage().WriteMessage(xw);
            xw.Flush();
            s.Position = 0;

            byte[] bXML = new byte[s.Length];
            s.Read(bXML, 0, (int)s.Length);

            if (bXML[0] != (byte)'<')
            {
                return Encoding.UTF8.GetString(bXML, 3, bXML.Length - 3);
            }
            else
            {
                return Encoding.UTF8.GetString(bXML, 0, bXML.Length);
            }
        }

        XmlReader XmlReaderFromString(String xml) {
            var stream = new MemoryStream();
            var writer = new System.IO.StreamWriter(stream);
            writer.Write(xml);
            writer.Flush();
            stream.Position = 0;
            return XmlReader.Create(stream);
        }

        Message CreateMessageFromString(String xml, MessageVersion ver) {
            return Message.CreateMessage(XmlReaderFromString(xml), int.MaxValue, ver);
        }


    }
    public class MyEndpointBehavior : IEndpointBehavior {
        public void Validate(ServiceEndpoint endpoint) {
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher) {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime) {
            clientRuntime.MessageInspectors.Add(new MessageInspectorBehavior());
        }
    }

    public class MyExtensionElement : BehaviorExtensionElement {
        public override Type BehaviorType {
            get { return typeof(MyEndpointBehavior); }
        }

        protected override object CreateBehavior() {
            return new MyEndpointBehavior();
        }
    }
}
