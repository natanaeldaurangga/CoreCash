using System;
using CoreCashApi.DTOs.Auth;
using CoreCashApi.Services;

namespace CoreCashTest;

public class UnitTest1
{
    private readonly AuthService _authService;

    public UnitTest1(AuthService authService)
    {
        _authService = authService;
    }

    [Fact]
    public async Task TestLoginAsync()
    {
        ResponseLogin response = await _authService.LoginAsync(new RequestLogin() { Email = "user1@example.com", Password = "Tester1234" }) ?? new ResponseLogin();
        string result = response.JwtToken!;
        Assert.NotNull(result);
    }

}