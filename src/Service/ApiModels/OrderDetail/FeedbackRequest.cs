using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Service.ApiModels.OrderDetail;

public class FeedbackRequest
{
    [Description("OrderDetailId")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập đánh giá")]
    [Range(1, 5, ErrorMessage = "Đánh giá phải từ 1 đến 5")]
    public byte Rating { get; set; }

    [MaxLength(500, ErrorMessage = "Đánh giá không được vượt quá 500 ký tự")]
    public string? ReviewContent { get; set; }
}