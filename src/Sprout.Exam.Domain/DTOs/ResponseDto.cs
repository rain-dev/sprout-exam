namespace Sprout.Exam.Domain.DTOs
{
    public record ResponseDto<TResult>(TResult? Result)
        where TResult : class, new()
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
    }

    public record ListResponseDto<TResult>(TResult[]? Result)
        where TResult : class, new()
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
    }
}
