﻿using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Wholesaler.Core.Dto.RequestModels;
using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Tests.Builders;
using Wholesaler.Tests.Helpers;
using Xunit;

namespace Wholesaler.Tests.RequirementController
{
    public class RequirementControllerTestsEditQuantity : WholesalerWebTest
    {
        private readonly ClientBuilder _clientBuilder;
        private readonly StorageBuilder _storageBuilder;
        private readonly RequirementBuilder _requirementBuilder;

        public RequirementControllerTestsEditQuantity(WebApplicationFactory<Program> factory) : base(factory)
        {
            _clientBuilder = new ClientBuilder();
            _storageBuilder = new StorageBuilder();
            _requirementBuilder = new RequirementBuilder();
        }

        [Fact]
        public async Task Edit_WithValidModel_ReturnsRequirementDto()
        {
            //Arrange

            var client = _clientBuilder.Build();
            var storage = _storageBuilder.Build();
            var requirement = _requirementBuilder
                .WithClientId(client.Id)
                .WithStorageId(storage.Id)
                .Build();

            Seed(client);
            Seed(storage);
            Seed(requirement);

            var id = requirement.Id;

            var updateRequestModel = new UdpateRequirementRequestModel()
            {
                Quantity = 50
            };

            var httpContent = updateRequestModel.ToJsonHttpContent();

            //Act

            var response = await _client.PatchAsync($"requirements/{id}", httpContent);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var requirementDto = await JsonDeserializeHelper.DeserializeAsync<RequirementDto>(response);

            requirementDto.Quantity.Should().Be(updateRequestModel.Quantity);
        }

        [Fact]
        public async Task Edit_WithInvalidId_ReturnsNotFound()
        {
            //Arrange

            var client = _clientBuilder.Build();
            var storage = _storageBuilder.Build();
            var requirement = _requirementBuilder
                .WithClientId(client.Id)
                .WithStorageId(storage.Id)
                .Build();

            Seed(client);
            Seed(storage);
            Seed(requirement);

            var id = Guid.NewGuid();

            var updateRequestModel = new UdpateRequirementRequestModel()
            {
                Quantity = 50
            };

            var httpContent = updateRequestModel.ToJsonHttpContent();

            //Act

            var response = await _client.PatchAsync($"requirements/{id}", httpContent);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Edit_WithInvalidQuantity_ReturnsBadRequest()
        {
            //Arrange

            var client = _clientBuilder.Build();
            var storage = _storageBuilder.Build();
            var requirement = _requirementBuilder
                .WithClientId(client.Id)
                .WithStorageId(storage.Id)
                .Build();

            Seed(client);
            Seed(storage);
            Seed(requirement);

            var id = requirement.Id;

            var updateRequestModel = new UdpateRequirementRequestModel()
            {
                Quantity = -50
            };

            var httpContent = updateRequestModel.ToJsonHttpContent();

            //Act

            var response = await _client.PatchAsync($"requirements/{id}", httpContent);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
