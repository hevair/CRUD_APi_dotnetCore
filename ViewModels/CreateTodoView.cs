using System.ComponentModel.DataAnnotations;

namespace MeuTodo.ViewModels
{
    public class CreateTodoView{
        
        [Required]
        public string Title { get; set; }
    }    
}