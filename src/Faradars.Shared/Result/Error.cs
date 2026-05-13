namespace Faradars.Shared.Result;

public class Error(string code, string message)
{
    public string Code = code;
    public string Message = message;

    public override string ToString() => Code;

    // Generic
    public static readonly Error None = new(string.Empty, string.Empty);

    public static readonly Error Unknown =
        new("Error.Unknown", "An unexpected error occurred.");

    public static readonly Error InternalServerError =
        new("Error.InternalServerError", "An internal server error occurred. Please try again later.");

    public static readonly Error BadRequest =
        new("Error.BadRequest", "The request is invalid or cannot be processed.");

    public static readonly Error ValidatorNotFound =
        new("Error.ValidatorNotFound", "No validator was found for the requested operation.");

    public static Error FluentValidationError(string message)
        => new("Error.FluentValidation", message);

    // Authorization & Authentication
    public static readonly Error Unauthorized =
        new("Error.Unauthorized", "Authentication is required to perform this action.");

    public static readonly Error Forbidden =
        new("Error.Forbidden", "You do not have permission to access this resource.");

    public static readonly Error InvalidToken =
        new("Error.InvalidToken", "The provided token is invalid or has expired.");

    public static readonly Error InvalidJwtStructure =
        new("Error.InvalidJwtStructure", "The structure of the JWT token is invalid.");

    public static readonly Error UnexpectedAlgorithm =
        new("Error.UnexpectedAlgorithm", "The JWT signing algorithm does not match the expected algorithm.");

    public static readonly Error MissingOrInvalidExp =
        new("Error.MissingOrInvalidExp", "The 'exp' claim is missing or contains an invalid value.");

    public static readonly Error AccessTokenHasNotExpired =
        new("Error.AccessTokenHasNotExpired", "The access token is still valid and has not expired.");

    public static readonly Error InvalidRefreshToken =
        new("Error.InvalidRefreshToken", "The refresh token is invalid. Please log in again.");

    public static readonly Error EmailAlreadyVerified =
        new("Error.EmailAlreadyVerified", "This email address has already been verified.");

    public static readonly Error PhoneAlreadyVerified =
        new("Error.PhoneAlreadyVerified", "This phone number has already been verified.");

    public static readonly Error CodeNotValid =
        new("Error.CodeNotValid", "The verification code is invalid or does not match.");

    // User-related
    public static readonly Error UserNotFound =
        new("Error.UserNotFound", "No user was found with the provided credentials.");

    public static readonly Error UserAlreadyExists =
        new("Error.UserAlreadyExists", "A user with the provided information already exists.");

    public static readonly Error WrongPassword =
        new("Error.WrongPassword", "The provided password is incorrect.");

    public static readonly Error UnauthorizedToModifyManager =
        new("Error.UnauthorizedToModifyManager", "Manager accounts cannot be modified through this operation.");

    public static readonly Error UserAlreadyInAdminRole =
        new("Error.UserAlreadyInAdminRole", "The user is already assigned to the Admin role.");

    public static readonly Error UserAlreadyInUserRole =
        new("Error.UserAlreadyInUserRole", "The user is already assigned to the User role.");

    public static readonly Error UserIdDoesNotExist =
        new("Error.UserIdDoesNotExist", "No user exists with the provided ID.");

    public static readonly Error PhoneAlreadyExists =
        new("Error.PhoneAlreadyExists", "This phone number is already registered. Please use another number or log in.");

    public static readonly Error EmailAlreadyExists =
        new("Error.EmailAlreadyExists", "This email address is already registered.");

    // OTP
    public static readonly Error InvalidOtpCode =
        new("Error.InvalidOtpCode", "The OTP code is invalid or has expired.");

    public static readonly Error ValidOtpCodeNotFound =
        new("Error.ValidOtpCodeNotFound", "No valid OTP code could be found.");

    // Resource existence
    public static readonly Error NotFound =
        new("Error.NotFound", "The requested resource could not be found.");

    public static readonly Error ItemIdDoesNotExist =
        new("Error.ItemIdDoesNotExist", "No item exists with the provided ID.");

    public static readonly Error CategoryIdDoesNotExist =
        new("Error.CategoryIdDoesNotExist", "No category exists with the provided ID.");

    public static readonly Error PartOfSpeechIdDoesNotExist =
        new("Error.PartOfSpeechIdDoesNotExist", "No part of speech exists with the provided ID.");

    public static readonly Error VocabularyLevelIdDoesNotExist =
        new("Error.VocabularyLevelIdDoesNotExist", "No vocabulary level exists with the provided ID.");

    public static readonly Error VocabularyIdDoesNotExist =
        new("Error.VocabularyIdDoesNotExist", "No vocabulary exists with the provided ID.");

    public static readonly Error VocabularyStatusDoesNotExist =
        new("Error.VocabularyStatusDoesNotExist", "No vocabulary status exists with the provided ID.");

    public static readonly Error StoryIdDoesNotExist =
        new("Error.StoryIdDoesNotExist", "No story exists with the provided ID.");

    public static readonly Error StoryDoesNotHaveAnyPages =
        new("Error.StoryDoesNotHaveAnyPages", "The story does not contain any pages.");

    public static readonly Error FeatureIdDoesNotExist =
        new("Error.FeatureIdDoesNotExist", "No feature exists with the provided ID.");

    public static readonly Error SubscriptionPlanIdDoesNotExist =
        new("Error.SubscriptionPlanIdDoesNotExist", "No subscription plan exists with the provided ID.");

    public static readonly Error InvalidId =
        new("Error.InvalidId", "The provided ID is invalid.");

    public static readonly Error ParentNotFound =
        new("Error.ParentNotFound", "The specified parent category does not exist.");

    public static readonly Error HasUsage =
        new("Error.HasUsage", "The resource cannot be modified or deleted because it is referenced elsewhere.");

    // Vocabulary
    public static readonly Error DuplicateVocabularyStatus =
        new("Error.DuplicateVocabularyStatus", "A vocabulary status for this user already exists.");

    public static readonly Error DuplicateBookmark =
        new("Error.DuplicateBookmark", "This vocabulary item is already bookmarked by the user.");

    public static readonly Error CannotChangeVocabularyStatusState =
        new("Error.CannotChangeVocabularyStatusState", "The current vocabulary status cannot be changed.");

    public static readonly Error VocabularyIsAlreadyInReviewList =
        new("Error.VocabularyAlreadyInReviewList", "This vocabulary item is already in the review list.");

    public static readonly Error InvalidReviewQuality =
        new("Error.InvalidReviewQuality", "Review quality must be a number between 0 and 5.");

    // Extra useful errors for Faradars
    public static readonly Error PaymentFailed =
        new("Error.PaymentFailed", "The payment process failed. Please try again.");

    public static readonly Error InsufficientBalance =
        new("Error.InsufficientBalance", "Your balance is insufficient to complete this purchase.");

    public static readonly Error CourseNotFound =
        new("Error.CourseNotFound", "The requested course could not be found.");

    public static readonly Error AlreadyEnrolledInCourse =
        new("Error.AlreadyEnrolledInCourse", "The user is already enrolled in this course.");

    public static readonly Error EnrollmentNotFound =
        new("Error.EnrollmentNotFound", "No enrollment was found for this user in the specified course.");

    public static readonly Error AlreadyExists =
        new("Error.AlreadyExists", "The resource you are trying to create or update already exists.");
}
