# Result Class

The Result Class is a small concept and playground implementation created for experimentation purposes. It is not intended for practical use in real-world applications.

## About

The `Result<TOk, TError>` class provides a basic structure for working with results that can have either successful or error outcomes. It contains two private record types: `Ok` for representing successful results and `Error` for representing error results.

## Class Description

The `Result<TOk, TError>` class offers several methods and properties to handle and extract the underlying values based on the result type.

### Example Usage

```cs
Result<int, string> DivideNumbers(int dividend, int divisor)
{
    if (divisor != 0)
    {
        return dividend / divisor;
    }
    else
    {
        return "Cannot divide by zero";
    }
}

string message = DivideNumbers(10, 2)
    .Match(
        value => $"Division result: {value}",
        error => $"Error occurred: {error}"
    );
```

### Public Methods

- `bool IsOk()`: Checks if the result is a successful value.
- `bool IsError()`: Checks if the result is an error value.
- `T Match<T>(Func<TOk, T> ok, Func<TError, T> error)`: Executes the appropriate function based on the result type.
- `TOk GetValue()`: Retrieves the successful value if the result is successful; otherwise, throws an exception.
- `TError GetError()`: Retrieves the error value if the result is an error; otherwise, throws an exception.
- `bool TryGetValue(out TOk? value)`: Attempts to retrieve the successful value, returning `true` if successful or `false` if the result is an error.
- `bool TryGetError(out TError? error)`: Attempts to retrieve the error value, returning `true` if an error occurred or `false` if the result is successful.
- `TOk ValueOr(TOk or)`: Retrieves the successful value if the result is successful; otherwise, returns a provided default value (`or`).
- `TError ErrorOr(TError or)`: Retrieves the error value if the result is an error; otherwise, returns a provided default value (`or`).

### Public Static Methods

- `static implicit operator Result<TOk, TError>(TOk value)`: Implicitly creates a successful result from a value.
- `static implicit operator Result<TOk, TError>(TError value)`: Implicitly creates an error result from a value.
- `static Result<TOk, TError> From(TOk value)`: Creates a successful result from a value.
- `static Result<TOk, TError> From(TError value)`: Creates an error result from a value.
