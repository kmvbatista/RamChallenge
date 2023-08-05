using System.Text.RegularExpressions;
using FluentValidation;

public class LinkValidator : AbstractValidator<Link>
{
    public LinkValidator()
    {
        RuleFor(d => d)
          .Must(link => IsLinkValid(link)).WithMessage("The link is not valid");
    }

    private bool IsLinkValid(Link link)
    {
        string pattern = @"https?://[\w\.\-]+(/[^\s]*)?";
        var result = Regex.IsMatch(link.Url, pattern);
        return result;
    }

}