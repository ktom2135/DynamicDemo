using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AutoMapper;
using Dapper;
using DynamicObj.Business;
using DynamicObj.DAL;
using DynamicObj.Share;
using DynamicObj.Share.DTO;
using DynamicObj.Share.Module;
using LightInject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DynamicObj.Test
{
    [TestClass]
    public class FooServiceUAT
    {
        [TestInitialize]
        public void SetUp()
        {
            //Mock<ConnectionFactory> _connectionFactory = new Mock<ConnectionFactory>();
            //string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Database1.mdf;Integrated Security=True";

            //_connectionFactory.SetupGet(x => x.connectionString).Returns(connectionString);

            //_container.Register<ConnectionFactory>(factory => _connectionFactory.Object, "ConnectionFactory");
            //cleanObjAndObjPropertiesTable(connectionString);
        }

        [TestMethod]
        public void TestMethod2()
        {
            using (ServiceContainer _container = new ServiceContainer())
            {
                Mock<BaseObjRepository> mock = new Mock<BaseObjRepository>();
                mock.Setup(p => p.GetObj(It.IsAny<int>())).Returns(new Obj());
                _container.Register<BaseObjRepository>(factory => mock.Object);
                _container.Register<FooService>();
                FooService fooService = _container.GetInstance<FooService>();
            }
        }

        [TestMethod]
        public void insert_then_get()
        {
            using (ServiceContainer _container = new ServiceContainer())
            {
                FooService fooService = _container.GetInstance<FooService>();

                Foo foo = new Foo()
                {
                    Name = "Tom Chen",
                    Birthday_Title = DateTime.Now,
                    Address = "Beijing"
                };

                FooDTO fooDTO = Mapper.Map<FooDTO>(foo);

                var baseObj = fooService.Insert(fooDTO);

                var aa = fooService.GetById(baseObj.Id);

                Assert.AreEqual(baseObj.Id, aa.Id);
                Assert.AreEqual(baseObj.BirthDayTitle, aa.BirthDayTitle);
                Assert.AreEqual(baseObj.Name, aa.Name);
                Assert.AreEqual(baseObj.Address_title, aa.Address_title);

                List<int> ids = new List<int>() { 1, 2, 3, 4, 5, 6 };
                var bb = fooService.GetByIds(ids);

            }

        }

        private void cleanObjAndObjPropertiesTable(string connetionString)
        {
            using (IDbConnection conn = new SqlConnection(connetionString))
            {
                string deleteObj = "delete from Obj";
                string deleteObjPropert = "delete from ObjProperty";

                conn.Execute(deleteObjPropert);
                conn.Execute(deleteObj);
            }
        }
    }
}
