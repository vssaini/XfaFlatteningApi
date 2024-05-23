namespace XfaFlatteningApi.Models;

public record ErrorResponse(int StatusCode, string Message, string Detail);