using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using Google.FlatBuffers;
using FlatBufferTest;

namespace DotnetSupersocket
{
    public class Class1
    {
        public void ServerTest()
        {
            TcpListener listener = new TcpListener(21000);
            listener.Start();

            Socket clinet = listener.AcceptSocket();

            byte[] buffer = new byte[1024];
            clinet.BeginReceive(buffer, 0, 1024, 0, DataRecieved, buffer);

            while (true) { }

            listener.Stop();
        }

        private void DataRecieved(IAsyncResult ar)
        {
            byte[] buff = (byte[])ar.AsyncState;

            var byteBuf = new ByteBuffer(buff);
            var testStruct = TestStruct.GetRootAsTestStruct(byteBuf);
            Console.WriteLine("{0}", testStruct.Name);
        }
    }
}
