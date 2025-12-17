using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class TariffDto
    {
        [Required]
        [StringLength( 100 )]
        public string Name { get; set; }

        [Required]
        [StringLength( 20 )]
        public decimal DailyRate { get; set; }

        public TariffDto( string name, decimal dailyRate )
        {
            Name = name;
            DailyRate = dailyRate;
        }
    }

    public class TariffCreateDto : TariffDto
    {
        public TariffCreateDto( string name, decimal dailyRate )
            : base( name, dailyRate )
        {
        }
    }

    public class TariffUpdateDto : TariffDto
    {
        public TariffUpdateDto( string name, decimal dailyRate )
            : base( name, dailyRate )
        {
        }
    }

    public class TariffReadDto : TariffDto
    {
        public int Id { get; set; }

        public TariffReadDto( int id, string name, decimal dailyRate )
            : base( name, dailyRate )
        {
            Id = id;
        }
    }
}