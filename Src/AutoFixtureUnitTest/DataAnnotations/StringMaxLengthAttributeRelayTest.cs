﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.DataAnnotations;
using AutoFixture.Kernel;
using AutoFixtureUnitTest.Kernel;
using Xunit;

namespace AutoFixtureUnitTest.DataAnnotations
{
    public class StringMaxLengthAttributeRelayTest
    {
        [Fact]
        public void SutIsSpecimenBuilder()
        {
            // Fixture setup
            // Exercise system
            var sut = new StringMaxLengthAttributeRelay();
            // Verify outcome
            Assert.IsAssignableFrom<ISpecimenBuilder>(sut);
            // Teardown
        }

        [Fact]
        public void CreateWithNullRequestReturnsCorrectResult()
        {
            // Fixture setup
            var sut = new StringMaxLengthAttributeRelay();
            // Exercise system
            var dummyContext = new DelegatingSpecimenContext();
            var result = sut.Create(null, dummyContext);
            // Verify outcome
            Assert.Equal(new NoSpecimen(), result);
            // Teardown
        }

        [Fact]
        public void CreateWithNullContextThrows()
        {
            // Fixture setup
            var sut = new StringMaxLengthAttributeRelay();
            var dummyRequest = new object();
            // Exercise system and verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                sut.Create(dummyRequest, null));
            // Teardown
        }

        [Fact]
        public void CreateWithAnonymousRequestReturnsCorrectResult()
        {
            // Fixture setup
            var sut = new StringMaxLengthAttributeRelay();
            var dummyRequest = new object();
            // Exercise system
            var dummyContainer = new DelegatingSpecimenContext();
            var result = sut.Create(dummyRequest, dummyContainer);
            // Verify outcome
            var expectedResult = new NoSpecimen();
            Assert.Equal(expectedResult, result);
            // Teardown
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(1)]
        [InlineData(typeof(object))]
        [InlineData(typeof(string))]
        [InlineData(typeof(int))]
        [InlineData(typeof(Version))]
        public void CreateWithNonConstrainedStringRequestReturnsCorrectResult(object request)
        {
            // Fixture setup
            var sut = new StringMaxLengthAttributeRelay();
            // Exercise system
            var dummyContext = new DelegatingSpecimenContext();
            var result = sut.Create(request, dummyContext);
            // Verify outcome
            var expectedResult = new NoSpecimen();
            Assert.Equal(expectedResult, result);
            // Teardown
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void CreateWithConstrainedMaxLengthStringRequestReturnsCorrectResult(int maximum)
        {
            // Fixture setup
            var stringLengthAttribute = new StringLengthAttribute(maximum);
            var providedAttribute = new ProvidedAttribute(stringLengthAttribute, true);
            var request = new FakeMemberInfo(providedAttribute);
            var expectedRequest = new ConstrainedMaxLengthStringRequest(stringLengthAttribute.MaximumLength);
            var expectedResult = new object();
            var context = new DelegatingSpecimenContext
            {
                OnResolve = r => expectedRequest.Equals(r) ? expectedResult : new NoSpecimen()
            };
            var sut = new StringMaxLengthAttributeRelay();
            // Exercise system
            var result = sut.Create(request, context);
            // Verify outcome
            Assert.Equal(expectedResult, result);
            // Teardown
        }

        [Theory]
        [InlineData("12")]
        public void CreateParameterWithMaxLengthConstraint([MaxLength(2)]string p)
        {
            Assert.True(p.Length == 2);
        }

        [Theory]
        [InlineData("123")]
        public void CreateParameterWithMaxLengthConstraintWithTooLongInput([MaxLength(2)]string p)
        {
            Assert.False(p.Length == 2);
        }
    }
}