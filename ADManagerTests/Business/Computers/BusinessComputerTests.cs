using Microsoft.VisualStudio.TestTools.UnitTesting;
using ADManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ADManager.Tests
{
    [TestClass()]
    public class BusinessComputerTests
    { 
        [TestMethod()]
        public void GetAllComputersTest()
        {
            ComputersProperties pcListesi = new BusinessComputer().GetAllComputers().First();

            bool actual = (pcListesi.computerName == "my.computer");
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void FillGridViewTest()
        {
            DataTable pcList = new BusinessComputer().FillGridView("HepsiniGetirBtn", string.Empty);
            bool actual = (pcList.Rows.Count > 150);
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SearchSingleComputerTest()
        {
            BusinessComputer computer = new BusinessComputer();
            computer.ComputerList = computer.GetAllComputers().ToList();
            List<ComputersProperties> pcList = computer.SearchSingleComputer("far");
            bool actual = (pcList.Count == 1);
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetComputerIpAddressTest()
        {
            BusinessComputer computer = new BusinessComputer();
            computer.ComputerDomain = "mydomain.local";
            string actual = computer.GetComputerIpAddress("my.pc");
            string expected = "ip adresi ";
            Assert.AreEqual(expected, actual);
        }
    }
}