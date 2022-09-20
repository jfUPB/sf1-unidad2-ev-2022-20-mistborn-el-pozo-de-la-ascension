using UnityEngine;
using System.IO.Ports;
using System.Threading;
using TMPro;
using UnityEngine.UI;

enum TaskState
{
    INIT,
    WAIT_COMMANDS
}

public class Serial : MonoBehaviour
{
    private static TaskState taskState = TaskState.INIT;
    private SerialPort _serialPort;
    private byte[] buffer;
    public TextMeshProUGUI txt1;
    public TextMeshProUGUI txt2;
    public TextMeshProUGUI txt3;
    //private int counter = 0;
    private string input = "";
    private string input2 = "";

    void Start()
    {
        _serialPort = new SerialPort();
        _serialPort.PortName = "COM10";
        _serialPort.BaudRate = 9600;
        _serialPort.DtrEnable = true;
        _serialPort.NewLine = "\n";
        _serialPort.Open();
        Debug.Log("Open Serial Port");
        buffer = new byte[128];
    }

    void Update()
    {
        switch (taskState)
        {
            case TaskState.INIT:
                taskState = TaskState.WAIT_COMMANDS;
                Debug.Log("WAIT COMMANDS");
                break;
            case TaskState.WAIT_COMMANDS:
                
                if (_serialPort.BytesToRead > 0)
                {
                    string response = _serialPort.ReadLine();
                    Debug.Log(response);

                    if (response == "OFFOFFOFF")
                    {
                        txt1.text = "RELEASED";
                        txt2.text = "RELEASED";
                        txt3.text = "RELEASED";
                        
                    }
                    else if (response == "OFFOFFON")
                    {
                        txt1.text = "RELEASED";
                        txt2.text = "RELEASED";
                        txt3.text = "PUSHED";
                    }
                    else if (response == "OFFONOFF")
                    {
                        txt1.text = "RELEASED";
                        txt2.text = "PUSHED";
                        txt3.text = "RELEASED";
                    }
                    else if (response == "OFFONON")
                    {
                        txt1.text = "RELEASED";
                        txt2.text = "PUSHED";
                        txt3.text = "PUSHED";
                    }
                    else if (response == "ONOFFOFF")
                    {
                        txt1.text = "PUSHED";
                        txt2.text = "RELEASED";
                        txt3.text = "RELEASED";
                    }
                    else if (response == "ONONOFF")
                    {
                        txt1.text = "PUSHED";
                        txt2.text = "PUSHED";
                        txt3.text = "RELEASED";
                    }
                    else if (response == "ONONON")
                    {
                        txt1.text = "PUSHED";
                        txt2.text = "PUSHED";
                        txt3.text = "PUSHED";
                    }
                    else if (response == "ONOFFON")
                    {
                        txt1.text = "PUSHED";
                        txt2.text = "RELEASED";
                        txt3.text = "PUSHED";
                    }
                    /*
                    if (response == "OFF")
                    {
                        txt1.text = "RELEASED";
                        _serialPort.Write("readNext\n");
                        
                        if (response == "OFF")
                        {
                            txt2.text = "RELEASED";
                            _serialPort.Write("readNext2\n");
                            if (response == "OFF")
                            {
                                txt3.text = "RELEASED";
                            } 
                            else if (response == "ON")
                            {
                                txt3.text = "PUSHED";
                            }
                        }
                        else if (response == "ON")
                        {
                            txt2.text = "PUSHED";
                            _serialPort.Write("readNext2\n");
                            if (response == "OFF")
                            {
                                txt3.text = "RELEASED";
                            } 
                            else if (response == "ON")
                            {
                                txt3.text = "PUSHED";
                            }
                        }
                        
                    }
                    else if (response == "ON")
                    {
                        txt1.text = "PUSHED";
                        _serialPort.Write("readNext\n");
                        if (response == "OFF")
                        {
                            txt2.text = "RELEASED";
                            _serialPort.Write("readNext2\n");
                            if (response == "OFF")
                            {
                                txt3.text = "RELEASED";
                            } 
                            else if (response == "ON")
                            {
                                txt3.text = "PUSHED";
                            }
                        }
                        else if (response == "ON")
                        {
                            txt2.text = "PUSHED";
                            _serialPort.Write("readNext2\n");
                            if (response == "OFF")
                            {
                                txt3.text = "RELEASED";
                            } 
                            else if (response == "ON")
                            {
                                txt3.text = "PUSHED";
                            }
                        }
                    }*/
                }

                break;
            default:
                Debug.Log("State Error");
                break;
        }
    }

    public void btn1Event()
    {
        switch (input)
        {
            case "1":
                Debug.Log("En el case 1");
                if (input2 == "ON" || input2 == "on")
                {
                    _serialPort.Write("led1ON\n");
                }
                else if (input2 == "OFF" || input2 == "off")
                {
                    _serialPort.Write("led1OFF\n");
                }

                break;
            
            case "2":
                if (input2 == "ON" || input2 == "on")
                {
                    _serialPort.Write("led2ON\n");
                }
                else if (input2 == "OFF" || input2 == "off")
                {
                    _serialPort.Write("led2OFF\n");
                }

                break;
            
            case "3":
                if (input2 == "ON" || input2 == "on")
                {
                    _serialPort.Write("led3ON\n");
                }
                else if (input2 == "OFF" || input2 == "off")
                {
                    _serialPort.Write("led3OFF\n");
                }

                break;
            
            default:
                break;
        }
        _serialPort.Write("ledON\n");
        Debug.Log("entra btn 1");
    }
    
    public void btn2Event()
    {
        _serialPort.Write("readBUTTONS\n");
        Debug.Log("Send readBUTTONS");
    }

    public void readStringInput(string txt)
    {
        input = txt;
        Debug.Log(input);
    }
    
    public void readStringInput2(string txt)
    {
        input2 = txt;
        Debug.Log(input2);
    }
}
