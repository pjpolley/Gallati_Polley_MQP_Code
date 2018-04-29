using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Accord;
using Newtonsoft.Json.Linq;

namespace Sender
{
    class SerialReader
    {
        private volatile double[] dataOut;
        private volatile double[] betaDataOut;
        private volatile List<double[]> lastDataOut;
        private Mutex bciDataLock;
        private SerialPort serialPort1;
        private int rate;

        //Scale for OpenBCI data to mV (highest setting)
        private static float scale = 0.02235f;

        //Previous data for filter
        static double[,] prev_x_notch = new double[8, 5];
        static double[,] prev_y_notch = new double[8, 5];
        static double[,] prev_x_standard = new double[8, 5];
        static double[,] prev_y_standard = new double[8, 5];

        static double[,] prev_x_notchBeta = new double[8, 5];
        static double[,] prev_y_notchBeta = new double[8, 5];
        static double[,] prev_x_standardBeta = new double[8, 5];
        static double[,] prev_y_standardBeta = new double[8, 5];

        public SerialReader()
        {
            bciDataLock = new Mutex();
            //serialPort1 = new SerialPort("COM3", 115200);
            //serialPort1.Open();
            //serialPort1.Write("s");
            //serialPort1.Write("~5");
            dataOut = new double[16];
            lastDataOut = new List<double[]>();

            setRate(250);
        }

        public int getRate()
        {
            return this.rate;
        }

        //Set rate in Hz
        public void setRate(double desiredRate) { rate = (int)(desiredRate * 255 / 250); }

        //Starts board output
        public void Start() { /*serialPort1.Write("b");*/ }

        //Stops board output
        public void Stop() { /*serialPort1.Write("s");*/ }

        //Reads board output
        public void Read()
        {
            Start();

            Task dataReader = new Task(getData);
            dataReader.Start();
        }

        public double[] GetData()
        {
            bciDataLock.WaitOne();
            double[] returnData = dataOut;
            //var notGood = true;
            //while (notGood)
            //{
            //    notGood = false;
            //    foreach (var nodeReading in returnData)
            //    {
            //        if (double.IsNaN(nodeReading))
            //        {
            //            notGood = true;
            //            bciDataLock.ReleaseMutex();
            //            bciDataLock.WaitOne();
            //            returnData = dataOut;
            //            bciDataLock.ReleaseMutex();
            //        }
            //    }

            //}
            bciDataLock.ReleaseMutex();
            return returnData;
            //if (Array.Exists(returnData, input => double.IsNaN(input)))
            //{
            //    bciDataLock.ReleaseMutex();
            //    while (Array.Exists(returnData, input => double.IsNaN(input)))
            //    {
                    
            //    }
            //}
            //else
            //{
                
            //}
            //return returnData;
        }

