using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string CarAdded = "Araç eklendi";
        public static string CarNameAndDailyPriceInvalid = "Ekleme işlemi başarısız. Araç ismini en az 2 karakter ve araç günlük fiyatını 0'dan büyük giriniz.";
        public static string CarNameInvalid = "Ekleme işlemi başarısız. Araç ismini en az 2 karakter giriniz.";
        public static string CarDailyPriceInvalid = "Ekleme işlemi başarısız. Araç günlük fiyatını 0'dan büyük giriniz.";
        public static string CarNotFound = "Araç bulunamadı.";
        public static string CarCheckImageLimited = "resim limiti aşıldı";
        public static string AuthorizationDenied = "yetkin yok";
        public static string UserRegistered = "Kayıt oldu";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Parola hatalı";
        public static string SuccessfulLogin = "Başarılı giriş";
        public static string UserAlreadyExists = "Kullanıcı mevcut";
        public static string AccessTokenCreated = "Token oluşturuldu";
    }
}
