using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Security.Cryptography;

namespace Serwer
{
    public partial class Serwer : Form
    {
        static RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048);
        static RSAParameters privKey = csp.ExportParameters(true);
        static RSAParameters pubKey = csp.ExportParameters(false);
        string pubKeyString = GetKeyString(pubKey);
        static string publicKeyKlientaString;
        string privKeyString = GetKeyString1(privKey);

        static private string GetKeyString(RSAParameters publicKey)
        {

            var stringWriter = new System.IO.StringWriter();
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            xmlSerializer.Serialize(stringWriter, publicKey);
            return stringWriter.ToString();
        }
        static private string GetKeyString1(RSAParameters privKey)
        {

            var stringWriter = new System.IO.StringWriter();
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            xmlSerializer.Serialize(stringWriter, privKey);
            return stringWriter.ToString();
        }
        public static string Encrypt(string texttoencrypt, string publicKeyKlientaString)
        {
            var bytesToEncrypt = Encoding.UTF8.GetBytes(texttoencrypt);

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    rsa.FromXmlString(publicKeyKlientaString.ToString());
                    var encryptedData = rsa.Encrypt(bytesToEncrypt, true);
                    var base64Encrypted = Convert.ToBase64String(encryptedData);
                    return base64Encrypted;
                    
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
        public static string Decrypt(string textToDecrypt, RSAParameters privKey)
        {
            var resultBytes = Convert.FromBase64String(textToDecrypt);
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    rsa.ImportParameters(privKey);

                    byte[] decryptedBytes = rsa.Decrypt(resultBytes, true);
                    var decryptedData = System.Text.Encoding.Default.GetString(decryptedBytes);
                    return decryptedData;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
        Socket sock, odbieranie;
        public Serwer() 
        {
            InitializeComponent();
        }

        Socket socket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private void button2_Click(object sender, EventArgs e)
        {
        
            byte[] buffer = Encoding.Default.GetBytes(tekstSerwer.Text);
            string texttoencrypt = buffer.ToString();
            string textdowyslania = Encrypt(texttoencrypt, publicKeyKlientaString);
            byte[] buffer1 = Encoding.Default.GetBytes(textdowyslania);
            odbieranie.Send(buffer1, 0, buffer1.Length, 0);
            tekstSerwer.Clear();
        }
       
        

        private void button1_Click(object sender, EventArgs e)
        {
            sock = socket();
            sock.Bind(new IPEndPoint(0, 1234));
            przychodzaceSerwer.Items.Add("Rozpoczęto nasłuchiwanie");
            sock.Listen(0);
            
            
            new Thread(delegate()
            {
                odbieranie = sock.Accept();
                byte[] dane = Encoding.Default.GetBytes(pubKeyString);
                odbieranie.Send(dane, 0, dane.Length, 0);
                byte[] bufferklucza = new byte[2555];
                int recklucza = odbieranie.Receive(bufferklucza, 0, bufferklucza.Length, 0);
                Array.Resize(ref bufferklucza, recklucza);
                string publicKeyKlientaString = Encoding.Default.GetString(bufferklucza);
                Serwer.publicKeyKlientaString = publicKeyKlientaString;

                sock.Close();
               
                try
                {
                    while (true)
                    {
                        byte[] buffer = new byte[2555];
                        int rec = odbieranie.Receive(buffer, 0, buffer.Length, 0);
                        Array.Resize(ref buffer, rec);
                        string textToDecrypt = Encoding.Default.GetString(buffer);
                        Thread.Sleep(1000);
                        string textodebrany = Decrypt(textToDecrypt, privKey);
                        Invoke((MethodInvoker)delegate()
                        {
                            przychodzaceSerwer.Items.Add(Encoding.Default.GetString(buffer));
                            przychodzaceSerwer.Items.Add(textodebrany);
                        });
                    }
                }
                catch {
                    Thread.Sleep(2000);
                    Application.Exit();
                }
            }
           ).Start();
        }

    }
}
