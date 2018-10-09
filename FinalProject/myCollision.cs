using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net;         // For IPEndPoint, Dns
using System.Net.Sockets; // For TcpClient, NetworkStream, SocketException 
using System.Threading;   // For Thread.Sleep
using System.Text;        // For Encoding

public class myCollision : MonoBehaviour
{

    public GameObject player1;
    // Use this for initialization
    public Vector3 oldPosition;
    public Vector3 newPosition;
    public float Timer1;
    public float Timer1Passed;

    public bool FALLDETECTED;

    public float Timer2;
    public float Timer2Passed;

    public bool FALLDETECTEDANDSTAY;

    public float writeFALSEFileTimer3;
    public float writeTRUEFileTimer4;
    public bool writeFALSEbool;
    public bool writeTRUEbool;

    public bool genafterArise;

    public bool oldDetectedFALSE;
    public bool newDetectedFALSE;

    public int myCounterWrite;

    public DateTime myDateTime;
    public string myDateTimeString;
    public string myOutputString;

    public bool onlyONECOMPLETEDSTM;

    void Start()
    {
        player1 = new GameObject();
        player1 = GameObject.Find("U_CharacterFront");

        oldPosition = new Vector3(2.0f, 0.0f, 0.0f);
        newPosition = new Vector3(2.0f, 0.0f, 0.0f);

        Timer1 = 0.0f;
        Timer1Passed = 6.0f; //6.0f normal

        FALLDETECTED = false;

        Timer2 = 0.0f;
        Timer2Passed = 3.0f;

        FALLDETECTEDANDSTAY = false;

        writeFALSEFileTimer3 = 0.0f;
        writeTRUEFileTimer4 = 0.0f;
        //writeFALSEbool = false;
        writeTRUEbool = false;

        genafterArise = false;

        newDetectedFALSE = false;
        oldDetectedFALSE = false;

        myCounterWrite = 1;

        myDateTime = new DateTime();
        myDateTimeString = "";
        myOutputString = "";

        onlyONECOMPLETEDSTM = false;

        //Server code...
        //sock = new Socket();
        sock = null;
        ipAd = null;
        //byteBuffer = new byte[];

        SocketTimer = 0.0f;
        SocketTimerTripTime = 0.4f; //2.0f for first true

        myDataToSend = "";

        isSocketClosed = true; //Do not run until we connect run on == false //Do not set here
        SetUpSocket();


        byteBuffer = new byte[BUFSIZE]; // Receive buffer

        //writeFALSEbool = false;
        Timer3 = 0.0f;
        trip = false;
    }

    public Socket sock;
    public byte[] byteBuffer;
    public IPAddress ipAd;
    public int servPort;
    public String server; // Server name or IP address
    public int totalBytesSent; // Total bytes sent so far 
    public int totalBytesRcvd; // Total bytes received so far
    private const int BUFSIZE = 1000/*32*/; // Size of receive buffer

    public void SetUpSocket()
    {

        //if ((args.Length < 2) || (args.Length > 3)) // Test for correct # of args 
        //    throw new ArgumentException("Parameters: <Server> <Word> [<Port>]");

        IPAddress ipAd = IPAddress.Parse(/*"10.255.254.200"*//*"10.255.254.200"*/"10.190.5.50"/*"192.168.1.102"*/);//"24.114.57.111"); //McDonalds
        //server = ipAd.ToString(); // Server name or IP address 
        //server = "10.255.254.200";

        // Convert input String to bytes //reinitialize in code
        //byte[] byteBuffer = Encoding.ASCII.GetBytes("Message Sent 001");

        // Use port argument if supplied, otherwise default to 7 
        //int servPort = (args.Length == 3) ? Int32.Parse(args[2]) : 7;
        int servPort = 7;

        // Create Socket and connect !Socket sock == null //overrides
        sock = null;
        try
        {
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                              ProtocolType.Tcp);

            sock.Connect(new IPEndPoint(/*Dns.Resolve(server).AddressList[0]*/ipAd.Address, servPort));
            //Debug.Log("*****" + Dns.Resolve(server).AddressList[0].ToString() + ": " + servPort.ToString());
        }
        catch (Exception e)
        {
            //Console.WriteLine(e.Message);
            //Environment.Exit(-1);
            Debug.Log("****Socket Failed: " + e.Message.ToString());
            Debug.Log("*****" + Dns.Resolve(server).AddressList[0].ToString() + ": " + servPort.ToString());
        }

