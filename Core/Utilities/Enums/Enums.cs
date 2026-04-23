using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Enums
{
        public enum TransactionType : short
        {
            Purchase = 1,       // Satın Alma (Giriş)
            Sale = 2,           // Satış (Çıkış)
            ProductionInput = 3,// Üretim Girişi
            ProductionOutput = 4, // Üretim Çıkışı (Hammadde düşümü)
            ReturnIn = 5,       // İade Alım
            ReturnOut = 6       // İade Gönderim
        }
}
