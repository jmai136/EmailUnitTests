using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Email
{
    public static class EmailValidator
    {
        public static void Main(string[] args)
        {

        }

        // https://huddle.zendesk.com/hc/en-us/articles/360001092034-allowedSpecialCharacters-characters-not-supported-in-email-addresses
        public static bool IsValidEmail(string email)
        {
            /*
             * Is not null or empty
             * Contains an "@" symbol
             * No duplicate "@"
             * Has valid characters before and after "@"
             */

            // Soft hyphens are invisible??
            if (
                String.IsNullOrEmpty(email) 
                || !email.Contains("@")
                )
                return false;

            int atPosition = email.IndexOf("@");

            if (email.IndexOf("@", atPosition + 1) != -1)
                return false;

            string[] subemails = email.Split('@');
            string name = subemails[0];
            string domain = subemails[1];

            // Username must be minimum 1 character and <= 100
            if (name.Length < 1 || name.Length > 100)
                return false;

            // Domain must have ., <= 100 and >= 3 characters
            if (!domain.Contains(".") || domain.Length > 100 || domain.Length < 3)
                return false;

            // No non-alpha characters present at beginning or end of email
            char
                firstCharacter = name[0],
                lastCharacter = domain[domain.Length - 1];

            if (!(firstCharacter >= 65 && firstCharacter <= 90) && !(firstCharacter >= 97 && firstCharacter <= 122))
                return false;

            if (!(lastCharacter >= 65 && lastCharacter <= 90) && !(lastCharacter >= 97 && lastCharacter <= 122))
                return false;

            string allowedSpecialCharacters = "~!$%^&*_=+}{'?-.";

            foreach (char letter in name)
            {
                // Excludes characters including ones on the list which would be international
                if (letter > 126 || letter < 33)
                    return false;

                // 0-9
                if (letter >= 48 && letter <= 57)
                    continue;
                
                // A-Z
                if (letter >= 65 && letter <= 90)
                    continue;

                // a-z
                if (letter >= 97 && letter <= 122)
                    continue;

                // Check for the allowedSpecialCharacters characters
                foreach (char character in allowedSpecialCharacters)
                {
                    if (allowedSpecialCharacters.IndexOf(character) == allowedSpecialCharacters.Length - 1)//If the allowedSpecialCharacters character we're looking at is ".", the last one
                        if (letter != character)//If the letter in name is not "."
                            return false;

                    if (letter == character)
                        break;
                }
            }

            foreach (char letter in domain)
            {
                // Excludes characters including ones on the list which would be international
                if (letter > 126 || letter < 33)
                    return false;

                // 0-9
                if (letter >= 48  && letter <= 57)
                    continue;

               // A-Z
               if (letter >= 65 && letter <= 90)
                    continue;

                // a-z
                if (letter >= 97 && letter <= 122)
                    continue;

                // Check for the allowedSpecialCharacters characters
                foreach (char character in allowedSpecialCharacters)
                {
                    if (allowedSpecialCharacters.IndexOf(character) == allowedSpecialCharacters.Length - 1)
                        if (letter != character)
                            return false;

                    if (letter == character)
                        break;
                }
            }

            return true;
        }
    }
}
