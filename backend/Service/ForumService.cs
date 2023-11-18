using System.Collections;
using System.Net.Mail;
using System.Security.Authentication;
using backend.DAL;
using backend.Model;
using MimeKit;
using MailKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace backend.Service;

public class ForumService
{
    private readonly ForumDAL _forumDal;

    public ForumService(ForumDAL forumDal)
    {
        _forumDal = forumDal;
    }

    /*
     * //Encrypts the password with a workFactor of 15.
     * WorkFactor slows the encryption ensuring brute-forcing takes longer amounts of time
     */
    public bool Register(User user) 
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password, 12);

        //Replaces existing password with an encrypted created by Bcrypt
        user.Password = hashedPassword;
        
       return _forumDal.Register(user);
    }
    


    public void DeleteUser(int id)
    {
        _forumDal.DeleteUser(id);
    }
    
    public IEnumerable<User> getUserFeed()
    {
        return _forumDal.GetUserFeed();
    }

    public User Login(User user)
    {
        try
        {
            var userToCheck = _forumDal.login(user);
            if (BCrypt.Net.BCrypt.Verify(user.Password, userToCheck.Password))
            {
                return userToCheck;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred during login" + e.Message);
        }
        return null;
    }
}