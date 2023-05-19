﻿using PhoneNumbers;
using System.Security.Cryptography;
using System.Text;

namespace HueFestivalTicket.Middlewares
{
    public class Generate
    {
        public static string GetMD5Hash(string str)
        {
            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(str);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public static string GetRefreshToken()
        {
            var random = new byte[32];
            using (var ran = RandomNumberGenerator.Create())
            {
                ran.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }

        public static string GetVerifyCode()
        {
            Random random = new Random();
            int verificationCode = random.Next(100000, 999999);
            return verificationCode.ToString();
        }

        public static string GetPhoneNumber(string phoneNumber)
        {
            PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
            PhoneNumber parsedPhoneNumber = phoneNumberUtil.Parse(phoneNumber, "VN");

            string internationalPhoneNumber = phoneNumberUtil.Format(parsedPhoneNumber, PhoneNumberFormat.E164);
            return internationalPhoneNumber;
        }
    }
}
