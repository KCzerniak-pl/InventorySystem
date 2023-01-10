using InventorySystemWebApi.Models;
using InventorySystemWebApi.Models.Account;
using InventorySystemWebApi.Models.Item;
using InventorySystemWebApi.Tests.Integration.Helpers;
using System.Net;

namespace InventorySystemWebApi.Tests.Integration.Controllers
{
    public class ItemControllerTests : IClassFixture<InwentorySystemWebApiFactory> // Shared context.
    {
        private readonly HttpClient _client;

        public ItemControllerTests(InwentorySystemWebApiFactory inwentorySystemWebApiFactory)
        {
            _client = inwentorySystemWebApiFactory.Client;
        }

        public static IEnumerable<object[]> SampleValidDataPageQuery()
        {
            yield return new object[] { new PageQuery() { PageNumber = 1, PageSize = 10 } };
            yield return new object[] { new PageQuery() { SearchPhrase = "Test", PageNumber = 1, PageSize = 10 } };
        }

        public static IEnumerable<object[]> SampleInvalidDataPageQuery()
        {
            yield return new object[] { new PageQuery() { PageNumber = 10, PageSize = 10 } }; // No result for 'PageNumber'.
            yield return new object[] { new PageQuery() { SearchPhrase = "TestTest", PageNumber = 1, PageSize = 10 } }; // No result for 'SearchPhrase'.
        }

        public static IEnumerable<object[]> SampleValidModelCreateItemDto()
        {
            yield return new object[] { new CreateItemDto() { Name = "Item Test 3", TypeId = 1, GroupId = 1, LocationId = 1 } };
            yield return new object[] { new CreateItemDto() { Name = "Item Test 4", Description = "Description Test 3", TypeId = 1, GroupId = 1, LocationId = 1 } };
        }

        public static IEnumerable<object[]> SampleInvalidModelCreateItemDto()
        {
            yield return new object[] { new CreateItemDto() { TypeId = 1, GroupId = 1, LocationId = 1 } }; // Without 'Name'.
            yield return new object[] { new CreateItemDto() { Name = "Item Test 5", GroupId = 1, LocationId = 1 } }; // Without 'TypeId'.
            yield return new object[] { new CreateItemDto() { Name = "Item Test 5", TypeId = 1, LocationId = 1 } }; // Without 'GroupId'.
            yield return new object[] { new CreateItemDto() { Name = "Item Test 5", TypeId = 1, GroupId = 1 } }; // Without 'LocationId'.
            yield return new object[] { new CreateItemDto() { Name = new string('*', 151), TypeId = 1, GroupId = 1, LocationId = 1 } }; // Invalid 'Name' (over 150 characters).
            yield return new object[] { new CreateItemDto() { Name = "Item Test 5", Description = new string('*', 4001), TypeId = 1, GroupId = 1, LocationId = 1 } }; // Invalid 'Description' (over 4000 characters).
            yield return new object[] { new CreateItemDto() { Name = "Item Test 5", TypeId = 1, GroupId = 1, InvoiceNumber = new string('*', 256), LocationId = 1 } }; // Invalid 'InvoiceNumber' (over 255 characters).
        }

        [Theory]
        [MemberData(nameof(SampleValidDataPageQuery))]
        public async Task GetAll_ForValidData_ReturnsOk(PageQuery pageQuery)
        {
            // Act.
            var response = await _client.GetAsync($"/api/items?searchphrase={pageQuery.SearchPhrase}&pagenumber={pageQuery.PageNumber}&pagesize={pageQuery.PageSize}");

            // Assert.
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(SampleInvalidDataPageQuery))]
        public async Task GetAll_ForInvalidData_ReturnsNotFound(PageQuery pageQuery)
        {
            // Act.
            var response = await _client.GetAsync($"/api/items?searchphrase={pageQuery.SearchPhrase}&pagenumber={pageQuery.PageNumber}&pagesize={pageQuery.PageSize}");

            // Assert.
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetByItem_ForValidData_ReturnsOk(int item)
        {
            // Act.
            var response = await _client.GetAsync($"/api/item/{item}");

            // Assert.
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(200)]
        public async Task GetByItem_ForInvalidData_ReturnsNotFound(int item)
        {
            // Act.
            var response = await _client.GetAsync($"/api/item/{item}");

            // Assert.
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(SampleValidModelCreateItemDto))]
        public async Task CreateItem_ForValidModel_ReturnsCreated(CreateItemDto dto)
        {
            // Arrange.
            var httpContent = dto.ToJsonHttpContent();

            // Act.
            var response = await _client.PostAsync("/api/item", httpContent);

            // Assert.
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(SampleInvalidModelCreateItemDto))]
        public async Task CreateItem_ForInvalidModel_ReturnsBadRequest(CreateItemDto dto)
        {
            // Arrange.
            var httpContent = dto.ToJsonHttpContent();

            // Act.
            var response = await _client.PostAsync("/api/item", httpContent);

            // Assert.
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task RemoveItem_ForValidData_ReturnsNoContent()
        {
            // Arrange.
            var item = 1;

            // Act.
            var response = await _client.DeleteAsync($"/api/item/{item}");

            // Assert.
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task RemoveItem_ForInvalidData_ReturnsNotFound()
        {
            // Arrange.
            var item = 111;

            // Act.
            var response = await _client.DeleteAsync($"/api/item/{item}");

            // Assert.
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
