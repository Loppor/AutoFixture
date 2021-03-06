﻿using System;
using NUnit.Framework;

namespace AutoFixture.NUnit3.UnitTest
{
    [TestFixture]
    public class CustomizeAttributeTest
    {
        [Test]
        public void TestableSutIsSut()
        {
            // Fixture setup
            // Exercise system
            var sut = new DelegatingCustomizeAttribute();
            // Verify outcome
            Assert.IsInstanceOf<CustomizeAttribute>(sut);
            // Teardown
        }

        [Test]
        public void SutIsAttribute()
        {
            // Fixture setup
            // Exercise system
            var sut = new DelegatingCustomizeAttribute();
            // Verify outcome
            Assert.IsInstanceOf<Attribute>(sut);
            // Teardown
        }

        [Test]
        public void SutImplementsIParameterCustomizationSource()
        {
            // Fixture setup
            // Exercise system
            var sut = new DelegatingCustomizeAttribute();
            // Verify outcome
            Assert.IsInstanceOf<IParameterCustomizationSource>(sut);
            // Teardown
        }
    }
}
