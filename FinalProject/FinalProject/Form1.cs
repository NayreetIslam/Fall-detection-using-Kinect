using System; // For Console, Int32, ArgumentException, Environment
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using System.Net;          // For IPAddress
using System.Net.Sockets;  // For TcpListener, TcpClient
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace FinalProject
{
    public partial class Form1 : Form
    {
        public bool runonce;
        public int rmode;

        public Form1()
        {
            InitializeComponent();
            runonce = true;
            rmode = 0;

        }

        private void b1_Click(object sender, EventArgs e)
        {
            //t1.Text="Yo Yo";
           /* String fileName = "C:/Users/100631155/Documents/Visual Studio 2010/Projects/project\\file.txt";
            if (System.IO.File.Exists(fileName))
            {
                //MessageBox.Show("File Exists");
                String[] lines = System.IO.File.ReadAllLines(fileName);
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    string[] words = line.Split(' ');
                    if (words[2] == "True")
                    {
                        MessageBox.Show("The user is colliding");
                    }
                    if (words[2] == "True," && words[3] == "True")
                    {
                        t1.Text = "Fall";
                        MessageBox.Show("The user has been colliding for more than x seconds");

                    }
                    /*else
                    {
                        t1.Text="Okay";
                    }
                    //Do something

                }

            }
            else
            {
                MessageBox.Show("No Such File");
            }*/
            Timer2_Run(sender, e);
        }

        public int servPort;
        public Socket server;
        public Socket client;
        private const int BUFSIZE = 1000/*32*/; // Size of receive buffer
        private const int BACKLOG = 5;  // Outstanding connection queue max size
        public byte[] rcvBuffer;
        public int bytesRcvd;
        public int totalBytesEchoed;


        public void SetupServerConnect()
        {
            //MessageBox.Show("Timer has run..." + " " + runonce.ToString());
            //int servPort = (args.Length == 1) ? Int32.Parse(args[0]) : 7;
            servPort = 7;

            //Socket server = null;
            server = null;

            try
            {
                // Create a socket to accept client connections
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                                    ProtocolType.Tcp);

                server.Bind(new IPEndPoint(IPAddress.Any, servPort));

                server.Listen(BACKLOG);
            }
            catch (SocketException se)
            {
                //Console.WriteLine(se.ErrorCode + ": " + se.Message);
                MessageBox.Show(se.ErrorCode + ": " + se.Message);
                //Environment.Exit(se.ErrorCode);
            }

            //check001
            rcvBuffer = new byte[BUFSIZE]; // Receive buffer
            bytesRcvd = 0;
            //blhere
            //client.Blocking = false; == null does not work


        }
        public void TimerOn()
        {

            //timer1.Interval = (1000) * (1);              // Timer will tick evert second
            timer1.Interval = (400) * (1);
            timer1.Tick += new EventHandler(Timer2_Run); // Everytime timer ticks, timer_Tick will be called
            timer1.Enabled = true; // Enable the timer
            //timer1.Start(); // Start the timer

        }

        public void WriteSTRtoFile(string myOutputString)
        {
            StreamWriter sw = File.AppendText("FileDetectingFall.txt");
            sw.WriteLine(myOutputString);
            sw.Close();
        }

        public bool ConnectAccept()
        {

            client = null;
            //orhere
            try
            {
                //MessageBox.Show("Here");
                //client = server.Accept(); // Get client connection

                client = server.Accept();
                //MessageBox.Show("Here2");
                //blocking here
                if (client != null)
                {

                    //MessageBox.Show("Handling client at " + client.RemoteEndPoint + " - ");
                    //Console.Write("Handling client at " + client.RemoteEndPoint + " - ");

                    // Receive until client closes connection, indicated by 0 return value
                    totalBytesEchoed = 0;
                    //totalBytesEchoed = 0;

                    client.Blocking = false;
                    //client.EnableBroadcast = false;
                    // client.
                    //client.
                    //rmode = 2;
                    //counter = 0;
                    TimerOn();


                    return true;
                }

            }
            catch (Exception e2)
            {
                MessageBox.Show("Error in Accept..." + e2.ToString());
                //Console.WriteLine(e2.Message);
                client.Close();
                //return false;
            }
            return false;
        }

        public int counter;

        //******************Error Here
        public void Timer2_Run(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (rmode == 0) //Get the data
            {
                //use while not known
                //MessageBox.Show("Made it here");
                //MessageBox.Show("Data: "/*rcvBuffer.ToString()*/);
                //counter++;

                //Loops through star star
                try
                {
                    while ((bytesRcvd = client.Receive(rcvBuffer, 0, rcvBuffer.Length,
                                                       SocketFlags.None)) > 0)
                    {

                        rmode = 1;
                        //client.Send(rcvBuffer, 0, bytesRcvd, SocketFlags.None);
                        totalBytesEchoed += bytesRcvd;
                    }
                }
                catch (SocketException t)
                {
                    //A non blocking socket did not complete //error
                    //MessageBox.Show("Error 0001:= " + t.ErrorCode + " " + t.Message);
                }
                if (rmode == 1) //Process the data
                {
                    if (getlight() < 25)
                    {
                        System.Media.SoundPlayer player = new System.Media.SoundPlayer();
                        player.SoundLocation = "light.wav";
                        //player.Play();
                    }
                    string str2 = System.Text.Encoding.Default.GetString(rcvBuffer); //byteBuffer is empty

                    if (str2.Substring(0, 6) == "CloseS") //(matches now) 
                    {
                        //End of app
                        //MessageBox.Show("Closing******* Socket...");
                        //client.Close();
                        rmode = 1; //Next frame will close socket
                    }
                    else //Write the data to a file and reinitialize data
                    {
                        //Debug.Log("Sending data: " + str2.ToString());
///////********************* Uncomment here
                        //MessageBox.Show("Data2: " + str2.ToString()/*rcvBuffer.ToString()*/);

                        Array.Clear(rcvBuffer, 0, rcvBuffer.Length);
                        bytesRcvd = 0;
                        rmode = 0;
                        ////////////Write Data To File ///////////////
                        WriteSTRtoFile(str2);
                        /////////// End Write Data To File ////////////
                        /////////// Nayreet Process Your Data Here using str2 as incoming data ////////////
                        ProcessData(str2);
                        ////////// End of Nayreet Process /////////////////////////////////////////////////
                    }
                }
                //rmode = 1;



            }
            else if (rmode == 1) //Close the socket
            {
                client.Close();
                //MessageBox.Show("Closing******* Socket...");
                //rmode = 2;
                //MessageBox.Show("Made it here");
                rmode = 2; //No more processing of TCPIP data
            }
            timer1.Enabled = true;
        }
        public void ProcessData(String data)
        {

            
                string[] words = data.Split(' ');
                //MessageBox.Show(words[2]);
                if (words[2].Contains("True") )
                {
                    Console.WriteLine("**************"+words[2]);
                    t1.Text = "Colliding";
                    try
                    {
                        if (words[3].Contains("True"))
                        {
                            t1.Text = "Fall";
                            Console.WriteLine("**************" + words[3]);
                            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
                            player.SoundLocation = "alarm.wav";
                            player.Play();
                        }
                    }
                    catch (Exception e)
                    {

                    }

                }
                
                /*else
                {
                    t1.Text="Okay";
                }*/
                //Do something

            
        }
 
        private void Form1_Load(object sender, EventArgs e)
        {
            //runonce = false;
            //timer1.Tick += new EventHandler(Timer1_Run); // Everytime timer ticks, timer_Tick will be called
            //timer1.Interval = (1000) * (1);              // Timer will tick evert second
            //timer1.Enabled = true;                       // Enable the timer
            //timer1.Start();                              // Start the timer
            //WinTimer = new System.Windows.Forms.Timer();
            SetupServerConnect();
            ConnectAccept();
        }

        private void l1_Click(object sender, EventArgs e)
        {
            //client.Close();
        }


        public System.Windows.Forms.Timer WinTimer;// = new System.Windows.Forms.Timer();

        public int getlight()
        {
            Random rnd = new Random();

            return (rnd.Next(20, 40));
        }
    }
}
