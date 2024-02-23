namespace InventoryManagementSystem.Domain.DTOs.Response
{
    public class BaseResponse
    {
        public string? Message { get; set; }
        public bool IsSucceeded { get; set; } = false;
    }
}
