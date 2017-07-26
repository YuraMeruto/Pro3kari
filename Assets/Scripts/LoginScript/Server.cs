using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine.UI;
public class Server : MonoBehaviour {

    private Socket server;
    private Text IpText;
    private IPAddress server_ip;
    private enum Status {Server,Client };
	// Use this for initialization
	void Start () {
        server = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
        server_ip = IPAddress.Parse("10.40.0.1");
        Thread thread = new Thread(Ini);
        thread.Start();
	}
	
    void SocketStart()
    {

    }

    void Ini()
    {

    }



}
