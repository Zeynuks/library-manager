using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class TariffDto
    {
        [Required]
        public int Id { get; init; }
        
        [Required]
        [StringLength( 100 )]
        public string Name { get; set; }

        [Required]
        [StringLength( 20 )]
        public decimal DailyRate { get; set; }

        public TariffDto( int id, string name, decimal dailyRate )
        {
            Id = id;
            Name = name;
            DailyRate = dailyRate;
        }
    }

    public class TariffCreateDto : TariffDto
    {
        public TariffCreateDto( int id, string name, decimal dailyRate )
            : base( id, name, dailyRate )
        {
        }
    }

    public class TariffUpdateDto : TariffDto
    {
        public TariffUpdateDto( int id, string name, decimal dailyRate )
            : base( id, name, dailyRate )
        {
        }
    }

    public class TariffReadDto : TariffDto
    {
        public TariffReadDto( int id, string name, decimal dailyRate )
            : base( id, name, dailyRate )
        {
        }
    }
}