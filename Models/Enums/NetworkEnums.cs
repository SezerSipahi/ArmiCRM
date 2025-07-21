using System;

namespace deneme.Models.Enums
{
    public enum KullanimSekli
    {
        Lokal,
        Uzak,
        Hibrit
    }

    public enum LisansTuru
    {
        Perpetual,
        Subscription,
        Trial
    }

    public enum YedeklemeDurumu
    {
        Gunluk,
        Haftalik,
        Aylik,
        Yapilmiyor
    }

    public enum KiralamaModeli
    {
        Aylik,
        Yillik,
        Perpetual
    }

    public enum ParametreTuru
    {
        Genel,
        Teknik,
        Mali,
        Diger
    }
} 