using birdsProject;
using DocumentFormat.OpenXml.Office2010.CustomUI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace TestBirdsProject;
[TestClass()]
public class Testing
{
    [TestMethod()]
    public void signUp()
    {
        User a = new User("ahkcht!!","123456787","");
        Assert.IsFalse(a.signUpVlidation());
    }
    [TestMethod()]
    public void addBird()
    {
        Bird b = new Bird("pink", "12/12/19", "Male","12345677", "Goldian", "ab12345","12312312","12121222");
        Assert.IsFalse(b.Add());
    }
    [TestMethod()]
    public void addCage()
    {
        Cage b = new Cage("AB123","aa","ss","100","steel");
        Assert.IsFalse(b.add());
    }
    [TestMethod()]
    public void logIn()
    {
        User userNotExist = new User("shayll", "123456787", "");
        Assert.IsFalse(userNotExist.logInValidation());
    }
}