﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sender
{
    class SerialReader
    {
        private double textOut;
        private Mutex bciDataLock;
        private SerialPort serialPort1;
        private int rate;

        //Scale for OpenBCI data to mV (highest setting)
        private static double scale = 0.02235;

        public SerialReader()
        {
            bciDataLock = new Mutex();
            serialPort1 = new SerialPort("COM5", 115200);
            serialPort1.Open();
            serialPort1.Write("s");

            setRate(250);
        }

        //Set rate in Hz
        public void setRate(double desiredRate) { rate = (int)(desiredRate * 255 / 250); }

        //Starts board output
        public void Start() { serialPort1.Write("b"); }

        //Stops board output
        public void Stop() { serialPort1.Write("s"); }

        //Reads board output
        public void Read()
        {
            Start();
            Thread dataReader = new Thread(new ThreadStart(getData));
            dataReader.Name = "OpenBCI Serial Reader";
            dataReader.Start();
        }

        //Get current serial data
        public double GetData()
        {
            bciDataLock.WaitOne();
            double returnData = textOut;
            bciDataLock.ReleaseMutex();
            return returnData;
        }

        //Private function to read from serial
        private void getData()
        {
            var inData = new Byte[32];
            while (true)
            {
                try
                {
                    bciDataLock.WaitOne();
                }
                catch (AbandonedMutexException e)
                {
                    bciDataLock.ReleaseMutex();
                }
                if (serialPort1.ReadByte() == 0xA0)
                {
                    serialPort1.Read(inData, 0, 32);
                    if (inData[31] > 0xBF && inData[31] < 0xD0 && inData[0] <= rate)
                    {
                        int outVal = interpret24bitAsInt32(inData[1], inData[2], inData[3]);
                        textOut = outVal * scale;
                    }
                }
                bciDataLock.ReleaseMutex();
            }
        }

        //Provided by OpenBCI
        public int interpret24bitAsInt32(byte byte1, byte byte2, byte byte3)
        {
            int newInt = (
                ((0xFF & byte1) << 16) |
                ((0xFF & byte2) << 8) |
                (0xFF & byte3)
              );
            if ((newInt & 0x00800000) > 0)
            {
                newInt = (int)((uint)newInt | (uint)0xFF000000);
            }
            else
            {
                newInt = (int)((uint)newInt & (uint)0x00FFFFFF);
            }
            return (newInt);
        }

    }
}
