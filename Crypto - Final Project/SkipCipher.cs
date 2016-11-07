using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto___Final_Project
{
    class SkipCipher
    {
        private const String EXCEPTION_INVALID_CIPHER_TEXT = "Exception::Invalid cipher text.";
        private const String EXCEPTION_INVALID_CIPHER_SIZE = "Exception::Invalid cipher size.";

        private const String DYNAMIC_ALPHABET_24 = "YMXLWKVIUHTGSFREQDPCOBNA";
        private const String DYNAMIC_ALPHABET_25 = "NZMYLXKWIVHUGTFSERDQCPBOA";
        private const String DYNAMIC_ALPHABET_26 = "ZMYLXKWJVIUHTGSFREQDPCOBNA";

        //when mode = true, encrypt; else, if mode = false, decrypt
        private bool _mode;
        private String _text;

        public SkipCipher()
        {
            _mode = true;
        }

        //target = permuted alphabet
        //source = dynamic alphabet
        private String PerformCipher(String targetAlphabet, String sourceAlphabet)
        {
            bool iFlag = false, yFlag = false;
            char newC = '\0';
            int index = 0, alphaLength = targetAlphabet.Length;
            String temp = "", newAlpha = "";

            foreach (char c in _text)
            {
                newC = '\0';

                //only enter this process if the character is a letter
                if (Char.IsLetter(c))
                {
                    if (c == 'J' && (alphaLength == Schema.ALPHABET_SIZE_24 || alphaLength == Schema.ALPHABET_SIZE_25))
                        newC = 'I';

                    else if (c == 'Z' && alphaLength == Schema.ALPHABET_SIZE_24)
                        newC = 'Y';

                    //Looks a bit confusing, but here is the explanation:
                    //if the mode is set to encrypt, look for the index in the source alphabet and get the encrypted letter from the target alphabet
                    //if the mode is set to decrypt, look for the index in the target alphabet and get the decrypted letter from the source alphabet
                    index = (_mode == true ? sourceAlphabet.IndexOf((newC != '\0' ? newC : c)) : targetAlphabet.IndexOf((newC != '\0' ? newC : c)));
                    newC = (_mode == true ? targetAlphabet[index] : sourceAlphabet[index]);

                    //if the alphabet size is 24, perform the check for I/J or Y/Z processing
                    if (targetAlphabet.Length == Schema.ALPHABET_SIZE_24)
                    {
                        if (newC == 'I')
                        {
                            //flip flop the value in here for the specified flag value
                            newC = (!iFlag ? 'I' : 'J');
                            //flip the flag
                            iFlag = !iFlag;
                        }

                        else if (newC == 'Y')
                        {
                            newC = (!yFlag ? 'Y' : 'Z');
                            yFlag = !yFlag;
                        }
                    }

                    //if the alphabet size is 25, perform the check for I/J processing
                    else if (targetAlphabet.Length == Schema.ALPHABET_SIZE_25)
                    {
                        if (newC == 'I')
                        {
                            newC = (!iFlag ? 'I' : 'J');
                            iFlag = !iFlag;
                        }
                    }

                    //append the substring from the beginning to the index obtained
                    if (index > 0)
                        newAlpha += sourceAlphabet.Substring(0, index);

                    //append the substring that starts the next character after the index
                    if (index + 1 < sourceAlphabet.Length)
                        newAlpha += sourceAlphabet.Substring(index + 1);

                    //append the recently used character
                    newAlpha += sourceAlphabet[index];
                    //save the shifted alphabet as the new source alphabet
                    sourceAlphabet = newAlpha;
                    newAlpha = "";
                }

                //if newC has a value, use newC
                //else use c (used to append spaces)
                temp += (newC != '\0' ? newC : c);
            }

            return temp;
        }

        public String Encrypt(String textKey, String numericKey, String text)
        {
            String cipherText = "";

            Schema schema = new Schema(numericKey);
            Permute permute = new Permute(textKey, schema);

            if (ValidateText(text))
            {
                _mode = true;
                cipherText = SelectCipherSize(schema, permute);
            }

            else
                throw new Exception(EXCEPTION_INVALID_CIPHER_TEXT);

            return cipherText;
        }

        public String Decrypt(String textKey, String numericKey, String text)
        {
            String plainText = "";

            Schema schema = new Schema(numericKey);
            Permute permute = new Permute(textKey, schema);

            if (ValidateText(text))
            {
                _mode = false;
                plainText = SelectCipherSize(schema, permute);
            }

            else
                throw new Exception(EXCEPTION_INVALID_CIPHER_TEXT);

            return plainText;
        }

        private String SelectCipherSize(Schema schema, Permute permute)
        {
            String plainText = "";

            if (schema._alphabetSize == Schema.ALPHABET_SIZE_24)
                plainText = PerformCipher(permute.PermutedAlphabet, DYNAMIC_ALPHABET_24);

            else if (schema._alphabetSize == Schema.ALPHABET_SIZE_25)
                plainText = PerformCipher(permute.PermutedAlphabet, DYNAMIC_ALPHABET_25);

            else if (schema._alphabetSize == Schema.ALPHABET_SIZE_26)
                plainText = PerformCipher(permute.PermutedAlphabet, DYNAMIC_ALPHABET_26);

            else
                throw new Exception(EXCEPTION_INVALID_CIPHER_TEXT);

            return plainText;
        }

        bool ValidateText(String text)
        {
            String temp = "";

            foreach (char c in text)
            {
                if (Char.IsLetter(c) || c == ' ')
                {
                    temp += c;
                }

                else if (c == '.')
                    temp += " STOP ";

                else if (c == ',')
                    temp += " PAUSE ";

                else if (c == '?')
                    temp += " QUESTION ";

                else if (c == '!')
                    temp += " EXCLAMATION ";
            }

            _text = temp.ToUpper();

            return (temp == null ? false : true);
        }
    }
}
