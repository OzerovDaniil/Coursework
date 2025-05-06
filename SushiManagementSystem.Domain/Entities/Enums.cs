namespace SushiManagementSystem.Domain.Entities
{
    public enum OrderStatus
    {
        Pending,
        InProgress,
        Completed,
        Cancelled
    }

    public enum DeliveryStatus
    {
        Pending,
        InTransit,
        Delivered,
        Failed
    }
}