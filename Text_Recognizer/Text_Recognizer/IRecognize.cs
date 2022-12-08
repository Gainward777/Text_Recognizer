using System;
using System.Collections.Generic;
using System.Text;

namespace Text_Recognizer
{
    public interface IRecognize
    {
        string Recognize(byte[] input);
    }
}
