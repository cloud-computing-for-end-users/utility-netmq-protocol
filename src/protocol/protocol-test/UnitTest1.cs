using net_mq_decoder;
using net_mq_encoder;
using NUnit.Framework;
using protocol.methods.server_methods;
using protocol.methods.slave_owner_methods;
using protocol.server_methods;
using protocol.slave_owner_methods;
using System;

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

            if (decodedMethod is HelloWorldMethod method)
            {
                if (testParam.Equals(method.param1))
                {
                    Assert.Pass();
                }
            }
            Assert.Fail();
        }

        /// <summary>
        /// testing GetSlaveMethod
        /// </summary>
        [Test]
        public void Test2()
        {
            var appInfo = new protocol.model.ApplicationInfo()
            {
                Application = protocol.model.ApplicationInfo.SupportedApplication.Excel
            };
            var key = new protocol.model.PrimaryKey()
            {
                ThePrimaryKey = "hello there"
            };

            var methodToCall = new GetSlaveMethod()
            {
                ApplicationInfo = appInfo,
                SlaveBelongsTo = key

            };

            var encodedMethod = NetMqEncoder.GenerateSlaveOwnerModuleMethodMessage(methodToCall);
            var decodedMethod = NetMqDecoder.DecodeSlaveOwnerModuleMethod(encodedMethod);

            if (decodedMethod is GetSlaveMethod _method)
            {
                var cp1 = _method.ApplicationInfo.Equals(appInfo);
                var cp2 = _method.SlaveBelongsTo.Equals(key);
                if (cp1 && cp2)
                {
                    Assert.Pass();
                }
            }
            Assert.Fail();
        }


        /// <summary>
        /// testing GetListOfRunnableApplicationsMethod
        /// </summary>
        [Test]
        public void Test3()
        {
            var methodToCall = new GetListOfRunnableApplicationsMethod()
            {
            };

            var encodedMethod = NetMqEncoder.GenerateSlaveOwnerModuleMethodMessage(methodToCall);
            var decodedMethod = NetMqDecoder.DecodeSlaveOwnerModuleMethod(encodedMethod);

            if (decodedMethod is GetListOfRunnableApplicationsMethod _method)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        /// <summary>
        /// testing RegisterSlaveOwnerServermoduleMethod
        /// </summary>
        [Test]
        public void Test4()
        {
            var methodToCall = new RegisterSlaveOwnerServermoduleMethod()
            {
            };

            var encodedMethod = NetMqEncoder.GenerateServerModuleMethodMessage(methodToCall);
            var decodedMethod = NetMqDecoder.DecodeServerModuleMethod(encodedMethod);

            if (decodedMethod is RegisterSlaveOwnerServermoduleMethod _method)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        /// <summary>
        /// testing RegisterFileServermoduleMethod
        /// </summary>
        [Test]
        public void Test5()
        {
            var methodToCall = new RegisterFileServermoduleMethod()
            {
            };

            var encodedMethod = NetMqEncoder.GenerateServerModuleMethodMessage(methodToCall);
            var decodedMethod = NetMqDecoder.DecodeServerModuleMethod(encodedMethod);

            if (decodedMethod is RegisterFileServermoduleMethod _method)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        /// <summary>
        /// testing RegisterDatabaseServermoduleMethod
        /// </summary>
        [Test]
        public void Test6()
        {
            var methodToCall = new RegisterDatabaseServermoduleMethod()
            {
            };

            var encodedMethod = NetMqEncoder.GenerateServerModuleMethodMessage(methodToCall);
            var decodedMethod = NetMqDecoder.DecodeServerModuleMethod(encodedMethod);

            if (decodedMethod is RegisterDatabaseServermoduleMethod _method)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

    }
}