namespace DTO.Base;

public class ModelApiBasic
{
    public bool Success { get; set; }

    public int StatusCode { get; set; }

    public string? Message { get; set; }

    public object? Result { get; set; }
}