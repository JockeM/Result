namespace Result;

public class InvalidUnwrapResultException : Exception
{
    public InvalidUnwrapResultException() : base("Attempted to unwrap an invalid result.") { }

    public InvalidUnwrapResultException(string message) : base(message) { }

    public InvalidUnwrapResultException(string message, Exception inner) : base(message, inner) { }

    protected InvalidUnwrapResultException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context
    ) : base(info, context) { }
}