using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace MudBlazor.Docs.Examples.ValidatorComponent
{
    public class CustomValidator : ComponentBase
    {
        private ValidationMessageStore _messages;

        [CascadingParameter] private EditContext EditContext { get; set; }

        [Inject] private IServiceProvider ServiceProvider { get; set; }

        protected override void OnInitialized()
        {
            _messages = new(EditContext);

            EditContext.EnableDataAnnotationsValidation(ServiceProvider);
        }

        public bool Validate(IEnumerable<ServerValidationError> errors)
        {
            _messages.Clear();

            foreach (var error in errors)
            {
                _messages.Add(EditContext.Field(error.FieldName), error.ErrorMessage);
            }

            return EditContext.Validate();
        }
    }

    public class ServerValidationError
    {
        public string FieldName { get; set; }

        public string ErrorMessage { get; set; }

        public ServerValidationError(string fieldName, string errorMessage)
        {
            FieldName = fieldName;
            ErrorMessage = errorMessage;
        }
    }
}
