using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Security.Cryptography;
using System.Collections;

namespace Klient
{
    public partial class Klient : Form
    {
        static RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048);
        static RSAParameters privKey = csp.ExportParameters(true);
        static RSAParameters pubKey = csp.ExportParameters(false);
        string pubKeyString = GetKeyString(pubKey);
        string privKeyString = GetKeyString1(privKey);
        static string publicKeySerweraString;

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
        public static string Encrypt(string texttoencrypt, string publicKeySerweraString)
        {
            var bytesToEncrypt = Encoding.UTF8.GetBytes(texttoencrypt);

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    rsa.FromXmlString(publicKeySerweraString.ToString());
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
        Socket sock;
        public Klient()
        {
            InitializeComponent();
            sock = socket();
            FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }
        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            sock.Close();
        }
        Socket socket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        private void Klient_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string adres = "127.0.0.1";
                //próba połączenia do serwera 
                sock.Connect(new IPEndPoint(IPAddress.Parse(adres), 1234));
                przychodzaceKlient.Items.Add("Połączono");
                Console.WriteLine("klucz klienta"+pubKeyString);

                new Thread(delegate ()
                {
                    byte[] dane = Encoding.Default.GetBytes(pubKeyString);
                    sock.Send(dane, 0, dane.Length, 0);
                    byte[] bufferklucza = new byte[2555];
                    int recklucza = sock.Receive(bufferklucza, 0, bufferklucza.Length, 0);
                    Array.Resize(ref bufferklucza, recklucza);
                    string publicKeySerweraString = Encoding.Default.GetString(bufferklucza);
                    Klient.publicKeySerweraString = publicKeySerweraString;
                    try
                    {
                        while (true)
                        {
                            byte[] buffer = new byte[2555];//buffer na dane
                            int rec = sock.Receive(buffer, 0, buffer.Length, 0);
                            Array.Resize(ref buffer, rec);
                            string textToDecrypt = Encoding.Default.GetString(buffer);
                            Thread.Sleep(1000);
                            string textodebrany = Decrypt(textToDecrypt, privKey);
                            
                            Invoke((MethodInvoker)delegate
                            {
                                przychodzaceKlient.Items.Add(Encoding.Default.GetString(buffer));
                                przychodzaceKlient.Items.Add(textodebrany);
                            });
                        }
                    }
                    catch
                    {
                        Thread.Sleep(2000);
                        Application.Exit();
                        sock.Close();
                    }
                }
           ).Start();
            }
            catch
            {
                przychodzaceKlient.Items.Add("Nie można połączyć");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            byte[] dane = Encoding.Default.GetBytes(tekstKlient.Text);
            string texttoencrypt = dane.ToString();
            string textdowyslania = Encrypt(texttoencrypt, publicKeySerweraString);
            byte[] dane1 = Encoding.Default.GetBytes(textdowyslania);
            sock.Send(dane1, 0, dane1.Length, 0);
            tekstKlient.Clear();
        }
       
    }
}
