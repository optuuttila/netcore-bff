using System.ComponentModel.DataAnnotations;

namespace netcore_data_access.Models {
    public class RubberDuck {
        public int Id { get; set; }

        [Required] 
        public string Name { get; set; }

        [Required] 
        public string Color { get; set; }
    }
}