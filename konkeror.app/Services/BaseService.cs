using AutoMapper;
using konkeror.app.Models;
using konkeror.app.Services.Interface;
using konkeror.data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konkeror.app.Services
{
    public abstract class BaseService 
    {
        protected void ValidateDateTimeValue(DateTime value, string fieldName, IList<ValidationMessage> validationMessages)
        {
            if (value.Equals(DateTime.MinValue))
                AddValidationMessage(validationMessages, $"{fieldName} {value} is not a valid date", $"Provide a valid date for {fieldName}");
        }
        protected void ValidateStringValue(string value, string fieldName, IList<ValidationMessage> validationMessages)
        {
            if (string.IsNullOrEmpty(value))
                AddValidationMessage(validationMessages, $"{fieldName} was not provided");
        }

        protected void ValidateIntValue(int value, string fieldName, IList<ValidationMessage> validationMessages)
        {
            if (value <= 0)
                AddValidationMessage(validationMessages, $"{fieldName} should be greater than zero");
        }
        protected void ValidateGuidValue(string value, string fieldName, IList<ValidationMessage> validationMessages)
        {
            ValidateStringValue(value, fieldName, validationMessages);

            if (!Guid.TryParse(value, out Guid theguid))
                AddValidationMessage(validationMessages, $"{fieldName} {value} is not a valid Id", $"Provide a value for {fieldName} in GUID format");
        }

        protected void AddValidationMessage(IList<ValidationMessage> theList, string message, string suggestion = "")
        {
            theList.Add(new ValidationMessage()
            {
                Message = message,
                Suggestion = suggestion
            });
        }
    }
}
