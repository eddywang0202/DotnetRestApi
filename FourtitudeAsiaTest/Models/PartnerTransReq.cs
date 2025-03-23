using System.ComponentModel.DataAnnotations;

namespace FourtitudeAsiaTest.Model
{
    public class PartnerTransReq
    {
    }

    public class SubmittrxmessageReq
    {
        [Required(ErrorMessage = "Partnerkey is required")]
        [StringLength(50, ErrorMessage = "Partnerkey cannot exceed 50 characters")]
        public string? Partnerkey { get; set; }

        [Required(ErrorMessage = "Partnerrefno is required")]
        [StringLength(50, ErrorMessage = "Partnerrefno cannot exceed 50 characters")]
        public string? Partnerrefno { get; set; }

        [Required(ErrorMessage = "Partnerpassword is required")]
        [StringLength(50, ErrorMessage = "Partnerpassword cannot exceed 50 characters")]
        public string? Partnerpassword { get; set; }

        [Required(ErrorMessage = "Totalamount is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Totalamount must be greater than 0.")]
        public long Totalamount { get; set; }

        public List<Itemdetail> Items { get; set; } = new List<Itemdetail>();

        [Required(ErrorMessage = "Timestamp is required")]
        public string? Timestamp { get; set; }

        [Required(ErrorMessage = "Sig is required")]
        public string? Sig { get; set; }
    }

    public class Itemdetail
    {
        [Required(ErrorMessage = "Partneritemref is required")]
        [StringLength(50, ErrorMessage = "Partneritemref cannot exceed 50 characters")]
        public string? Partneritemref { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 50 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Qty is required")]
        [Range(2, 5, ErrorMessage = "Qty must be between 2 and 5")]
        public int? Qty { get; set; }

        [Required(ErrorMessage = "Unitprice is required")]
        public long? Unitprice { get; set; }
    }
}
