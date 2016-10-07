
using FluentValidation;
using MB.Data.DTO;

namespace MB.Data.Validator
{
    public class MemberValidator : AbstractValidator<MemberDTO>
    {
        public MemberValidator()
        {
            RuleFor(member => member.UserName).NotEmpty().WithMessage("用户名不能为空");
        }
    }
}

