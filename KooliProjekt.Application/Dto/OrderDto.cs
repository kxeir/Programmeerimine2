using System;

namespace KooliProjekt.Application.Dto;

public class OrderDto
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string OrderState { get; set; } = string.Empty;
    public int UserId { get; set; }
}