        private void getData() 
        {
            var inData = new Byte[32];
            bool frequencyToggle = true;
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

                /*
                    if (serialPort1.ReadByte() == 0xA0)
                    {
                        serialPort1.Read(inData, 0, 32);
                        if (inData[31] > 0xBF && inData[31] < 0xD0 && inData[0] <= rate)
                        {
                            var loggingData = new double[8];
                            for (int i = 0; i < 8; i++)
                            {
                                int outVal = interpret24bitAsInt32(inData[i * 3 + 1], inData[i * 3 + 2],
                                    inData[i * 3 + 3]);
                                dataOut[i] = (double) (outVal * scale);
                                if (frequencyToggle)
                                {
                                    dataOut[(i+8)] = FilterBeta(dataOut[i], i);
                                }
                                dataOut[i] = Filter(dataOut[i], i);
                                loggingData[i] = dataOut[i];
                                if (lastDataOut.Count > 5 && dataOut[i] == lastDataOut.First()[i])
                                {
                                    dataOut[i] = double.NaN;
                                    Console.WriteLine("Node " + (i+1) +" is not connected");
                                }
                                else if (i == 7 && lastDataOut.Count > 5) { 

                                    Console.WriteLine("Node " + (i + 1) + " is connected with value " + dataOut[i]);
                                    lastDataOut.RemoveAt(0);
                                }
                                else
                                {
                                    Console.WriteLine("Node " + (i + 1) + " is connected with value " + dataOut[i]);
                                }

                            }

                            lastDataOut.Add(loggingData);
                        }

                        frequencyToggle = !frequencyToggle;
                    }
                    */



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

        //Filtering function for OpenBCI Nodes
        //Adapted from nekrodezynfekator's OpenBCI_GUI repository
        private double Filter(double inputVal, int i)
        {
            double returnVal = 0;
            var b = new double[5] { 0.1173510367246093, 0, -0.2347020734492186, 0, 0.1173510367246093 };
            var a = new double[5] { 1, -2.137430180172061, 2.038578008108517, -1.070144399200925, 0.2946365275879138 };
            var b2 = new double[5] { 0.9650809863447347, -0.2424683201757643, 1.945391494128786, -0.2424683201757643, 0.9650809863447347 };
            var a2 = new double[5] { 1, -0.2467782611297853, 1.944171784691352, -0.2381583792217435, 0.9313816821269039 };

            for (int j = 4; j > 0; j--)
                {
                    prev_x_notch[i, j] = prev_x_notch[i, j - 1];
                    prev_y_notch[i, j] = prev_y_notch[i, j - 1];
                    prev_x_standard[i, j] = prev_x_standard[i, j - 1];
                    prev_y_standard[i, j] = prev_y_standard[i, j - 1];
                }

                prev_x_notch[i, 0] = inputVal;

                double score = 0;

                for (int j = 0; j < 5; j++)
                {
                    score += b2[j]*prev_x_notch[i, j];
                    if (j > 0)
                    {
                        score -= a2[j]*prev_y_notch[i, j];
                    }
                }

                prev_y_notch[i, 0] = score;
                prev_x_standard[i, 0] = score;
                score = 0;
                for (int j = 0; j < 5; j++)
                {
                    score += b[j]*prev_x_standard[i, j];
                    if (j > 0)
                    {
                        score -= a[j]*prev_y_standard[i, j];
                    }
                }

                prev_y_standard[i, 0] = score;
                returnVal = score;

            return returnVal;
        }

        //Filter modified for Beta waves while recording at higher frequencies
        private double FilterBeta(double inputVal, int i)
        {
            double returnVal = 0;
            var b = new double[5] { 0.1173510367246093, 0, -0.2347020734492186, 0, 0.1173510367246093 };
            var a = new double[5] { 1, -2.137430180172061, 2.038578008108517, -1.070144399200925, 0.2946365275879138 };
            var b2 = new double[5] { 0.96508099, -1.19328255, 2.29902305, -1.19328255, 0.96508099 };
            var a2 = new double[5] { 1, -1.21449347931898, 2.29780334191380, -1.17207162934772, 0.931381682126902 };

            for (int j = 4; j > 0; j--)
            {
                prev_x_notchBeta[i, j] = prev_x_notchBeta[i, j - 1];
                prev_y_notchBeta[i, j] = prev_y_notchBeta[i, j - 1];
                prev_x_standardBeta[i, j] = prev_x_standardBeta[i, j - 1];
                prev_y_standardBeta[i, j] = prev_y_standardBeta[i, j - 1];
            }

            prev_x_notchBeta[i, 0] = inputVal;

            double score = 0;

            for (int j = 0; j < 5; j++)
            {
                score += b2[j] * prev_x_notchBeta[i, j];
                if (j > 0)
                {
                    score -= a2[j] * prev_y_notchBeta[i, j];
                }
            }

            prev_y_notchBeta[i, 0] = score;
            prev_x_standardBeta[i, 0] = score;
            score = 0;
            for (int j = 0; j < 5; j++)
            {
                score += b[j] * prev_x_standardBeta[i, j];
                if (j > 0)
                {
                    score -= a[j] * prev_y_standardBeta[i, j];
                }
            }

            prev_y_standardBeta[i, 0] = score;
            returnVal = score;

            return returnVal;
        }
    }
}
