using System.Net.Sockets;
using System.Text;
using Google.FlatBuffers;
using FlatBufferTest;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();
            client.Connect("localhost", 21000);

            NetworkStream stream = client.GetStream();

            var builder = new FlatBufferBuilder(1024);
            var name = builder.CreateString("wshan");
            var testStruct = TestStruct.CreateTestStruct(builder, name);

            builder.Finish(testStruct.Value);

            byte[] buf = builder.SizedByteArray();
            stream.Write(buf, 0, buf.Length);
        }
    }
}
