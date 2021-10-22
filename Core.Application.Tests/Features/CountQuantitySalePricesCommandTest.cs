using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.DTOs;
using Core.Application.Features;
using Core.Application.Interfaces;
using Domain.Models;
using Moq;
using NUnit.Framework;

namespace Core.Application.Tests.Features
{
    [TestFixture]
    public class CountQuantitySalePricesCommandTest
    {
        [Test]
        public async Task CountQuantitySaleTest()
        {
            // Arrange
            var repositoryMock = SetupRepositoryMock();

            var target = new CountSalePriceCommandHandler(repositoryMock.Object);

            var command = new CountQuantitySalePricesCommand
            {
                BasePrice = 12.20M,
                CustomerDiscountMultiplier = 0.1,
                ProductDiscountMultiplier = 0.19
            };
            
            var expectedResult = PrepareExpectedResult();

            // Act
            var result = await target.Handle(command, new CancellationToken());

            // Assert
            Assert.AreEqual(expectedResult.Count, result.Count);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(expectedResult[i].MinQuantity, result[i].MinQuantity);
                Assert.AreEqual(expectedResult[i].SalePrice, result[i].SalePrice);
            }
        }

        private static List<QuantitySalePriceResponse> PrepareExpectedResult()
        {
            return new List<QuantitySalePriceResponse>()
            {
                new QuantitySalePriceResponse
                {
                    MinQuantity = 10,
                    SalePrice = 7.80M
                },
                new QuantitySalePriceResponse
                {
                    MinQuantity = 25,
                    SalePrice = 7.36M
                },
                new QuantitySalePriceResponse
                {
                    MinQuantity = 50,
                    SalePrice = 5.20M
                },
            };
        }

        private static Mock<IRepository>? SetupRepositoryMock()
        {
            var repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(x => x.GetAllQuantityDiscountsAsync()).ReturnsAsync(() => new List<QuantityDiscount>
            {
                new QuantityDiscount
                {
                    MinQuantity = 10,
                    Multiplier = 0.10
                },
                new QuantityDiscount
                {
                    MinQuantity = 25,
                    Multiplier = 0.15
                },
                new QuantityDiscount
                {
                    MinQuantity = 50,
                    Multiplier = 0.40
                }
            });
            return repositoryMock;
        }
    }
}