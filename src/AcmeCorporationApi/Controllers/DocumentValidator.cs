using System;

namespace AcmeCorporationApi
{
    internal class DocumentValidator
    {
        private string documentType;
        private string document;

        public DocumentValidator(string documentType, string document)
        {
            this.documentType = documentType;
            this.document = document;
        }

        internal bool isValid()
        {
            bool isValid = false;
            if (this.documentType.Equals("DNI"))
            {

                isValid = isDNIValid(document);
                
            }
            else if (this.documentType.Equals("NIE"))
            {
                isValid = isNIEValid(document);
            }
            else if (this.documentType.Equals("CIF"))
            {
                isValid = isCIFValid(document);
            }
            else
            {
                isValid = false;
            }
            return isValid;
        }

        private bool isCIFValid(string document)
        {
            bool isValid = false;
            if (this.document.Length == 9)
            {
                string niePreLetter = document.Substring(0, 1);
                string nieNumber = document.Substring(1, 7);
                string niePostLetter = document.Substring(document.Length - 1, 1);

                var numbersValid = int.TryParse(nieNumber, out int nieInteger);
                if (!numbersValid)
                {
                    isValid = false;
                }
                else
                {
                    if (niePreLetter.Equals("K") || niePreLetter.Equals("L") || niePreLetter.Equals("M"))
                    {
                        isValid = true;
                    }
                    else
                    {
                        isValid = false;
                    }
                }
            }
            return isValid;
        }

        private bool isNIEValid(string document)
        {
            bool isValid = false;
            if (this.document.Length == 9)
            {
                string niePreLetter = document.Substring(0, 1);
                string nieNumber = document.Substring(1, 7);
                string niePostLetter = document.Substring(document.Length - 1, 1);

                var numbersValid = int.TryParse(nieNumber, out int nieInteger);
                if (!numbersValid)
                {
                    isValid = false;
                }
                else
                {
                    if(niePreLetter.Equals("X") || niePreLetter.Equals("Y") || niePreLetter.Equals("Z"))
                    {
                        isValid= true;
                    }
                    else
                    {
                        isValid = false;
                    }
                }
            }
            return isValid;
        }

        private bool isDNIValid(string document)
        {
            bool isValid = false;
            if (this.document.Length == 9)
            {
                string dniNumbers = document.Substring(0, document.Length - 1);
                string dniLeter = document.Substring(document.Length - 1, 1);
                var numbersValid = int.TryParse(dniNumbers, out int dniInteger);
                if (!numbersValid)
                {
                    isValid = false;
                }
                else
                {
                    if (CalculateDNILetter(dniInteger) != dniLeter)
                    {
                        isValid = false;
                    }
                    else
                    {
                        isValid = true;
                    }
                }
            }
            return isValid;
        }

        static string CalculateDNILetter(int dniNumbers)
        {
            string[] control = { "T", "R", "W", "A", "G", "M", "Y", "F", "P", "D", "X", "B", "N", "J", "Z", "S", "Q", "V", "H", "L", "C", "K", "E" };
            var mod = dniNumbers % 23;
            return control[mod];
        }
    }
}