using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection11_Tests.Models.ForJson2
{
    public record OrderData(
    string OrderId,
    string CreatedAt,
    Customer Customer,
    List<Item> Items,
    Payment Payment,
    Delivery Delivery,
    Summary Summary
);

    public record Customer(
        int Id,
        string Name,
        string Email,
        string Phone,
        Address Address
    );

    public record Address(
        string Country,
        string City,
        string Street,
        string Zip
    );

    public record Item(
        int ProductId,
        string Name,
        string Category,
        int Quantity,
        decimal Price
    );

    public record Payment(
        string Method,
        string Status,
        string TransactionId
    );

    public record Delivery(
        string Type,
        string Status,
        string EstimatedDate,
        string TrackingNumber
    );

    public record Summary(
        decimal ItemsTotal,
        decimal DeliveryFee,
        decimal Discount,
        decimal FinalTotal
    );

}
