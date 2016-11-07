using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto___Final_Project
{
    class Schema
    {
        public const int ALPHABET_SIZE_24 = 24;
        public const int ALPHABET_SIZE_25 = 25;
        public const int ALPHABET_SIZE_26 = 26;

        private const String EXCEPTION_INVALID_NUMER_KEY = "Exception::Invalid numeric key.";
        private const String EXCEPTION_INVALID_NUMER_KEY_LENGTH = "Exception::Invalid numeric key length.";
        private const String EXCEPTION_INVALID_NUMER_KEY_START_INDEX = "Exception::Invalid numeric start index.";

        public int _row;
        public int _column;
        public int _startIndex;
        public int _alphabetSize;
        public String _schemaKey;

        public int Row
        {
            get { return _row; }
            set { _row = value; }
        }

        public int Column
        {
            get { return _column; }
            set { _column = value; }
        }

        public int StartIndex
        {
            get { return _startIndex; }
            set { _startIndex = value; }
        }

        public String SchemaKey
        {
            get { return _schemaKey; }
            set { _schemaKey = value; }
        }

        public Schema(String schema)
        {
            if (schema.Length != 4)
                throw new Exception(EXCEPTION_INVALID_NUMER_KEY_LENGTH);

            foreach(char c in schema.Substring(1))
                if (!Char.IsNumber(c))
                    throw new Exception(EXCEPTION_INVALID_NUMER_KEY);

            _schemaKey = schema;

            ValidateSchema();
        }

        private void ValidateSchema()
        {
            int layout = Convert.ToInt32(_schemaKey.Substring(2, 2)),
                startIndex = GetStartIndex(_schemaKey.Substring(0,1)),
                longedge = Convert.ToInt32(_schemaKey.Substring(1, 1));

            try
            {
                SetEdges(layout, longedge);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            if(startIndex > 0 && (startIndex < _column || _row == 1))
                _startIndex = startIndex;

            else
                throw new Exception(EXCEPTION_INVALID_NUMER_KEY_START_INDEX);
        }

        private int GetStartIndex(String val)
        {
            if (Char.IsNumber(val[0]))
                return Convert.ToInt32(val);

            else if(Char.IsLetter(val[0]))
                return Convert.ToInt32(Char.ToUpper(val[0])) - 55;
                
            return -1;
        }

        //1 for long side = row
        //0 for long side = col
        private void SetEdges(int layout, int longedge)
        {
            bool found = false;
            int row = 0, column = 0;

            if( longedge == 0 || longedge == 1)
            {
                for(int i = 1; i < layout && !found; ++i)
                {
                    if((layout - i) * i == ALPHABET_SIZE_24 || (layout - i) * i == ALPHABET_SIZE_25 || (layout - i) * i == ALPHABET_SIZE_26 )
                    {
                        if(longedge == 1 && i > 1)
                        {
                            row = layout - i;
                            column = i;
                            found = true;
                        }

                        else if(longedge == 0 || i == 1)
                        {
                            row = i;
                            column = layout - i;
                            found = true;
                        }
                    }
                }

                if(!found)
                    throw new Exception(EXCEPTION_INVALID_NUMER_KEY);

                else
                {
                    _row = row;
                    _column = column;
                    _alphabetSize = _row * _column;
                }
            }

            else
                throw new Exception(EXCEPTION_INVALID_NUMER_KEY);
        }
    }
}
