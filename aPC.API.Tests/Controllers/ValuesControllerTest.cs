using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using aPC.Web;
using aPC.Web.Controllers;
using aPC.Web.Models;
using aPC.Common.Entities;

namespace aPC.Web.Tests.Controllers
{
  [TestClass]
  public class ValuesControllerTest
  {
    [TestMethod]
    public void Get()
    {
      // Arrange
      IntegratedController controller = new IntegratedController();

      // Act
      IEnumerable<amBXSceneSummary> result = controller.Get();

      // Assert
      Assert.IsNotNull(result);
      Assert.AreEqual(2, result.Count());
      Assert.AreEqual("value1", result.ElementAt(0));
      Assert.AreEqual("value2", result.ElementAt(1));
    }

    [TestMethod]
    public void GetById()
    {
      // Arrange
      IntegratedController controller = new IntegratedController();

      // Act
      amBXScene result = controller.Get("5");

      // Assert
      Assert.AreEqual("value", result);
    }

    [TestMethod]
    public void Post()
    {
      // Arrange
      IntegratedController controller = new IntegratedController();

      // Act
      controller.Post("value");

      // Assert
    }
  }
}
