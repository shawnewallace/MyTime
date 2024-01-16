using System.IO;

namespace MyTime.App.Abstractions;

public class Result
{
  private Result(bool isSuccess, Error error)
  {
    IsSuccess = isSuccess;
    Error = error;
  }

  public bool IsSuccess { get; }
  public bool IsFailure => !IsSuccess;

  public Error Error { get; }

  public static Result Success() => new(true, Error.None);
  public static Result Failure(Error error) => new(false, error);
}
