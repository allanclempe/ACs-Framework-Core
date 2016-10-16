using ACs.Misc;

namespace ACs.Framework.Web.Core.Infra
{
    public enum ExceptionMessage
    {
        [EnumStringValue("Email \"{0}\" already exists")]
        UserEmailAlreadyExists = 1,
        [EnumStringValue("The user has an invalid status or already activated.")]
        UserInvalidStatus = 2,
        [EnumStringValue("Invalid Token.")]
        InvalidToken = 3,
        [EnumStringValue("The entity wasn't found")]
        EntityNotFounded = 404,
        [EnumStringValue("Email or password is not valid")]
        LoginFailure = 5
    }
}