        // Receive the same string back from the server 
        totalBytesSent = 0; // Total bytes sent so far 
        totalBytesRcvd = 0; // Total bytes received so far 

        // Make sock a non-blocking Socket
        sock.Blocking = false;



        //Data to send
        myDataToSend = "My message 002...";
        //byte[] byteBuffer = ASCIIEncoding.ASCII.GetBytes(myDataToSend);
        //check if seen by function
        byteBuffer = ASCIIEncoding.ASCII.GetBytes(myDataToSend); //check Data is stored (check)
        //string str3 = System.Text.Encoding.Default.GetString(byteBuffer); //byteBuffer is empty
        //Debug.Log("Sending data 002: " + str3.ToString());

        Debug.Log("Entered004..."); //ok
//////////AA11
        isSocketClosed = true; //call update for sending data //FALSE == SEND DATA //SET HERE
        //Debug.Log("isSocClosed:= " + isSocketClosed.ToString());
    }

    public bool isSocketClosed;

    public void CloseSocket()
    {
        
        myOutputString = "CloseSocket...";
        myDataToSend = myOutputString; //Set data to send
        totalBytesSent = 0; //reset amount of bytes sent //Once over will not run again
        isSocketClosed = false; //FALSE SEND THE DATA
        

        //Thread.Sleep(1000);
        //isSocketClosed = true;
        Debug.Log("**********Closing Socket...");
        //sock.Close();

    }

    public float SocketTimer;
    public float SocketTimerTripTime;
    string myDataToSend;

    public bool writeTCPIPBool;

    public void AttachSocket()
    {
        Debug.Log("running AttachSocket...");
        //string str2 = System.Text.Encoding.Default.GetString(byteBuffer);
        //Debug.Log("Sending data: " + str2.ToString());

        //SocketTimer += Time.deltaTime;

        //Debug.Log("Time: = " + SocketTimer.ToString() + SocketTimerTripTime.ToString());
        //if (SocketTimer > /*SocketTimerTripTime*/ 0.01f) //every time interval
        //{
        Debug.Log("Writing Data...");
        //if (writeFALSEbool == false)
        //{
            //writeFALSEbool = true;
            //Process //get data here
            //Debug.Log("Process: " + byteBuffer.ToString());
            byteBuffer = ASCIIEncoding.ASCII.GetBytes(myDataToSend);
            //string str = System.Text.Encoding.Default.GetString(byteBuffer);
            //Debug.Log("Sending data 001: " + str.ToString() + ": " + myDataToSend.ToString());
            //Data is good********;
            //here
            // Send the encoded string to the server 
            while (totalBytesSent < byteBuffer.Length) //theory 1 //18 //Fixed here
            {

                // Send the encoded string to the server 
                if (totalBytesSent < byteBuffer.Length)
                {
                    try
                    {
                        /*if(sock == null)
                        {
                            Debug.Log("Socket == null 001"); //null
                        }else
                        {
                            Debug.Log("Socket == valid 002 "); //no longer null
                        }*/ //sends the data

                        totalBytesSent += sock.Send(byteBuffer, totalBytesSent,
                                                    byteBuffer.Length - totalBytesSent,
                                                    SocketFlags.None);
                        //Console.WriteLine("Sent a total of {0} bytes to server...", totalBytesSent);
                        Debug.Log("Sent a total of bytes to server:= " + totalBytesSent.ToString());
                    }
                    catch (SocketException se)
                    {
                        Debug.Log("ErrorCode 0005");
                        if (se.ErrorCode == 10035)
                        { // WSAEWOULDBLOCK: Resource temporarily unavailable
                          //Console.WriteLine("Temporarily unable to send, will retry again later.");
                            Debug.Log("Temporarily unable to send, will retry again later.");
                        }
                        else
                        {
                            //Console.WriteLine(se.ErrorCode + ": " + se.Message);
                            Debug.Log("***************Error: " + se.ErrorCode.ToString() + ": " + se.Message.ToString());
                            sock.Close();
                            //Environment.Exit(se.ErrorCode);
                        }
                    } //end try
                } //end if

                //}

            } //end while
            //End of Process
            //SocketTimer = 0.0f;
        //}


    }

    public void GetString()
    {

    }

    // Update is called once per frame
    public float Timer3;
    public bool trip;
    void Update()
    {
        ///added here
        //Debug.Log("Made it here...");
        //RUNNING

        Timer3 += Time.deltaTime;
        Debug.Log("Made it here... " + Timer3.ToString());

        if(Timer3 > 2.0f)
        {
            trip = true;
        }
        
        if (Timer3 < 2.0f && trip == false) //0.04 works but Timer stops
        {
            //FALLDETECTED = false;
            //trip = true;
            //myOutputString = "Sending Confirm...";
            //myDataToSend = myOutputString; //Set data to send
            //totalBytesSent = 0; //reset amount of bytes sent //Once over will not run again
            //isSocketClosed = false; //FALSE SEND THE DATA
            //Send data out
            Debug.Log("Entered Timer3 ****...");
        }

        /*
         * 
         * ///////////////
           //AA1, false          
           myDataToSend = myOutputString; //Set data to send
           totalBytesSent = 0; //reset amount of bytes sent //Once over will not run again
           isSocketClosed = false; //FALSE SEND THE DATA, Runs once
           //////////////
         * */

        newPosition = player1.transform.position; //Get new position
        if (newPosition == oldPosition) //Check old and new frame
        {
            //The position has not changed
            Timer1 += Time.deltaTime; //Add time
//******** Moved to after detect
            //genafterArise = true;
        }
        else
        {
            Debug.Log("We have detected movement...");
            Timer1 = 0.0f;
            //FALLDETECTED = false; //Error here
            onlyONECOMPLETEDSTM = false;
//******** Updated here
            genafterArise = true;
        }
        oldPosition = newPosition;

        if (Timer1 >= Timer1Passed) //No more movement
        {
            Debug.Log("Timer has passed...");
            FALLDETECTED = false;
            FALLDETECTEDANDSTAY = false;
            genafterArise = false;
            //Write Close File
            if (onlyONECOMPLETEDSTM == false)
            {
                StreamWriter sw = File.AppendText("c:\\unitywritetext\\file.txt");
                myOutputString = "Completed";
                sw.WriteLine(myOutputString);
                sw.Close();
                //new code here
                CloseSocket(); //only run once

                onlyONECOMPLETEDSTM = true;

            }
            myCounterWrite = 1;


        }
        //unitywritetext :: folder

        if (Timer1 > 60000.0f) //Reset Timer if high
            Timer1 = 0.0f;

        if (Timer2 > 60000.0f) //Reset Timer if high
            Timer2 = 0.0f;

        /*newDetectedFALSE = FALLDETECTED;
        if (newDetectedFALSE == oldDetectedFALSE)
        {
            genafterArise = 
        }*/

        //Write False to file every x seconds
        writeFALSEFileTimer3 += Time.deltaTime;
        writeTRUEFileTimer4 += Time.deltaTime;

        if (genafterArise == true)
        {
            Debug.Log("************************************** " + FALLDETECTED.ToString());
            if (FALLDETECTED == false && writeFALSEFileTimer3 >= 2.0f)
            {
                Debug.Log("Updating false == " + FALLDETECTED.ToString());
                writeFALSEFileTimer3 = 0.0f;
                ////////////////////// Write File //////////////
                myDateTimeString = System.DateTime.Now.ToString("MM/dd/yyyy") + ":" + System.DateTime.Now.ToString("hh:mm:ss") + System.DateTime.Now.Millisecond;
                myOutputString = myCounterWrite.ToString() + " " + myDateTimeString + ", " + FALLDETECTED.ToString();
                Debug.Log(myOutputString);
                StreamWriter sw = File.AppendText("c:\\unitywritetext\\file.txt");
                sw.WriteLine(myOutputString);
                sw.Close();

                myCounterWrite += 1;
                ////////////////////// End Write File //////////////
                ////////////////////// Send Data Here //////////////
                //AA1
                myDataToSend = myOutputString; //Set data to send
                totalBytesSent = 0; //reset amount of bytes sent //Once over will not run again
                isSocketClosed = false; //FALSE SEND THE DATA
                ////////////////////// End of send Data ////////////

            }
            //Write True to file every x seconds

            if (FALLDETECTED == true && FALLDETECTEDANDSTAY == false && writeTRUEFileTimer4 >= 0.4f)
            {
                Debug.Log("Write FALL == true and Stay on ground == false");
                writeTRUEFileTimer4 = 0.0f;

                ////////////////////// Write File //////////////
                myDateTimeString = System.DateTime.Now.ToString("MM/dd/yyyy") + ":" + System.DateTime.Now.ToString("hh:mm:ss") + System.DateTime.Now.Millisecond;
                myOutputString = myCounterWrite.ToString() + " " + myDateTimeString + ", " + FALLDETECTED.ToString();
                Debug.Log(myOutputString);
                StreamWriter sw = File.AppendText("c:\\unitywritetext\\file.txt");
                sw.WriteLine(myOutputString);
                sw.Close();

                myCounterWrite += 1;
                ////////////////////// End Write File //////////////
                ////////////////////// Send Data Here //////////////
                //AA2
                myDataToSend = myOutputString; //Set data to send
                totalBytesSent = 0; //reset amount of bytes sent //Once over will not run again
                isSocketClosed = false; //FALSE SEND THE DATA
                ////////////////////// End of send Data ////////////
               
            }

            if (FALLDETECTED == true && FALLDETECTEDANDSTAY == true && writeTRUEFileTimer4 >= 0.4f)
            {
                Debug.Log("Write FALL == true and Stay on ground == true");
                writeTRUEFileTimer4 = 0.0f;

                ////////////////////// Write File //////////////
                myDateTimeString = System.DateTime.Now.ToString("MM/dd/yyyy") + ":" + System.DateTime.Now.ToString("hh:mm:ss") + System.DateTime.Now.Millisecond;
                myOutputString = myCounterWrite.ToString() + " " + myDateTimeString + ", " + FALLDETECTED.ToString() + ", " + FALLDETECTEDANDSTAY.ToString();
                Debug.Log(myOutputString);
                StreamWriter sw = File.AppendText("c:\\unitywritetext\\file.txt");
                sw.WriteLine(myOutputString);
                sw.Close();

                myCounterWrite += 1;
                ////////////////////// End Write File //////////////
                ////////////////////// Send Data Here //////////////
                //AA3
                myDataToSend = myOutputString; //Set data to send
                totalBytesSent = 0; //reset amount of bytes sent //Once over will not run again
                isSocketClosed = false; //FALSE SEND THE DATA
                ////////////////////// End of send Data ////////////
            }
        }

        if (writeFALSEFileTimer3 > 60000.0f) //Reset Timer if high
            writeFALSEFileTimer3 = 0.0f;

        if (writeTRUEFileTimer4 > 60000.0f) //Reset Timer if high
            writeTRUEFileTimer4 = 0.0f;

        if (myCounterWrite >= 20000)
        {
            myCounterWrite = 1;
        }

        //Debug.Log("aaa3:= " + isSocketClosed.ToString()); //True
        if (isSocketClosed == false)
        {
            isSocketClosed = true; //Only run once: send data once
            AttachSocket();
            //AS1();
        }
        //End of Update
    }

    public void AS1()
    {
        Debug.Log("Enter003...");
        string str2 = System.Text.Encoding.Default.GetString(byteBuffer); //byteBuffer is empty
        Debug.Log("Sending data: " + str2.ToString());
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Collision Detected");
        //if(collider.gameObject.tag == "CannonBall")
        if (collider.gameObject.name == "my_HEAD_Cube")
        {
            Debug.Log("***Fall Detected");
            FALLDETECTED = true;
            Debug.Log("**********************************************************************************");
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.name == "my_HEAD_Cube")
        {
            Timer2 += Time.deltaTime;
            if (Timer2 >= Timer2Passed)
            {
                Debug.Log("***Emergency... Patient has fallen and has been on the ground for more than: " + Timer2.ToString() + " seconds...");
                FALLDETECTEDANDSTAY = true;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.Log("Exit Collider...");
        if (collider.gameObject.name == "my_HEAD_Cube")
        {
            if (FALLDETECTED = true && FALLDETECTEDANDSTAY == true)
            {
                Debug.Log("***The patient has risen and was on the ground for: " + Timer2.ToString() + " seconds...");
            }
            if (FALLDETECTED = true && FALLDETECTEDANDSTAY == false)
            {
                FALLDETECTED = false;
                Debug.Log("***The patient has risen and has been on the ground for less than " + Timer2.ToString() + " seconds...");
            }
            Timer2 = 0.0f;

        }
    }
}