using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockTable.Tests.ServiceTest.ServiceFolder
{
    public class TestFolderExist
    {
        public stockTable.Service.ServiceFolder _service;
        [SetUp]
        public void Setup() 
        {
           _service = new stockTable.Service.ServiceFolder();
        }

        [Test]
        public void TestExist_ReturnTrue()
        {

            Assert.That(_service.FolderExists("C:\\Users\\denis\\Desktop\\Copy\\stockTable","Logs"), Is.True);
        }

    }
}
