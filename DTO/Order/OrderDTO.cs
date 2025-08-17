namespace Restaurant.DTO
{
    public record OrderRequest(
        long Id,
        long ItemId,
        long Quantity,
        DateTime ReceiptDate
    );
    public record ManagerOrderRequestForCreate(
        long UserId,
        long ItemId,
        long Quantity,
        DateTime ReceiptDate
    );
    public record OrderRequestForCreate(
        long ItemId,
        long Quantity,
        DateTime ReceiptDate
    );
    public record OrderResponse(
        long Id,
        string ItemName,
        string Category,
        float Price,
        long Quantity,
        DateTime RequestDate,
        DateTime ReceiptDate,
        float Delivery
    );
    public record ManagerOrderResponse(
        long Id,
        string ItemName,
        string Category,
        float Price,
        long Quantity,
        DateTime RequestDate,
        DateTime ReceiptDate,
        float Delivery,
        long UserId,
        string Name,
        string Email,
        string Phone,
        string Address
    );
}