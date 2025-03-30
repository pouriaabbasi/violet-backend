namespace violet.backend.Models.Common;

public record TableResponse<T>(List<T> Data, int Page, int Total);