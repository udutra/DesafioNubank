using FluentValidation;

namespace DesafioNubank.Application.DTO.Request.Cliente;

public class ClienteCreateDtoValidator: AbstractValidator<ClienteCreateDto>{
    public ClienteCreateDtoValidator(){
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(2, 100).WithMessage("O nome deve ter entre 2 e 100 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("E-mail inválido.")
            .Length(3, 150).WithMessage("O email deve ter entre 3 e 150 caracteres.");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .Length(6, 50).WithMessage("A senha deve ter entre 6 e 50 caracteres.");
    }
}