namespace EZCommerce.Common.Extensions
{
    public static class ErrorExtensions
    {
        public static string ToErrorMessage(this List<Error> errors)
        {
            return string.Join("\n", errors.Select(e => $"{e.Code}: {e.Message}"));
        }
    }
}
