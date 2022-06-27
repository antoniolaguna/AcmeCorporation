using System.ComponentModel.DataAnnotations;

namespace AcmeCorporationApi.DTOs
{
    public class PersonDTO
    {
        public string Name { get; set; }

        [Range(18, 120)]
        public int Age { get; set; }
        public string Document { get; set; }

        [DocumentTypeAttribute(new string[] { "DNI", "NIE", "CIF" })]
        public string DocumentType { get; set; }
    }


    public class DocumentTypeAttribute : ValidationAttribute
    {
        private string[] _allowedValues;

        public DocumentTypeAttribute(string[] allowedValues)
        {
            _allowedValues = allowedValues;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            foreach(string s in _allowedValues)
            {
                if (s.Equals(value.ToString()))
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("Document Type not allowed: "+value.ToString());
            
        }
    }
}
