using System.Text.Json.Serialization;

namespace Core.Dtos;

public class Result
{
    [JsonPropertyName("success")]
    public bool IsSuccess { get; init; }

    [JsonIgnore]
    public IReadOnlyCollection<ErrorMessage> Errors { get; init; }
        = [];

    [JsonPropertyName("errors")]
    public Dictionary<string, string[]> ErrorsDictionary =>
        Errors
            .GroupBy(x => x.Field)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.Message).ToArray());

    public static Result Success() =>
        new()
        {
            IsSuccess = true
        };

    public static Result Failure(IEnumerable<ErrorMessage> errors) =>
        new()
        {
            IsSuccess = false,
            Errors = errors.ToArray()
        };

    public static Result Failure(params ErrorMessage[] errors) =>
        new()
        {
            IsSuccess = false,
            Errors = errors
        };
}



public class Result<T> : Result
{
    public T? Value { get; init; }

    public static Result<T> Success(T value) =>
        new()
        {
            IsSuccess = true,
            Value = value
        };

    public new static Result<T> Failure(IEnumerable<ErrorMessage> errors) =>
        new()
        {
            IsSuccess = false,
            Errors = errors.ToArray()
        };

    public new static Result<T> Failure(params ErrorMessage[] errors) =>
        new()
        {
            IsSuccess = false,
            Errors = errors
        };
}