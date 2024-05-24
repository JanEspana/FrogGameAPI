using Microsoft.AspNetCore.Mvc;
using FrogGameAPI.Models;
using FrogGameAPI.Models.DTO;
using Microsoft.AspNetCore.Cors;
//using criptografia
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FrogGameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController
    {
        private readonly AppDbContext _context;
        private ResponseDTO _response;

        public UserController(AppDbContext context)
        {
            _context = context;
            _response = new ResponseDTO();
        }

        [HttpGet("GetUsers")]
        public ResponseDTO GetUsers()
        {
            try
            {
                IEnumerable<User> users = _context.Users.ToList();
                _response.Data = users;
            }
            catch (Exception ex)
            {
                _response.IsSuccessed = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("GetUserById")]
        public ResponseDTO GetUserById(int id)
        {
            try
            {
                User? user = _context.Users.FirstOrDefault(x => x.Id == id);
                _response.Data = user;
            }
            catch (Exception ex)
            {
                _response.IsSuccessed = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("PostUser")]
        public ResponseDTO PostUser(User user)
        {
            try
            {
                byte[] Key = Encoding.UTF8.GetBytes("0123456789abcdef0123456789abcdef");
                byte[] IV = Encoding.UTF8.GetBytes("1234567890123456");
                user.Password = Convert.ToBase64String(EncryptStringToBytes_Aes(user.Password, Key, IV));
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccessed = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut("PutUser")]
        public ResponseDTO PutUser(User user)
        {
            try
            {
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccessed = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete("DeleteUser")]
        public ResponseDTO DeleteUser(int id)
        {
            try
            {
                User? user = _context.Users.FirstOrDefault(x => x.Id == id);
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccessed = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        private static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;
        }
        private static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
    }
}
