﻿using System.ComponentModel.DataAnnotations;

namespace CTN4_Data.Models.DB_CTN4
{
    public class KhuyenMai
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống.")]
        [StringLength(30, ErrorMessage = "Không được quá 30 ký tự")]
        public string MaKhuyenMai { get; set; }
        [Required(ErrorMessage = " không được bỏ trống.")]
        [StringLength(10, ErrorMessage = "Không được quá 10 ký tự")]
        [Range(0, 100, ErrorMessage = "Phần trăm giảm giá phải nằm trong khoảng từ 0 đến 100.")]
        public int PhanTramGiamGia { get; set; }
        public DateTime NgayBatDau { get; set; }
        [EndGreaterThanStart(ErrorMessage = "Ngày kết thúc phải lớn hơn ngày bắt đầu.")]
        public DateTime NgayKetThuc { get; set; }
        [Required(ErrorMessage = " không được bỏ trống.")]
        [StringLength(30, ErrorMessage = "Không được quá 30 ký tự")]
        [Range(0, float.MaxValue, ErrorMessage = "Số tiền giảm phải lớn hơn hoặc bằng 0.")]
        public float SoTienGiam { get; set; }
        [Required(ErrorMessage = " không được bỏ trống.")]
        [StringLength(30, ErrorMessage = "Không được quá 30 ký tự")]
        [Range(0, float.MaxValue, ErrorMessage = "Đồng giá phải lớn hơn hoặc bằng 0.")]
        public float DongGia { get; set; }
        public bool TrangThai { get; set; }

        public bool? Mua1tang1 { get; set; }

        public string? Ghichu { get; set; }
        public bool Is_Detele { get; set; }

        public virtual List<KhuyenMaiSanPham>? KKhuyenMaiSanPhams { get; set; }
        public virtual List<KhuyenMaiPhanLoai>? KKhuyenMaiPhanLoais { get; set; }
    }
    public class EndGreaterThanStart : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var khuyenMai = (KhuyenMai)validationContext.ObjectInstance;

            if (khuyenMai.NgayKetThuc <= khuyenMai.NgayBatDau)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}