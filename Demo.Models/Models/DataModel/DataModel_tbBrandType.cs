using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.ComponentModel;

namespace Demo.Models
{
    /// <summary>
    /// 品牌頻道 - 產品類型
    /// </summary>
    [Validator(typeof(tbBrandTypeSaveValidator))]
    [PetaPoco.TableName("tbBrandType")]
    [PetaPoco.PrimaryKey("sID")]
    [PetaPoco.ExplicitColumns]
    public class DataModel_tbBrandType
    {
        [DisplayName("sID")]
        [PetaPoco.ResultColumn]
        public Int64 sID { get; set; }

        [DisplayName("產品類型名稱")]
        [PetaPoco.Column]
        public string sCaption { get; set; }

        [DisplayName("是否顯示")]
        [PetaPoco.Column]
        public bool sVisible { get; set; }

        [DisplayName("排序")]
        [PetaPoco.Column]
        public Int64 sSortid { get; set; }
    }

    // 存入驗證
    public class tbBrandTypeSaveValidator : AbstractValidator<DataModel_tbBrandType>
    {
        public tbBrandTypeSaveValidator()
        {
            RuleFor(c => c.sCaption)
                   .NotEmpty()
                   .Length(1, 20)
                   .WithName("產品類型名稱")
                   ;

            RuleFor(c => c.sVisible)
                   .NotNull()
                   .WithName("是否顯示")
                   ;

            RuleFor(c => c.sSortid)
                   .NotNull()
                   .WithName("排序")
                   ;
        }
    }
}
