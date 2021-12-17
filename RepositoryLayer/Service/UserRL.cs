using CommonLayer;
using CommonLayer.DTOS;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly ExpenseManagerContext _context;
        private readonly string _key;
        public UserRL(ExpenseManagerContext context, IConfiguration _config)
        {
            _context = context;
            _key = _config.GetSection("Settings").GetSection("SecretKey").Value;
        }

        public UserResponse RegisterUser(User RegData)
        {
            int userAdded = 0;
            _context.Users.Add(RegData);
            userAdded = _context.SaveChanges();
            if (userAdded != 0)
            {
                UserResponse res = new UserResponse
                {
                    UserID = RegData.UserID,
                    UserName = RegData.UserName,
                    Email = RegData.Email,
                    Role = RegData.Role,
                    isActive = RegData.isActive
                };
                res.Token = GenerateToken(res);
                return res;
            }
            return null;

        }

        /// <summary>
        /// ability to generate jwt token with 10mins of expiry time.
        /// </summary>
        /// <param name="userEmail">userEmail</param>
        /// <param name="userId">user id</param>
        /// <returns>jwt token in string format</returns>
        private string GenerateToken(UserResponse userData)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key);
            var tokenDescpritor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, userData.UserName),
                        new Claim(ClaimTypes.Email, userData.Email),
                        new Claim("userId", userData.UserID.ToString(), ClaimValueTypes.Integer64),
                        new Claim(ClaimTypes.Role , userData.Role),
                        new Claim("isActive", userData.isActive.ToString(), ClaimValueTypes.Boolean)
                    }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescpritor);
            string jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }

        public List<string> ValidateUserExists(User userData, string operation)
        {
            var errors = new List<string>();
            IQueryable<User> users = _context.Users;

            if (operation == "Update")
            {
                users = _context.Users.Where(user => user.UserID != userData.UserID);
            }
            if (users.Any(x => x.UserName == userData.UserName))
                errors.Add("UserName Exists");
            if (users.Any(x => x.Email == userData.Email))
                errors.Add("User Already Exists");

            return errors;
        }

        /// <summary>
        /// ability to decrypt password into human readable format
        /// </summary>
        /// <param name="encPassword"></param>
        /// <returns>decrypted password</returns>
        private string DecodePassword(string encPassword)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            Decoder decoder = encoder.GetDecoder();
            byte[] todecodeByte = Convert.FromBase64String(encPassword);
            int charCount = decoder.GetCharCount(todecodeByte, 0, todecodeByte.Length);
            char[] decodeChar = new char[charCount];
            decoder.GetChars(todecodeByte, 0, todecodeByte.Length, decodeChar, 0);
            string password = new String(decodeChar);
            return password;
        }

        public ResponseDto LoginUser(LoginReq loginReq)
        {
            var existingUser = _context.Users.FirstOrDefault(user => user.Email == loginReq.Email);
            ResponseDto response = new ResponseDto();
            if (existingUser != null)
            {
                var password = DecodePassword(existingUser.Password);
                if (!string.IsNullOrEmpty(password) && password.Equals(loginReq.Password))
                {
                    UserResponse res = new UserResponse
                    {
                        UserID = existingUser.UserID,
                        UserName = existingUser.UserName,
                        Email = existingUser.Email,
                        Role = existingUser.Role,
                        isActive = existingUser.isActive
                    };
                    res.Token = GenerateToken(res);
                    response.Status = true;
                    response.Message = new List<string> { "Login Successful" };
                    response.Result = res;
                    return response;
                }
            }
            response.Status = false;
            response.Message = new List<string> { "Login failed" };
            response.Result = loginReq;
            return response;
        }

        public ResponseDto UpdateUserData(UpdateReq updateReq)
        {
            var existingUser = _context.Users.FirstOrDefault(user => user.UserID == updateReq.UserID);
            ResponseDto response = new ResponseDto();
            if (existingUser != null)
            {
                if (string.IsNullOrEmpty(updateReq.UserName))
                    existingUser.UserName = updateReq.UserName;
                if (string.IsNullOrEmpty(updateReq.Email))
                    existingUser.Email = updateReq.Email;
                existingUser.ModifiedOn = DateTime.UtcNow;
                int updated = _context.SaveChanges();
                if (updated == 1)
                {
                    response.Status = true;
                    response.Message = new List<string> { "Update Successful" };
                    response.Result = updateReq;
                    return response;
                }
            }
            response.Status = false;
            response.Message = new List<string> { "Update failed" };
            response.Result = updateReq;
            return response;
        }

        public ResponseDto ActivateDeactivateUser(long userId)
        {
            ResponseDto response = new ResponseDto();
            var existingUser = _context.Users.FirstOrDefault(user => user.UserID == userId);
            if (existingUser != null)
            {
                existingUser.isActive = !existingUser.isActive;
                existingUser.ModifiedOn = DateTime.UtcNow;
                int updated = _context.SaveChanges();
                if(updated ==1)
                {
                    response.Status = true;
                    response.Message = new List<string> { "Profile Updated" };
                    return response;
                }
            }
            response.Status = false;
            response.Message = new List<string> { "Profile Update failed" };
            return response;
        }
    }
}
