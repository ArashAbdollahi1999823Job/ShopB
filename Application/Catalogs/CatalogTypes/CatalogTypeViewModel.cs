using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Utilites;

namespace Application.Catalogs.CatalogTypes
{
    public class CatalogTypeViewModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = ErrorAndMessage.Required)]
        [Display(Name = ErrorAndMessage.CatalogName)]
        [MaxLength(3,ErrorMessage = ErrorAndMessage.MaxCharacter)]
        public string Type { get; set; }
        public int? ParentCatalogTypeId { get; set; }

    }
}
