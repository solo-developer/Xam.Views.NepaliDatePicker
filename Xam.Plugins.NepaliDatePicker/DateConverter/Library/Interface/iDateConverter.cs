using DateConverter.Core.Enums;
using System;

namespace DateConverter.Core.Library
{
    public interface iDateConverter
    {
        EnglishDate ToAD(string nepali_date);
        NepaliDate ToBS(DateTime english_date, DateFormats date_format = DateFormats.mDy);
    }
}
