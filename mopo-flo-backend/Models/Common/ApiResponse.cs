namespace violet.backend.Models.Common;

public record ApiResponse<T>(string Message, int Code, bool IsSuccess, T Data);