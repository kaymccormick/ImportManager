// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using ImportManager;

namespace PackageBuilderTests
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void TestMethod()
        {
            PackageBuilder packageBuilder = new PackageBuilder();
            packageBuilder.BuildPackage();
        }
    }
}
