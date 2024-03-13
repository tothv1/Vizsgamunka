using AuthAPI.Models;
using AuthAPI.Services;
using AuthAPI.Services.AuthServices;
using AuthAPI.Services.IServices;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace AuthAPI.Tests.Services.AuthServices
{
    [ExcludeFromCodeCoverage]
    public class AuthServicesTests
    {
        [Fact]
        public async Task ConfirmAccount_ShouldReturnBadRequest()
        {
            // Arrange
            var registriesMock = new List<Registry>
            {
                new Registry() { TempConfirmationKey = "Dorya!" },
                new Registry() { TempConfirmationKey = "Masku!" }
            }.AsQueryable();

            var authContextMock = new Mock<AuthContext>();

            var registriesDbSetMock = new Mock<DbSet<Registry>>();
            registriesDbSetMock.As<IQueryable<Registry>>().Setup(m => m.Provider).Returns(registriesMock.AsQueryable().Provider);
            registriesDbSetMock.As<IQueryable<Registry>>().Setup(m => m.Expression).Returns(registriesMock.AsQueryable().Expression);
            registriesDbSetMock.As<IQueryable<Registry>>().Setup(m => m.ElementType).Returns(registriesMock.AsQueryable().ElementType);
            registriesDbSetMock.As<IQueryable<Registry>>().Setup(m => m.GetEnumerator()).Returns(registriesMock.GetEnumerator());

            authContextMock.Setup(method => method.Registries).Returns(registriesDbSetMock.Object);
            var authService = new AuthService(new Mock<ITokenManager>().Object, new Mock<IPasswordManager>().Object, new Mock<IConfirmationKeyGenerate>().Object, new Mock<IEmailSenderService>().Object, authContextMock.Object);

            // Act
            var result = (ResponseObject)await authService.ConfirmAccount("Bad Confirmation Key!");

            // Assert
            using (new AssertionScope())
            {
                result.Should().NotBeNull();
                result.Should().BeOfType<ResponseObject>();
                result.Status.Should().Be(400);
                result.ResponseMessage.Should().Be("Hibás kulcs, vagy nem létező fiók!");
            }
        }

        [Fact]
        public async Task ConfirmAccount_ShouldReturnFromCatch()
        {
            // Arrange
            var authContextMock = new Mock<AuthContext>();

            // Act
            var authService = new AuthService(new Mock<ITokenManager>().Object, new Mock<IPasswordManager>().Object, new Mock<IConfirmationKeyGenerate>().Object, new Mock<IEmailSenderService>().Object, authContextMock.Object);

            var result = (ResponseObject)await authService.ConfirmAccount("Bad Confirmation Key!");

            // Assert
            using (new AssertionScope())
            {
                result.Should().NotBeNull();
                result.Should().BeOfType<ResponseObject>();
                result.Status.Should().Be(400);
                result.ResponseMessage.Should().Be("Value cannot be null. (Parameter 'source')");
            }
        }
    }
}
