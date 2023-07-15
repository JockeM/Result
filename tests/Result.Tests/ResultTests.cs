using System;
using FluentAssertions;
using Xunit;

namespace Result.Tests;

public class ResultTests
{
    [Fact]
    public void IsOk_WhenResultIsOk_ShouldReturnTrue()
    {
        var result = Result<int, string>.From(42);

        var isOk = result.IsOk();

        isOk.Should().BeTrue();
    }

    [Fact]
    public void IsOk_WhenResultIsError_ShouldReturnFalse()
    {
        var result = Result<int, string>.From("Error");

        var isOk = result.IsOk();

        isOk.Should().BeFalse();
    }

    [Fact]
    public void IsError_WhenResultIsError_ShouldReturnTrue()
    {
        var result = Result<int, string>.From("Error");

        var isError = result.IsError();

        isError.Should().BeTrue();
    }

    [Fact]
    public void IsError_WhenResultIsOk_ShouldReturnFalse()
    {
        var result = Result<int, string>.From(42);

        var isError = result.IsError();

        isError.Should().BeFalse();
    }

    [Fact]
    public void Match_WhenResultIsOk_ShouldInvokeOkFunc()
    {
        var result = Result<int, string>.From(42);
        var expectedValue = 42;

        var value = result.Match(ok: x => x, failed: error => -1);

        value.Should().Be(expectedValue);
    }

    [Fact]
    public void Match_WhenResultIsError_ShouldInvokeErrorFunc()
    {
        var result = Result<int, string>.From("Error");
        var expectedValue = -1;

        var value = result.Match(ok: x => x, failed: error => expectedValue);

        value.Should().Be(expectedValue);
    }

    [Fact]
    public void GetValue_WhenResultIsOk_ShouldReturnOkValue()
    {
        var result = Result<int, string>.From(42);
        var expectedValue = 42;

        var value = result.GetValue();

        value.Should().Be(expectedValue);
    }

    [Fact]
    public void GetValue_WhenResultIsError_ShouldThrowInvalidUnwrapResultException()
    {
        var result = Result<int, string>.From("Error");

        var action = new Action(() => result.GetValue());

        action.Should().Throw<InvalidUnwrapResultException>();
    }

    [Fact]
    public void GetError_WhenResultIsError_ShouldReturnErrorValue()
    {
        var result = Result<int, string>.From("Error");
        var expectedValue = "Error";

        var value = result.GetError();

        value.Should().Be(expectedValue);
    }

    [Fact]
    public void GetError_WhenResultIsOk_ShouldThrowInvalidUnwrapResultException()
    {
        var result = Result<int, string>.From(42);

        var action = new Action(() => result.GetError());

        action.Should().Throw<InvalidUnwrapResultException>();
    }

    [Fact]
    public void TryGetValue_WhenResultIsOk_ShouldReturnTrueAndAssignOkValue()
    {
        var result = Result<int, string>.From(42);
        var expectedValue = 42;

        var success = result.TryGetValue(out var value);

        success.Should().BeTrue();
        value.Should().Be(expectedValue);
    }

    [Fact]
    public void TryGetValue_WhenResultIsError_ShouldReturnFalseAndAssignDefaultValue()
    {
        var result = Result<int, string>.From("Error");
        var expectedValue = default(int);

        var success = result.TryGetValue(out var value);

        success.Should().BeFalse();
        value.Should().Be(expectedValue);
    }

    [Fact]
    public void TryGetError_WhenResultIsError_ShouldReturnTrueAndAssignErrorValue()
    {
        var result = Result<int, string>.From("Error");
        var expectedValue = "Error";

        var success = result.TryGetError(out var value);

        success.Should().BeTrue();
        value.Should().Be(expectedValue);
    }

    [Fact]
    public void TryGetError_WhenResultIsOk_ShouldReturnFalseAndAssignDefaultValue()
    {
        var result = Result<int, string>.From(42);
        var expectedValue = default(string);

        var success = result.TryGetError(out var value);

        success.Should().BeFalse();
        value.Should().Be(expectedValue);
    }

    [Fact]
    public void ValueOr_WhenResultIsOk_ShouldReturnOkValue()
    {
        var result = Result<int, string>.From(42);
        var expectedValue = 42;
        var defaultValue = 0;

        var value = result.ValueOr(defaultValue);

        value.Should().Be(expectedValue);
    }

    [Fact]
    public void ValueOr_WhenResultIsError_ShouldReturnDefaultValue()
    {
        var result = Result<int, string>.From("Error");
        var expectedValue = 0;
        var defaultValue = 0;

        var value = result.ValueOr(defaultValue);

        value.Should().Be(expectedValue);
    }

    [Fact]
    public void ErrorOr_WhenResultIsError_ShouldReturnErrorValue()
    {
        var result = Result<int, string>.From("Error");
        var expectedValue = "Error";
        var defaultValue = "Default";

        var value = result.ErrorOr(defaultValue);

        value.Should().Be(expectedValue);
    }

    [Fact]
    public void ErrorOr_WhenResultIsOk_ShouldReturnDefaultValue()
    {
        var result = Result<int, string>.From(42);
        var expectedValue = "Default";
        var defaultValue = "Default";

        var value = result.ErrorOr(defaultValue);

        value.Should().Be(expectedValue);
    }
}