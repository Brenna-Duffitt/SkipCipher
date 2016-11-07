using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto___Final_Project
{
    class Permute
    {
        private const String EXCEPTION_INVALID_TEXT_KEY = "Exception::Invalid text key.";
        private const String EXCEPTION_IDK = "Exception::????.";

        private const String ALPHABET_FORMAT_24 = "ABCDEFGHIKLMNOPQRSTUVWXY";
        private const String ALPHABET_FORMAT_25 = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
        private const String ALPHABET_FORMAT_26 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private Schema _schema;
        private String _textKey;
        private String _permutedAlphabet;
        private String _formattedKey;

        public String PermutedAlphabet
        {
            get { return _permutedAlphabet; }
        }

        public Permute(String textKey, Schema schema)
        {
            bool invalid = false;

            foreach (char c in textKey)
                if (!Char.IsLetter(c))
                    invalid = true;

            if(invalid)
                throw new Exception(EXCEPTION_INVALID_TEXT_KEY);

            _textKey = textKey.ToUpper();
            _schema = schema;

            FormatTextKey();
            PermuteAlphabet();
        }

        private void FormatTextKey()
        {
            String temp = _textKey;

            if (_schema._alphabetSize == Schema.ALPHABET_SIZE_24)
                temp = FormatForAlphabet24(temp);

            else if (_schema._alphabetSize == Schema.ALPHABET_SIZE_25)
                temp = FormatForAlphabet25(temp);

            temp = StrRemDup(temp);
            _formattedKey = StrRev(temp);
        }

        private void PermuteAlphabet()
        {
            int strLoc = 0;
            String temp = "";
            String[,] box = new String[_schema._row, _schema._column];

            switch(_schema._alphabetSize)
            {
                case Schema.ALPHABET_SIZE_24:
                    temp = _formattedKey + ALPHABET_FORMAT_24;
                    break;

                case Schema.ALPHABET_SIZE_25:
                    temp = _formattedKey + ALPHABET_FORMAT_25;
                    break;

                case Schema.ALPHABET_SIZE_26:
                    temp = _formattedKey + ALPHABET_FORMAT_26;
                    break;

                default:
                    throw new Exception(EXCEPTION_IDK);
            }

            temp = StrRemDup(temp);

            if (temp.Length == _schema._alphabetSize)
            {
                for (int i = 0; i < _schema._row; ++i)
                {
                    for (int j = 0; j < _schema._column; ++j)
                    {
                        box[i, j] = temp[strLoc].ToString();
                        ++strLoc;
                    }
                }

                PermuteStringFromBox(box);
            }

            else
                throw new Exception(EXCEPTION_IDK);
        }

        void PermuteStringFromBox(String[,] box)
        {
            bool done = false, isEven = false;
            int currentCol = _schema._startIndex;
            String temp = "";

            if (_schema._column % 2 == 0)
                isEven = true;

            while(!done)
            {
                for(int i = 0; i < _schema._row; ++i)
                    temp += box[i, currentCol];

                currentCol += 2;

                if (currentCol == _schema._column + 1)
                    currentCol = (isEven ? 0 : 1);

                else if (currentCol == _schema._column)
                    currentCol = (isEven ? 1 : 0);

                if (currentCol == _schema._startIndex)
                    done = true;
            }

            _permutedAlphabet = temp;
        }

        private String FormatForAlphabet24(String key)
        {
            String temp = "";

            foreach (char c in key)
            {
                if (Char.ToLower(c) == 'i' || Char.ToLower(c) == 'j')
                    temp += "I";

                else if (Char.ToLower(c) == 'y' || Char.ToLower(c) == 'z')
                    temp += "Y";

                else if (Char.ToLower(c) != 'j' && Char.ToLower(c) != 'z')
                    temp += Char.ToUpper(c);
            }

            return temp;
        }

        private String FormatForAlphabet25(String key)
        {
            String temp = "";

            foreach (char c in key)
            {
                if (Char.ToLower(c) == 'i' || Char.ToLower(c) == 'j')
                    temp += "I";

                else if (Char.ToLower(c) != 'j')
                    temp += Char.ToUpper(c);
            }

            return temp;
        }

        private String StrRemDup(String text)
        {
            String temp = "";

            foreach (char c in text.Distinct<char>())
                temp += c;

            return temp;
        }

        private String StrRev(String text)
        {
            String temp = "";

            for (int i = text.Length - 1; i >= 0; --i)
                temp += text[i];

            return temp;
        }
    }
}
