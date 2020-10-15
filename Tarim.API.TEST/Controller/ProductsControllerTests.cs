//
//  Author:
//    Nur Karluk ns.karluk@gmail.com
//
//  Copyright (c) 2020, (c) Tarim Lab
//
//  All rights reserved.
//
//  Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
//
//     * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in
//       the documentation and/or other materials provided with the distribution.
//     * Neither the name of the [ORGANIZATION] nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
//
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
//  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
//  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
//  A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
//  CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
//  EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
//  PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
//  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
//  LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
//  NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
//  SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Tarim.Api;
using Tarim.Api.Infrastructure.Model.Products;
using Xunit;

namespace Tarim.API.TEST.Controller
{
    public class ProductsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
    public ProductsControllerTests(WebApplicationFactory<Startup> factory)
    {
        _httpClient = factory.CreateDefaultClient(new Uri("https://localhost:5001/api/products/"));
    }

    [Fact]
    public async Task GetProducts_ReturnsSuccessStatusCode()
    {
        var response = await _httpClient.GetAsync("");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetProducts_ReturnsExpectedMediaType()
    {
        var response = await _httpClient.GetAsync("");
        Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
    }

    [Fact]
    public async Task GetProducts_ReturnsContent()
    {
        var response = await _httpClient.GetAsync("");
        Assert.NotNull(response.Content);
        Assert.True(response.Content.Headers.ContentLength > 0);
    }

    [Fact]
    public async Task GetProducts_ReturnsSuccess()
    {
        var response = await _httpClient.GetFromJsonAsync<IList<Product>>("");
        Assert.NotNull(response);
    }

    [Fact]
    public async Task GetTodaySpecial_ReturnsSuccessStatusCode()
    {
        var response = await _httpClient.GetAsync("today-special");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetTodaySpecial_ReturnsExpectedMediaType()
    {
        var response = await _httpClient.GetAsync("today-special");
        Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
    }

    [Fact]
    public async Task GetTodaySpecial_ReturnsContent()
    {
        var response = await _httpClient.GetAsync("today-special");
        Assert.NotNull(response.Content);
        Assert.True(response.Content.Headers.ContentLength > 0);
    }

    [Fact]
    public async Task GetTodaySpecial_ReturnsSuccess()
    {
        var response = await _httpClient.GetFromJsonAsync<IList<Product>>("today-special");
        Assert.NotNull(response);
        Assert.True(response.Count>0);
    }
}
}
