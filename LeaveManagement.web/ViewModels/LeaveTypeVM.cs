using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.web.ViewModels
{
    public class LeaveTypeVM
    {
        public int Id { get; set; }
        [Display(Name ="نام")]
        [Required(ErrorMessage ="فیلد {0}الزامی است ")]
        [StringLength(100,ErrorMessage ="فیلد {0} نمیتواند بیشتر از {1} و کمتر از {2} باشد",MinimumLength =3)]
        public string Name { get; set; }
        [Display(Name ="تعداد روز ")]
        [Required(ErrorMessage ="فیلد {0}الزامی است ")]
        public int DefaultDays { get; set; }
    }
}
