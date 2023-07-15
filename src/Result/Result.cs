using System.Diagnostics.CodeAnalysis;

namespace Result;

public abstract record Result<TOk, TError>
{
    private record Ok(TOk Value) : Result<TOk, TError>;

    private record Error(TError Value) : Result<TOk, TError>;

    public bool IsOk() => this is Ok;

    public bool IsError() => this is Error;

    public T Match<T>(Func<TOk, T> ok, Func<TError, T> failed) =>
        this switch
        {
            Ok o => ok(o.Value),
            Error f => failed(f.Value),
            _ => throw new InvalidUnwrapResultException(),
        };

    public TOk GetValue() =>
        this switch
        {
            Ok o => o.Value,
            _ => throw new InvalidUnwrapResultException(),
        };

    public TError GetError() =>
        this switch
        {
            Error o => o.Value,
            _ => throw new InvalidUnwrapResultException(),
        };

    public bool TryGetValue([NotNullWhen(returnValue: true)] out TOk? value)
    {
        value = Match<TOk?>(ok => ok, error => default);
        return IsOk();
    }

    public bool TryGetError([NotNullWhen(returnValue: true)] out TError? error)
    {
        error = Match<TError?>(ok => default, error => error);
        return IsError();
    }

    public TOk ValueOr(TOk or) =>
        this switch
        {
            Ok o => o.Value,
            _ => or,
        };

    public TError ErrorOr(TError or) =>
        this switch
        {
            Error error => error.Value,
            _ => or,
        };

    public static implicit operator Result<TOk, TError>(TOk value) =>
        new Result<TOk, TError>.Ok(value);

    public static implicit operator Result<TOk, TError>(TError value) =>
        new Result<TOk, TError>.Error(value);

    public static Result<TOk, TError> From(TOk value) => value;

    public static Result<TOk, TError> From(TError value) => value;
}
