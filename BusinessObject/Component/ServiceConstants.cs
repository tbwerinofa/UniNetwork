using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObject
{
    public class DocumentTypeDiscriminator
    {
        public const string Trophy = "TrOp";
        public const string ProductImage = "PrIm";
        public const string BannerImage = "BaIm";
        public const string ASALicenseForm = "AsLi";
        public const string Constitution = "CoNs";
        public const string ProfilePic = "PrPc";
    }

    public class MessageTemplateConst
    {
        public const string AccountRegistration = "Account_Registration";
        public const string AccountConfirmation = "AccountConfirmation";
        public const string AccountCredential = "Account_Credential";
        public const string RegistrationNotification = "RegistrationNotification";
        public const string AccountSubscription = "AccountSubscription";
        public const string ApplicationRejection = "Application_Rejection";

    }


    public static class CrudError
    {
        public const string CreateErrorMsg = "Create Error!!";
        public const string ReadErrorMsg = "Read Error!!";
        public const string UpdateErrorMsg = "Update Error!!";
        public const string DeleteErrorMsg = "Delete Error!!";
        public const string SaveErrorMsg = "Save Error, Please try again!!";
    }

    public static class ConstEntity
    {
        public const string UniqueKeyMsg = "Record  details already exist.";
        public const string ForeignKeyDelMsg = "This Record  can not be deleted because it is mapped to another record !!";
        public const string MissingValueMsg = "This Record can not be saved because there is a missing value !!";

        public const string ArgumentNullException = "Etity  does not exist";

    }

    public class ApplicationRoleConst
    {
        public const string PlantOperation = "Plant Operations";
    }

    public class ValidateMessages
    {
        public const string DateRangeOverlap = "Date Range overlaps with an existing range.";
    }

    public class LkpStatusRef
    {
        public const string New = "NewP";
        public const string Fail = "Failed";
        public const string Processed = "Processed";
        public const string Verified = "Verified";
    }

    public class FrequencyDiscr
    {
        public const string CalendarMonth = "DcIm";
        public const string Remittance = "ReTi";
    }

    public class SqlStoredProc
    {
        public const string GetTrophyWinners = "accolade.web_GetTrophyWinners";
    }

    public class EventTypeDiscriminator
    {
        public const string Training = "TrNi";
    }

    public class OrderStatusRef
    {
        public const string New = "QoNw";
        public const string Invoice = "PePa";
        public const string Paid = "PaId";
    }

    public class NotifyStatusRef
    {
        public const string Complete = "COMPLETE";
    }

    public class EventTypeDiscr
    {
        public const string Training = "TrNi";
    }

}
