using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatToWatch.Business.Constant
{
    public class MessagesReturn
    {
        public static string NotFound => "Kayıt bulunamadı";
        public static string Add => "Kayıt eklendi";
        public static string Update => "Kayıt update edildi";
        public static string Delete => "Kayıt silindi";
        public static string GetAll => "Kayıtlar listelendi";
        public static string GetById => "Id'ye göre kayıt getirildi";
    }
}
