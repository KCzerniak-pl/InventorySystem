using InventorySystemWebApi.Models.Account;
using InventorySystemWebApi.Tests.Integration.Helpers;
using System.Net;

namespace InventorySystemWebApi.Tests.Integration.Controllers
{
    public class AccountControllerTests : IClassFixture<InwentorySystemWebApiFactory> // Shared context.
    {
        private readonly HttpClient _client;

        public AccountControllerTests(InwentorySystemWebApiFactory inwentorySystemWebApiFactory)
        {
            _client = inwentorySystemWebApiFactory.Client;
        }

        public static IEnumerable<object[]> SampleInvalidDataCreateAccountDto()
        {
            yield return new object[] { new CreateAccountDto() { LastName = "Doe", Email = "john.doe@test.com", Password = "password", RoleId = 2 } }; // Without 'FirstName'.
            yield return new object[] { new CreateAccountDto() { FirstName = "John", Email = "john.doe@test.com", Password = "password", RoleId = 2 } }; // Without 'Lastname'.
            yield return new object[] { new CreateAccountDto() { FirstName = "John", LastName = "Doe", Password = "password", RoleId = 2 } }; // Without 'Email'.
            yield return new object[] { new CreateAccountDto() { FirstName = "John", LastName = "Doe", Email = "john.doe@test.com", RoleId = 2 } }; // Without 'Password'.
            yield return new object[] { new CreateAccountDto() { FirstName = "John", LastName = "Doe", Email = "john.doe@test.com", Password = "password" } }; // Without 'RoleId'.
            yield return new object[] { new CreateAccountDto() { FirstName = "John", LastName = "Doe", Email = "john.doetest.com", Password = "password", RoleId = 2 } }; // Invalid 'Email'.
            yield return new object[] { new CreateAccountDto() { FirstName = "John", LastName = "Doe", Email = "john.doe@test.com", Password = "password", RoleId = 333 } }; // Invalid 'RoleId'. 
        }

        public static IEnumerable<object[]> SampleInvalidDataLoginRequestDto()
        {
            yield return new object[] { new LoginRequestDto() { Email = "noah.miller@test.com", Password = "password" } }; // Invalid 'Email'.
            yield return new object[] { new LoginRequestDto() { Email = "oliver.smith@test.com", Password = "invalidpassword" } }; // Invalid 'Password'.
            yield return new object[] { new LoginRequestDto() { Password = "password" } }; // Without 'Email'.
            yield return new object[] { new LoginRequestDto() { Email = "oliver.smith@test.com" } }; // Without 'Password'.
        }

        [Fact]
        public async Task CreateAccount_ForValidModel_ReturnsNoContent()
        {
            // Arrange.
            var createAccountDto = new CreateAccountDto()
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@test.com",
                Password = "password",
                RoleId = 1
            };

            var httpContent = createAccountDto.ToJsonHttpContent();

            // Act.
            var response = await _client.PostAsync("/api/account/create", httpContent);

            // Assert.
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(SampleInvalidDataCreateAccountDto))]
        public async Task CreateAccount_ForInvalidModel_ReturnsBadRequest(CreateAccountDto dto)
        {
            // Arrange.
            var httpContent = dto.ToJsonHttpContent();

            // Act.
            var response = await _client.PostAsync("/api/account/create", httpContent);

            // Assert.
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task LoginRequest_ForValidData_ReturnsOk()
        {
            // Arrange.
            var loginRequestDto = new LoginRequestDto()
            {
                Email = "oliver.smith@test.com",
                Password = "password",
            };

            var httpContent = loginRequestDto.ToJsonHttpContent();

            // Act.
            var response = await _client.PostAsync("/api/account", httpContent);

            // Assert.
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(SampleInvalidDataLoginRequestDto))]
        public async Task LoginRequest_ForInvalidData_ReturnsBadRequest(LoginRequestDto dto)
        {
            // Arrange.
            var httpContent = dto.ToJsonHttpContent();

            // Act.
            var response = await _client.PostAsync("/api/account", httpContent);

            // Assert.
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
