namespace FrogGameAPI.Models.DTO
{
    public class ResponseDTO
    {
        public object? Data { get; set; } //
        public bool IsSuccessed { get; set; } = true;
        public string Message { get; set; } = "";
    }
}
