using BusinessLayer.Interface;
using CommonLayer;
using CommonLayer.DTOS;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL _userRL;

        public UserBL(IUserRL userRL)
        {
            _userRL = userRL;

        }

        public UserResponse RegisterUser(UserReq RegData)
        {
            var userRes = new UserResponse();
            if (RegData != null)
            {
                var encPassword = EncodePassword(RegData.Password);
                User user = new User
                {
                    UserName = RegData.UserName,
                    Email = RegData.Email,
                    Password = encPassword,
                    Role = Roles.User,
                    isActive = true,
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = DateTime.UtcNow
                };
                var errors = _userRL.ValidateUserExists(user, "Insert");
                userRes.errors = new List<string>();
                if (errors.Count == 0)
                {
                    userRes = _userRL.RegisterUser(user);
                    if (userRes.UserID != 0)
                    {
                        return userRes;
                    }
                }
                userRes.errors.AddRange(errors);
            }
            return userRes;
        }

        /// <summary>
        /// ability to encrypt password using UTF8 standards
        /// </summary>
        /// <param name="password">user password</param>
        /// <returns>encrypted password</returns>
        private string EncodePassword(string password)
        {
            byte[] encData = new byte[password.Length];
            encData = Encoding.UTF8.GetBytes(password);
            string encodedData = Convert.ToBase64String(encData);
            return encodedData;
        }


        public ResponseDto Login(LoginReq loginReq)
        {
            return _userRL.LoginUser(loginReq);
        }

        public ResponseDto UpdateUser(UpdateReq updateReq)
        {
            ResponseDto response = new ResponseDto();
            User user = new User
            {
                UserID = updateReq.UserID,
                UserName = updateReq.UserName,
                Email = updateReq.Email
            };
            response.Errors = _userRL.ValidateUserExists(user, "Update");
            if (response.Errors.Count == 0)
            {
                return _userRL.UpdateUserData(updateReq);
            }
            response.Status = false;
            response.Message = new List<string> { "Update failed" };
            response.Result = updateReq;
            return response;

        }



        public ResponseDto DeleteUser()
        {
            throw new NotImplementedException();
        }

        public ResponseDto ActivateDeactivateUser(long userId)
        {
            return _userRL.ActivateDeactivateUser(userId);
        }
    }

}
