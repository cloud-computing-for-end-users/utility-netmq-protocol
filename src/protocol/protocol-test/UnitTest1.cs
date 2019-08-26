using net_mq_decoder;
using net_mq_encoder;
using NUnit.Framework;
using protocol.server_methods;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            string testParam = "Hello, world!";
            var methodToCall = new HelloWorldMethod(testParam);

            var encodedMethod = NetMqEncoder.GenerateServerModuleMethodMessage(methodToCall);
            var decodedMethod = NetMqDecoder.DecodeServerModuleMethod(encodedMethod);

            if(decodedMethod is HelloWorldMethod method)
            {
                if (testParam.Equals(method.param1))
                {
                    Assert.Pass();
                }
            }



            Assert.Fail();
        }
    }
}