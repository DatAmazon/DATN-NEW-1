﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace giadinhthoxinh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblProductSize
    {
        [Display(Name = "Mã kích thước sản phẩm")]
        public int PK_iProductSizeID { get; set; }
        [Display(Name = "Mã sản phẩm")]
        public int FK_iProductID { get; set; }
        [Display(Name = "Kích thước")]
        [Required(ErrorMessage = "Vui lòng nhập kích thước")]
        public string sSizeName { get; set; }

        public virtual tblProduct tblProduct { get; set; }
    }
}