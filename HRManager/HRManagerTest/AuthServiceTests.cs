using Moq;
using Xunit;
using Microsoft.Extensions.Configuration;
using HRManager.Model;

namespace HRManagerTest;
public class AuthServiceTests
{
	private readonly AuthService _authService;
	private readonly Mock<IConfiguration> _mockConfig;

	public AuthServiceTests()
	{
		// Mock the IConfiguration dependency
		_mockConfig = new Mock<IConfiguration>();
		_mockConfig.Setup(x => x["Admin:Username"]).Returns("admin");
		_mockConfig.Setup(x => x["Admin:Password"]).Returns("pass123");

		_authService = new AuthService(_mockConfig.Object);
	}

	[Fact]
	public void Authenticate_ValidCredentials_ReturnsTrue()
	{
		// Arrange
		var username = "admin";
		var password = "pass123";

		// Act
		var result = _authService.Authenticate(username, password);

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void Authenticate_InvalidCredentials_ReturnsFalse()
	{
		// Arrange
		var username = "wrongUser";
		var password = "wrongPass";

		// Act
		var result = _authService.Authenticate(username, password);

		// Assert
		Assert.False(result);
	}
}
