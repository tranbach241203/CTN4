﻿using CTN4_Data.Models;
using CTN4_Data.Models.DB_CTN4;

namespace CTN4_View.Areas.Admin.Viewmodel
{
    public class ThieuxkViewAdmin
    {
        public List<HoaDon> hoaDons { get; set; }
        public HoaDon HoaDon { get; set; }
        public List<HoaDonChiTiet> hoaDonChiTiets { get; set; }
        public int soLuongTong { get; set; }
        public float TongTienHang { get; set; }
        public List<LichSuDonHang> LichSuHoaDon { get;set; }

    }
}
