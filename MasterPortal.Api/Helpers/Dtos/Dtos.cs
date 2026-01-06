namespace MasterPortal.Api.Helpers.Dtos
{
    public record UserDto(
        int Id,
        string FirstName,
        string LastName,
        string Email
    );

    public record NotificationDto(
        int Id,
        string Title,
        string Message,
        DateTime? CreatedAt,
        int UserId
    );

    public record ProductsDto(ProductDto[] Products);

    public record ProductDto(
    int Id,
    string Title,
    string Description,
    string Category,
    double Price,
    double DiscountPercentage,
    double Rating,
    int Stock,
    List<string> Tags,
    string Brand,
    string Sku,
    double Weight,
    DimensionsDto Dimensions,
    string WarrantyInformation,
    string ShippingInformation,
    string AvailabilityStatus,
    List<ReviewDto> Reviews,
    string ReturnPolicy,
    int MinimumOrderQuantity,
    MetaDto Meta,
    List<string> Images,
    string Thumbnail
);

    public record DimensionsDto(
        double Width,
        double Height,
        double Depth
    );

    public record ReviewDto(
        int Rating,
        string Comment,
        DateTime Date,
        string ReviewerName,
        string ReviewerEmail
    );

    public record MetaDto(
        DateTime CreatedAt,
        DateTime UpdatedAt,
        string Barcode,
        string QrCode
    );
}
