using System.ComponentModel.DataAnnotations;

namespace Searcher.BLL.DTO
{
    public class SearchRequestDto
    {
        [Required]
        public string KeyWord { get; set; }
    }
}
