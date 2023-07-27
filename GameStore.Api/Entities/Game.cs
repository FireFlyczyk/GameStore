using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Entities
{
    public class Game
    {

        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }
        [Required]
        [MaxLength(20)]
        public required string Genre { get; set; }
        [Required]
        [Range(1, 120)]
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        [Url]
        [StringLength(100)]
        public required string ImageUrl { get; set; }

    }

}