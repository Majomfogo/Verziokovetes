using NUnit.Framework;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        [Test,
            TestCase("abcd1234", false),
    TestCase("irf@uni-corvinus", false),
    TestCase("irf.uni-corvinus.hu", false),
    TestCase("akos.kover@stud.uni-corvinus.hu", true)]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            // Arrange
            var accountController = new AccountController();

            // Act
            var actualResult = accountController.ValidateEmail(email);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test,
         TestCase("Majomfogo", false),
         TestCase("MAJOMFOGO1", false),
         TestCase("majomfogo1", false),
         TestCase("majom1", false),
         TestCase("Majomfogo1", true),]
        public void TestValidatePassword(string password, bool expectedResult)
        {
            //Arrange
            var accountController = new AccountController();

            // Act
            var actualResult = accountController.ValidatePassword(password);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        
            [Test,
            TestCase("akos.kover@stud.uni-corvinus.hu", "Majomfogo1"),
            TestCase("akos.kover@stud.uni-corvinus.hu", "Majomfogo1234"),]

        public void TestRegisterHappyPath(string email, string password)
        {
            // Arrange
            var accountController = new AccountController();

            // Act
            var actualResult = accountController.Register(email, password);

            // Assert
            Assert.AreEqual(email, actualResult.Email);
            Assert.AreEqual(password, actualResult.Password);
            Assert.AreNotEqual(Guid.Empty, actualResult.ID);
        }

        [
    Test,
    TestCase("akos.kover@uni-corvinus", "Abcd1234"),
    TestCase("akos.kover.stud.uni-corvinus.hu", "Abcd1234"),
    TestCase("akos.kover@stud.uni-corvinus.hu", "abcd1234"),
    TestCase("akos.kover@stud.uni-corvinus.hu", "ABCD1234"),
    TestCase("akos.kover@stud.uni-corvinus.hu", "abcdABCD"),
    TestCase("akos.kover@stud.uni-corvinus.hu", "Ab1234"),
]
        public void TestRegisterValidateException(string email, string password)
        {
            // Arrange
            var accountController = new AccountController();

            // Act
            try
            {
                var actualResult = accountController.Register(email, password);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ValidationException>(ex);
            }

            // Assert
        }
    }
}
