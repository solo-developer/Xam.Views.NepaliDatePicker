using System;
using System.Collections.Generic;
using System.Text;

namespace DateConverter.Core.Library
{
    public interface iDateConverter
    {
        EnglishDate ToAD(string nepali_date);
        NepaliDate ToBS(DateTime english_date, NepaliDate.DateFormats date_format = NepaliDate.DateFormats.mDy);
    }
}
