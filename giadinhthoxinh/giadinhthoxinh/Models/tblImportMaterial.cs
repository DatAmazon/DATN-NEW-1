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

    public partial class tblImportMaterial
    {
        [Display(Name = "Mã chi tiết nguyên liệu nhập")]
        public int PK_iImportMaterialID { get; set; }
        [Display(Name = "Mã hóa đơn nhập")]
        public int FK_iImportOrderID { get; set; }
        [Display(Name = "Mã nguyên liệu")]
        public int FK_iMaterialID { get; set; }
        [Display(Name = "Số lượng")]
        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        public Nullable<int> iQuatity { get; set; }
        [Display(Name = "Giá nhập")]
        [Required(ErrorMessage = "Vui lòng nhập giá nhập")]
        public Nullable<double> fPrice { get; set; }
    
        public virtual tblImportOrder tblImportOrder { get; set; }
        public virtual tblMaterial tblMaterial { get; set; }
    }
}