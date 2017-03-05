namespace _88Studio.Resource
{
    public class ErrorMessageResource
    {
        public static string InternalError
        {
            get
            {
                return DatabaseResourceManager.GetString(null, "Internal Server Error!");
            }
        }
        public static string IsRequired
        {
            get
            {
                return DatabaseResourceManager.GetHtmlString(null, "The {0} is required.");
            }
        }
        public static string LengthLimitation
        {
            get
            {
                return DatabaseResourceManager.GetHtmlString(null, "The {0} must be between {2} and {1} characters long.");
            }
        }
        public static string EmailIsRequired
        {
            get
            {
                return DatabaseResourceManager.GetHtmlString(null, "Please enter your email");
            }
        }

        public static string EmailExists
        {
            get
            {
                return DatabaseResourceManager.GetHtmlString(null, "Email already exists");
            }
        }

        public static string EmailOrPhoneExists
        {
            get
            {
                return DatabaseResourceManager.GetHtmlString(null, "Email or Phone are already existed!");
            }
        }

        public static string EmailLength
        {
            get
            {
                return DatabaseResourceManager.GetHtmlString(null, "The {0} must be between {2} and {1} characters long.");
            }
        }

        public static string PasswordIsRequired
        {
            get
            {
                return DatabaseResourceManager.GetHtmlString(null, "Password is required");
            }
        }

        public static string PasswordLength
        {
            get
            {
                return DatabaseResourceManager.GetHtmlString(null, "The {0} must be between {2} and {1} characters long.");
            }
        }

        public static string NameIsRequired
        {
            get
            {
                return DatabaseResourceManager.GetHtmlString(null, "Please enter your name");
            }
        }

        public static string NameLength
        {
            get
            {
                return DatabaseResourceManager.GetHtmlString(null, "The {0} must be between {2} and {1} characters long.");
            }
        }

        public static string PhoneNumberIsRequired
        {
            get
            {
                return DatabaseResourceManager.GetHtmlString(null, "Please enter your phone number");
            }
        }

        public static string PhoneLength
        {
            get
            {
                return DatabaseResourceManager.GetHtmlString(null, "The {0} must be between {2} and {1} characters long.");
            }
        }

        public static string PublishResponseSuccessButImages
        {
            get
            {
                return DatabaseResourceManager.GetHtmlString(null, "Update Successfully but Upload Images Failed!");
            }
        }

        public static string UpdateSuccess
        {
            get
            {
                return DatabaseResourceManager.GetHtmlString(null, "Update Successfully!");
            }
        }

        public static string SubmitSuccess
        {
            get
            {
                return DatabaseResourceManager.GetHtmlString(null, "Submit Successfully!");
            }
        }

        public static string UnableToUpdateTo2dehands
        {
            get
            {
                return DatabaseResourceManager.GetHtmlString(null, "Unable to update ad on 2dehands.");
            }
        }

        public static string NoPermission
        {
            get
            {
                return DatabaseResourceManager.GetString(null, "You don't have enough permission!");
            }
        }

        public static string InvalidParams
        {
            get
            {
                return DatabaseResourceManager.GetString(null, "Invalid Parameters!");
            }
        }

        public static string InvalidEmail
        {
            get
            {
                return DatabaseResourceManager.GetHtmlString(null, "Invalid Email Address!");
            }
        }

        public static string InvalidBranch
        {
            get
            {
                return DatabaseResourceManager.GetHtmlString(null, "This branch does not exist, you need to create first!");
            }
        }

        public static string Add2dehandsUserSuccess
        {
            get
            {
                return DatabaseResourceManager.GetString(null, "Add user to 2dehands successfully.");
            }
        }

        public static string Add2dehandsUserFail
        {
            get
            {
                return DatabaseResourceManager.GetString(null, "Add user to 2dehands fail.");
            }
        }

        public static string LinkUserSuccess
        {
            get
            {
                return DatabaseResourceManager.GetString(null, "Link user successfully.");
            }
        }

        public static string LinkUserPending
        {
            get
            {
                return DatabaseResourceManager.GetString(null, "An email will be sent to the email address.");
            }
        }

        public static string Unauthorized
        {
            get
            {
                return DatabaseResourceManager.GetString(null, "Credentials for the bulk user account don't match.");
            }
        }

        public static string Forbidden
        {
            get
            {
                return DatabaseResourceManager.GetString(null, "Your account does not have the correct status.");
            }
        }

        public static string LinkUserNotFound
        {
            get
            {
                return DatabaseResourceManager.GetString(null, "Unable to link the account. User account is not found. Please check the email address and account ID");
            }
        }

        public static string LinkUserConflict
        {
            get
            {
                return DatabaseResourceManager.GetString(null, "Unable to link the account. User account is already linked with different partner.");
            }
        }

        public static string AddUserConflict
        {
            get
            {
                return DatabaseResourceManager.GetString(null, "User with the supplied e-mail address already exists.");
            }
        }

        public static string BadRequest
        {
            get
            {
                return DatabaseResourceManager.GetString(null, "Request will be served when the data you provided is incomplete or incorrect.");
            }
        }

        public static string AddUserNotFound
        {
            get
            {
                return DatabaseResourceManager.GetString(null, "Unable to add the account. Please check the email address.");
            }
        }

        public static string UpdateUserNotFound
        {
            get
            {
                return DatabaseResourceManager.GetString(null, "Unable to update the account. Please check the email address.");
            }
        }

        public static string UpdateUserConflict
        {
            get
            {
                return DatabaseResourceManager.GetString(null, "User with the supplied e-mail address already exists.");
            }
        }

        public static string InvalidPostalCode
        {
            get
            {
                return DatabaseResourceManager.GetString(null, "Postal Code is invalid, please enter at least 2 characters.");
            }
        }
        public static string SendEmailSuccess
        {
            get
            {
                return DatabaseResourceManager.GetString(null, "Send email successfully!");
            }
        }
    }
}
