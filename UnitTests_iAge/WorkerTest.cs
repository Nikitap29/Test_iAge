using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test_iAge;

namespace UnitTests_iAge
{
    [TestClass]
    public class WorkerTest
    {
        [TestMethod]
        public void TestUpdateNormal()
        {
            var args = new string[4] {"-add", "FirstName:Johny", "LastName:Connor", "Salary:111" };
            var wrk = new Worker();
            wrk.Add(args);
            args = new string[4] { "-update", "Id:1", "FirstName:John", "LastName:Travolta" };
            var res = wrk.Update(args);
            Assert.AreEqual(0, res);
        }

        [TestMethod]
        public void TestUpdateZeroId()
        {
            var args = new string[4] { "-add", "FirstName:Johny", "LastName:Connor", "Salary:111" };
            var wrk = new Worker();
            wrk.Add(args);
            args = new string[4] { "-update", "Id:0", "FirstName:John", "LastName:Travolta" };
            var res = wrk.Update(args);
            Assert.AreEqual(-3, res);
        }

        [TestMethod]
        public void TestUpdateMinusId()
        {
            var args = new string[4] { "-add", "FirstName:Johny", "LastName:Connor", "Salary:111" };
            var wrk = new Worker();
            wrk.Add(args);
            args = new string[4] { "-update", "Id:-1", "FirstName:John", "LastName:Travolta" };
            var res = wrk.Update(args);
            Assert.AreEqual(-2, res);
        }

        [TestMethod]
        public void TestUpdateMinusSalary()
        {
            var args = new string[4] { "-add", "FirstName:Johny", "LastName:Connor", "Salary:111" };
            var wrk = new Worker();
            wrk.Add(args);
            args = new string[4] { "-update", "Id:-1", "FirstName:John", "Salary:-10" };
            var res = wrk.Update(args);
            Assert.AreEqual(-2, res);
        }

        [TestMethod]
        public void TestUpdateWordSalary()
        {
            var args = new string[4] { "-add", "FirstName:Johny", "LastName:Connor", "Salary:111" };
            var wrk = new Worker();
            wrk.Add(args);
            args = new string[4] { "-update", "Id:-1", "FirstName:John", "Salary:abc" };
            var res = wrk.Update(args);
            Assert.AreEqual(-2, res);
        }
    }
}
