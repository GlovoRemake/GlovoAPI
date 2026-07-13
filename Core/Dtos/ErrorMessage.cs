namespace Core.Dtos;

public sealed record ErrorMessage(
    string Field,
    string Message)
{
    public static ErrorMessage Create(
        string field,
        string message)
        => new(field, message);
}