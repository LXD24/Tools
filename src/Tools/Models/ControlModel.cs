using System;
using AduSkin.Utility.Extend;

namespace Tools.Models
{
    public class ControlModel
    {
        public ControlModel(string title, Type content,  string xaml = "", string code = "", string tags = "")
        {
            Title = title;
            TitlePinyin = StringExtend.GetPinyin(title);
            Content = content;
            Tags = tags;
            XAML = xaml;
            Code = code;
        }

        public ControlModel()
        {
            Tags = string.Empty;
        }


        public int Id { get; private set; }

        public string Title { get; set; }

        public string TitlePinyin { get; set; }

        public Type Content { get; set; }

        public string XAML { get; set; }

        public string Code { get; set; }

        public string Tags { get; set; }

    }
}
